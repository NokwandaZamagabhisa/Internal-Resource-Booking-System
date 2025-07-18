using BookingSystem.DataAccess;
using BookingSystem.DataAccess.Bookings;
using BookingSystem.DataAccess.Resources;
using Microsoft.EntityFrameworkCore;
using ResourceBooking.Core.Gateways;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

///// Database Configuration
builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.MigrationsAssembly("BookingSystem.DataAccess");
        });
});

//register implementation of gateway
builder.Services.AddScoped<IResourceGateway, SqlServerResourceGateway>();
builder.Services.AddScoped<IBookingGateway, SqlServerBookingGateway>();


var app = builder.Build();


// Automatically apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
