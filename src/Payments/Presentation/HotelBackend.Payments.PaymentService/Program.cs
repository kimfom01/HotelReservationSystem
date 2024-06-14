using HotelBackend.Payments.Application;
using HotelBackend.Payments.Infrastructure;
using HotelBackend.Payments.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "any origin";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(opt =>
{
    opt.AddSimpleConsole(options =>
    {
        options.TimestampFormat = "[HH:mm:ss] ";
    });
});
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