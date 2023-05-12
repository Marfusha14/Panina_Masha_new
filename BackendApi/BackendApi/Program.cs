using System.Reflection;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
//using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Domain.Wrapper;
using Domain.Interfaces;
using DataAccess;
using DataAccess.Models;

namespace BackendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<pharmacy199Context>(
                optionsAction: options => options.UseSqlServer(
                    connectionString: "Server=(LocalDB)\\MSSQLLocalDB;Database=apteka_new1;TrustServerCertificate=true;"));
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(name:"v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Интернет-магазин Апи",
                    Description = "Интернет-Аптека где вы сможете заказать лекартсва",
                    Contact = new OpenApiContact
                    {
                        Name = "Пример контакта",
                        Url = new Uri("https://example.cfile")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Пример лицензии",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });

            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IFilterrService, FilterrService>();
            builder.Services.AddScoped<IOrderrService, OrderrService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ISavedAdressService, SavedAdressService>();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();


            
        }
    }
}

//Server = lab116 - p; Database = pharmacy199; User Id = sa; Password = 12345; TrustServerCertificate = true;