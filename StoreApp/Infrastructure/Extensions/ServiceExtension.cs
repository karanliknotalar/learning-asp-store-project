using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructure.Extensions;

public static class ServiceExtension
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
        {
            //Sql Lite
            options.UseSqlite(configuration.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("StoreApp"));
            
            // MssSql Server
            // options.UseSqlServer(configuration.GetConnectionString("mssqlConnection"),
            //     b => b.MigrationsAssembly("StoreApp"));
            //
            
            options.EnableSensitiveDataLogging();
        });
        
        // services.AddDbContextPool<RepositoryContext>(options =>
        // {
        //     // // Polemo MySql Sql Server
        //     string connectionString = configuration.GetConnectionString("mysqlConnection");
        //     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), 
        //         b => b.MigrationsAssembly("StoreApp"));
        //     
        //     
        //     options.EnableSensitiveDataLogging();
        // });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 5;
        }).AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailTokenProvider<IdentityUser>>(TokenOptions.DefaultEmailProvider);
        
        services.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromHours(1));
  
    }

    public static void ConfigureSession(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.Cookie.Name = "StoreApp.Session";
            options.IdleTimeout = TimeSpan.FromMinutes(10);
        });
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(SessionCart.GetCart);
    }

    public static void ConfigureRepositoryRegistration(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }

    public static void ConfigureServiceRegistration(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IOrderService, OrderManager>();
        services.AddScoped<IAuthService, AuthManager>();
    }

    public static void ConfigureRouting(this IServiceCollection services)
    {
        services.AddRouting(options =>
        {
            // options.LowercaseQueryStrings = false;
            options.LowercaseUrls = true;
            options.AppendTrailingSlash = false;
        });
    }

    public static void ConfigureApplicationCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = new PathString("/Account/AccessDenied"); // is Default Path
            // options.LoginPath = new PathString("/Account/Login");
            // options.LogoutPath = new PathString("/Account/Logout");
            // options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            // options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        });
    }
}