using HotelManagement.Web.Data;
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
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddHttpClient();

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

var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
await ContextSeed.SeedRolesAsync(serviceProvider);
await ContextSeed.SeedSuperAdminAsync(serviceProvider);

app.Run();
