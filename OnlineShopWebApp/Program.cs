using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

using OnlineShop.BL;
using OnlineShop.BL.Domains;
using OnlineShop.BL.Interfaces;
using OnlineShop.Db;
using OnlineShop.Db.Entities;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Repositories;
using OnlineShop.Mappers;
using OnlineShopWebApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// �������� ������ ����������� �� ����� ������������
string connection = builder.Configuration.GetConnectionString("online_shop");

// ��������� �������� DataBaseContext � �������� ������� � ����������
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection));

// ��������� �������� IdentityContext � �������� ������� � ����������
builder.Services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(connection));

builder.Services.AddIdentity<UserEntity, IdentityRole>() // ����������� ������������ � ����
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

builder.Services.AddSingleton<ImagesProvider>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id?}");

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<UserEntity>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    IdentityInitializer.Initialize(userManager, roleManager);
}

app.Run();
