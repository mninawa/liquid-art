using KafkaFlow;
using KafkaFlow.Serializer;
using Template.Business;
using Template.Business.Abstraction;
using Template.Business.Kafka.TransactionalOutbox;
using Template.Business.Profiles;
using Template.Repository;
using Template.Repository.Abstraction;
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
// Kafka variables
var kafkaBrokers = builder.Configuration.GetSection("Kafka:Brokers").Value;

// Add Kafka Producer
builder.Services.AddKafka(
    kafka => kafka
        .UseConsoleLog()
        .AddCluster(
            cluster => cluster
                .WithBrokers(new[] { kafkaBrokers })
                .CreateTopicIfNotExists("template-created", 1, 1)
                .CreateTopicIfNotExists("device-created", 1, 1)
                .AddProducer(
                    "template",
                    producer => producer
                        .AddMiddlewares(m =>
                            m.AddSerializer<JsonCoreSerializer>()
                            )
                )
        )
);

// Add Kafka Outbox Message Processor as a hosted service
builder.Services.AddHostedService<OutboxMessageProcessor>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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