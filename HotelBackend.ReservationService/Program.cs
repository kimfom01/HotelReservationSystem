using System.Reflection;
using System.Text.Json.Serialization;
using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Infrastructure;
using HotelBackend.ReservationService.Repositories;
using HotelBackend.ReservationService.Repositories.Implementations;
using HotelBackend.ReservationService.Reservation;
using HotelBackend.ReservationService.Services;
using HotelBackend.ReservationService.Services.Implementations;
using HotelBackend.ReservationService.Utils;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "any origin";

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMqOption>(builder.Configuration.GetSection(nameof(RabbitMqOption)));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy => policy.WithOrigins().AllowAnyOrigin());
});
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else
    {
        options.UseNpgsql(builder.Configuration["DEFAULT_CONNECTION"]);
    }

    // options.UseInMemoryDatabase("DefaultConnection"); // For testing purpose... will revert to postgres when needed
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<QueueService>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
await DatabaseReset.SetupDatabase(scope, builder.Environment);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.MapControllers();

app.Run();