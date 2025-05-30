using Notifications.Business.Abstraction;
using Notifications.Business;
using Notifications.Repository.Abstraction;
using Notifications.Repository;
using Notifications.Business.Profiles;
using KafkaFlow;
using KafkaFlow.Serializer;
using Notifications.Business.Kafka.MessageHandlers;
using Notifications.Business.Kafka.TransactionalOutbox;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<MongoConnectionSetting>(builder.Configuration.GetSection(nameof(MongoConnectionSetting)));

builder.Services.AddSingleton<IMongoConnectionSetting>(sp =>
    sp.GetRequiredService<IOptions<MongoConnectionSetting>>().Value);

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusiness, Business>();

object value = builder.Services.AddAutoMapper(typeof(AssemblyMarker));

// Adding HttpClient to communicate with other microservices
builder.Services.AddHttpClient<Registry.ClientHttp.Abstraction.IClientHttp, Registry.ClientHttp.ClientHttp>("RegistryClientHttp", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("RegistryClientHttp:BaseAddress").Value ?? "");
});

// Kafka variables
var kafkaBrokers = builder.Configuration.GetSection("Kafka:Brokers").Value;

// Add Kafka Producer and Consumer
builder.Services.AddKafka(
    kafka => kafka
        .UseConsoleLog()
        .AddCluster(
            cluster => cluster
                .WithBrokers(new[] { kafkaBrokers })
                .CreateTopicIfNotExists("notification-created", 1, 1)
                .AddProducer(
                    "notifications",
                    producer => producer
                        .DefaultTopic("notification-created")
                        .AddMiddlewares(m =>
                            m.AddSerializer<JsonCoreSerializer>()
                            )
                )
                .AddConsumer(consumer => consumer
                    .Topic("client-created")
                    .WithGroupId("notification-group")
                    .WithBufferSize(100)
                    .WithWorkersCount(10)
                    .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                    .AddMiddlewares(middlewares => middlewares
                        .AddDeserializer<JsonCoreDeserializer>()
                        .AddTypedHandlers(h => h
                            .AddHandler<ClientCreatedHandler>()
                        )
                    )
                )
                .CreateTopicIfNotExists("send-notification", 1, 1)
                .AddConsumer(consumer => consumer
                    .Topic("send-notification")
                    .WithGroupId("notification-group")
                    .WithBufferSize(100)
                    .WithWorkersCount(10)
                    .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                    .AddMiddlewares(middlewares => middlewares
                        .AddDeserializer<JsonCoreDeserializer>()
                        .AddTypedHandlers(h => h
                            .AddHandler<NotificationSenderHandler>()
                        )
                    )
                )
        )
);

// Add Kafka Outbox Message Processor as a hosted service
builder.Services.AddHostedService<OutboxMessageProcessor>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Start Kafka Bus
var kafkaBus = app.Services.CreateKafkaBus();
await kafkaBus.StartAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();