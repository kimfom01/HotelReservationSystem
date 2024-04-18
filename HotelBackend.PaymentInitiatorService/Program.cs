using HotelBackend.Infrastructure.Infrastructure;
using HotelBackend.Infrastructure.Models;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMqOption>(builder.Configuration.GetSection(nameof(RabbitMqOption)));
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped(typeof(QueueService<>));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();