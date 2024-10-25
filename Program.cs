using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Services;
using AppointmentSystem.Data;
using AppointmentSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register services here
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<VisitorService>();
builder.Services.AddScoped<OfficerService>();
builder.Services.AddScoped<WorkDaysService>();  // Ensure consistent naming
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<ActivitiesService>();

// Register DbContext for your database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.Run();
