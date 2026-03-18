using Microsoft.EntityFrameworkCore;
using StructuraFlow.Models;
using StructuraFlow.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IExcelReader, ExcelReader>();
builder.Services.AddScoped<IValidator, Validator>();

builder.Services.AddScoped<JsonExporter>();

builder.Services.AddDbContext<StructuralDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<InitMigrations>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



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
