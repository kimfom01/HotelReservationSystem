using HotelManagement.Web.Data;
using HotelManagement.Web.Mappings;
using HotelManagement.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HotelManagementWebContextConnection")
    ?? throw new InvalidOperationException("Connection string 'HotelManagementWebContextConnection' not found.");

builder.Services.AddDbContext<HotelManagementWebContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<HotelManagementWebContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IGenericApiService<>), typeof(GenericApiService<>));
builder.Services.AddScoped<IRoomApiService, RoomApiService>();
builder.Services.AddScoped<IGuestApiService, GuestApiService>();
builder.Services.AddScoped(sp =>
{
    return new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAddress")
        ?? throw new InvalidOperationException("BaseAddress not found."))
    };
});
builder.Services.AddScoped<ManualMapper>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
await ContextSeed.SeedRolesAsync(serviceProvider);
await ContextSeed.SeedSuperAdminAsync(serviceProvider);

app.Run();
