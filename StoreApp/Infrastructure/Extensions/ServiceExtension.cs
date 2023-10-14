using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureDbContext(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("connection"),
                b => b.MigrationsAssembly("StoreApp"));
        });
    }

    public static void ConfigureSession(this IServiceCollection service)
    {
        service.AddDistributedMemoryCache();
        service.AddSession(options =>
        {
            options.Cookie.Name = "StoreApp.Session";
            options.IdleTimeout = TimeSpan.FromMinutes(10);
        });
        service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        service.AddScoped(SessionCart.GetCart);

    }

    public static void ConfigureRepositoryRegistration(this IServiceCollection service)
    {
        service.AddScoped<IRepositoryManager, RepositoryManager>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IOrderRepository, OrderRepository>();
    }

    public static void ConfigureServiceRegistration(this IServiceCollection service)
    {
        service.AddScoped<IServiceManager, ServiceManager>();
        service.AddScoped<ICategoryService, CategoryManager>();
        service.AddScoped<IProductService, ProductManager>();
        service.AddScoped<IOrderService, OrderManager>();
    }

    public static void ConfigureRouting(this IServiceCollection service)
    {
        service.AddRouting(options =>
        {
            options.LowercaseQueryStrings = false;
            options.LowercaseUrls = true;
            options.AppendTrailingSlash = false;
        });
    }
}