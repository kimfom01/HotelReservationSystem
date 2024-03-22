using System.Reflection;
using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Repositories;
using HotelBackend.ReservationService.Repositories.Implementations;
using HotelBackend.ReservationService.Services;
using HotelBackend.ReservationService.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Sqids;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "any origin";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy => policy.WithOrigins().AllowAnyOrigin());
});
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseInMemoryDatabase("DefaultConnection"); // For testing purpose... will revert to postgres when needed
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddSingleton(new SqidsEncoder<int>(new()
{
    MinLength = 10,
}));

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var context = serviceProvider.GetRequiredService<DatabaseContext>();
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.MapControllers();

app.Run();