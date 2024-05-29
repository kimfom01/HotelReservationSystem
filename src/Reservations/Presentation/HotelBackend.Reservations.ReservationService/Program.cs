using System.Text.Json.Serialization;
using HotelBackend.Reservations.Application;
using HotelBackend.Reservations.Infrastructure;
using HotelBackend.Reservations.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "any origin";

builder
    .Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod());
});

builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.MapControllers();

app.Run();