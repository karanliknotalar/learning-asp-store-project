using StoreApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//Custom extension
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureSession();
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureRouting();
//Custom Extension End

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseEndpoints(endpoint =>
{
    endpoint.MapAreaControllerRoute(name: "Admin", areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

    endpoint.MapControllerRoute("default", "{Controller=Home}/{Action=Index}/{id?}");

    endpoint.MapRazorPages();
});

//Custom Extension
app.AutoMigration();
app.ConfigureLocalization();
//Custom Extension End

app.Run();