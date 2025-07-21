using KafkaFlow;
using KafkaFlow.Serializer;
using Registry.Business;
using Registry.Business.Abstraction;
using Registry.Business.Kafka.TransactionalOutbox;
using Registry.Business.Profiles;
using Registry.Repository;
using Registry.Repository.Abstraction;
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