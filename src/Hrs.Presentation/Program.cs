using System.Text.Json.Serialization;
using Hrs.Application;
using Hrs.Infrastructure;
using Hrs.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.OpenApi.Models;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "any origin";

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<AdminDataContext>("hrs-db",
    configureDbContextOptions: o => o.UseNpgsql(y =>
        y.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "admin")));

builder.AddNpgsqlDbContext<ReservationDataContext>("hrs-db",
    configureDbContextOptions: o => o.UseNpgsql(y =>
        y.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "reservations")));

builder.AddNpgsqlDbContext<PaymentDataContext>("hrs-db", null,
    configureDbContextOptions: o => o.UseNpgsql(y =>
        y.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "payments")));

builder.AddRabbitMQClient("rabbitmq");

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(opt => { opt.AddSimpleConsole(options => { options.TimestampFormat = "[HH:mm:ss] "; }); });
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            List<string> allowedOrigins = [];

            builder.Configuration.GetSection("AllowedOrigins").Bind(allowedOrigins);

            policy
                .WithOrigins(allowedOrigins.ToArray())
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please provide a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.ConfigureInfrastructureServices(builder.Environment);
builder.Services.ConfigureApplicationServices();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.ApplyMigrations();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapDefaultEndpoints();

await app.RunAsync();