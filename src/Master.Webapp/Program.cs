using HouseWare.Base;
using HouseWarehouseStore.Data.EF;
using HouseWarehouseStore.Data.Repositories;
using HouseWarehouseStore.Data.UnitOfWork;
using LazZiya.ExpressLocalization;
using Master.Webapp.ApiClient;
using Master.Webapp.CustomHandler;
using Master.Webapp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("vi"),
};
builder.Services.AddControllersWithViews()
    .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
    {
        ops.UseAllCultureProviders = false;
        ops.ResourcesPath = "LocalizationResources";
        ops.RequestLocalizationOptions = o =>
        {
            o.SupportedCultures = cultures;
            o.SupportedUICultures = cultures;
            o.DefaultRequestCulture = new RequestCulture("vi");
        };
    });

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region Add DI

builder.Services.AddScoped<IAdminApiClient, AdminApiClient>();
builder.Services.AddScoped<IProductApiClient, ProductApiClient>();
builder.Services.AddScoped<ICollectionApiClient, CollectionApiClient>();
builder.Services.AddScoped<ISizeApiClient, SizeApiClient>();
builder.Services.AddScoped<IContactApiClient, ContactApiClient>();
builder.Services.AddScoped<ISendDiscordHelper, SendDiscordHelper>();
builder.Services.AddScoped<IArticleCategoryApiClient, ArticleCategoryApiClient>();
builder.Services.AddScoped<IArticlesApiClient, ArticlesApiClient>();
builder.Services.AddScoped<IConfigSiteApiClient, ConfigSiteApiClient>();
builder.Services.AddScoped<IVoucherApiClient, VoucherApiClient>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped<IBannerApiClient, BannerApiClient>();
builder.Services.AddScoped<IColorApiClient, ColorApiClient>();
builder.Services.AddScoped<IProductCategoryApiCient, ProductCategoryApiCient>();
builder.Services.AddScoped<ITagApiClient, TagApiClient>();
builder.Services.AddScoped<ITagProductApiClient, TagProductApiClient>();
builder.Services.AddScoped<IProductSizeColorApiClient, ProductSizeColorApiClient>();
builder.Services.AddScoped<IFilesApiClient, FilesApiClient>();
builder.Services.AddScoped<INotificationApiClient, NotificationApiClient>();
builder.Services.AddScoped<ICommentApiClient, CommentApiClient>();
builder.Services.AddScoped<IFollowApiClient, FollowApiClient>();
builder.Services.AddScoped<IMemberApiClient, MemberApiClient>();

#endregion Add DI

//DbContext
builder.Services.AddDbContext<HouseWarehouseStoreDbContext>(options => options.UseSqlServer(
                            builder.Configuration.GetConnectionString("HouseWarehouseStoreDatabase")));

//Repository
builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(RepositoryEF<>));

//Authorization
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
builder.Services.AddAuthorization(options =>
{
});

//AddCookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
 {
     config.LoginPath = "/Login/Index";
     config.AccessDeniedPath = "/Login/FalseLogin";
     config.ExpireTimeSpan = TimeSpan.FromHours(1);
     config.Cookie.HttpOnly = true;
     config.Cookie.IsEssential = true;
 });
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<PasswordHasherOptions>(option =>
{
    option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
    option.IterationCount = 12000;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();