using ElectricVehicleDealer.BLL.IServices;
using ElectricVehicleDealer.BLL.Services;
using ElectricVehicleDealer.DAL.DBContext;
using ElectricVehicleDealer.DAL.Repositories.IRepository;
using ElectricVehicleDealer.DAL.Repositories.Repository;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddRazorPages();
//Đăng ký DbContext với chuỗi kết nối từ appsettings.json
builder.Services.AddDbContext<ElectricVehicleDContext>();


// Đăng ký Repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IStationCarRepository, StationCarRepository>();
builder.Services.AddScoped<IStationRepository, StationRepository>();

//Đăng Ký Service
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IStationCarService, StationCarService>();




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapFallbackToPage("/Homepage/Home");


app.Run();
