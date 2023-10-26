using Api.Services;
using Api.Services.Implementations;
using DataAccess.Data;
using DataAccess.Repositories;
using DataAccess.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

var corsPolicy = "any origin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy => policy.WithOrigins().AllowAnyOrigin());
});
builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BackendConnection"));
    //options.UseInMemoryDatabase("TemporaryDb"); // For testing purpose... will revert to postgres when needed
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IDataServiceGeneric<>), typeof(DataServiceGeneric<>));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IReservationRoomService, ReservationRoomService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IRoomService, RoomService>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var context = serviceProvider.GetRequiredService<Context>();
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
