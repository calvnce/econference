using Econference.Data;
using Econference.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Database context
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<PasswordHasher<ApplicationUser>>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Resolve the RoleManager service
using var serviceScope = app.Services.CreateScope();
var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

// Seed roles
SeedRoles(roleManager).Wait();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LogIn}/{id?}"); // Correct default route


app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
{
    // Add roles if they don't exist
    if (!await roleManager.RoleExistsAsync("USER"))
    {
        await roleManager.CreateAsync(new IdentityRole("USER"));
    }

    if (!await roleManager.RoleExistsAsync("ADMIN"))
    {
        await roleManager.CreateAsync(new IdentityRole("ADMIN"));
    }
}