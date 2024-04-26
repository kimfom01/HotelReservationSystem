using System.Text.Json.Serialization;
using HotelBackend.Application;
using HotelBackend.Infrastructure;
using HotelBackend.Persistence;
using HotelBackend.Persistence.Utils;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "any origin";

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy => policy.WithOrigins().AllowAnyOrigin());
});

builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigureApplicationServices(builder.Configuration);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
await DatabaseReset.SetupDatabase(scope, builder.Environment.IsDevelopment());

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.MapControllers();

app.Run();