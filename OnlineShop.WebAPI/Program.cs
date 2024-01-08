using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Serilog;

using OnlineShop.BL.Interfaces;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using OnlineShop.Mappers;
using OnlineShop.Db;
using OnlineShop.Db.Entities;
using OnlineShop.BL;

namespace OnlineShop.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        // ѕолучаем строку подключени€ из файла конфигурации
        string connection = builder.Configuration.GetConnectionString("online_shop");

        // добавл€ем контекст DataBaseContext в качестве сервиса в приложение
        builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection));

        // добавл€ем контекст IdentityContext в качестве сервиса в приложение
        builder.Services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(connection));

        builder.Services.AddIdentity<UserEntity, IdentityRole>() // ѕрив€зываем пользовател€ к роли
            .AddEntityFrameworkStores<IdentityContext>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.Cookie = new CookieBuilder
            {
                IsEssential = true
            };
        });




        // AddAsync services to the container.
        builder.Services.AddTransient<IFavoriteRepository, FavoritesDbRepository>();
        builder.Services.AddTransient<IProductsRepository, ProductsDbRepository>();
        builder.Services.AddTransient<ICartsRepository, CartsDbRepository>();
        builder.Services.AddTransient<IOrdersRepository, OrdersDbRepository>();

        builder.Services.AddTransient<IProductsServices, ProductsServices>();
        builder.Services.AddTransient<ICartsServices, CartsServices>();
        builder.Services.AddTransient<IFavoriteServices, FavoritesServices>();
        builder.Services.AddTransient<IUsersServices, UsersServices>();
        builder.Services.AddTransient<IRolesServices, RolesServices>();

        //builder.Services.AddSingleton<ImagesProvider>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));

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
