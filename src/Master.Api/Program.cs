using HouseWarehouseStore.Data.Dapper;
using HouseWarehouseStore.Data.EF;
using Master.Api.SignalRHubs;
using Master.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HouseWarehouseStoreDbContext>(options => options.UseSqlServer(
                            builder.Configuration.GetConnectionString("HouseWarehouseStoreDatabase")));

#region Add Service

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IKeycloakService, KeycloakService>();
builder.Services.AddScoped<IDapper, Dapperr>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IConfigSiteService, ConfigSiteService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IProductSizeColorService, ProductSizeColorService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ITagProductService, TagProductService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IProductLikeService, ProductLikeService>();
builder.Services.AddScoped<IAlbumSevice, AlbumSevice>();

#endregion Add Service

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
    .WithOrigins("http://localhost:5100")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .SetIsOriginAllowed(host => true)
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapHub<ConnectRealTimeHub>("/signalr");

app.Run();