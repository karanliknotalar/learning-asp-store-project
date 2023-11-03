using StoreApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
// builder.Services.AddEndpointsApiExplorer();

//Custom extension
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureSession();
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureRouting();
builder.Services.ConfigureApplicationCookie();
//Custom Extension End

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoint.MapControllerRoute(
        "default",
        "{Controller=Home}/{Action=Index}/{id?}"
    );

    endpoint.MapRazorPages();

    endpoint.MapControllers();
});

//Custom Extension
app.AutoMigration();
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();
//Custom Extension End

app.Run();