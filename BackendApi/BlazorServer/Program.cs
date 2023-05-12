using BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Wrapper;
using DataAccess.Wrapper;
using Domain.Interfaces;
using BusinessLogic.Services;

namespace BlazorServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<pharmacy199Context>(
                optionsAction: options => options.UseSqlServer(
                    "Server=(LocalDB)\\MSSQLLocalDB;Database=apteka_new1;TrustServerCertificate=true;"));


            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IFilterrService, FilterrService>();
            builder.Services.AddScoped<IOrderrService, OrderrService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ISavedAdressService, SavedAdressService>();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}












