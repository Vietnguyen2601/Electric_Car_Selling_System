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


// Đăng ký Repository và Service vào DI container
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();



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

app.Run();
