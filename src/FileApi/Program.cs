using Files.Service;
using HouseWarehouseStore.Data.EF;
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

builder.Services.AddScoped<IFilesService, FilesService>();
builder.Services.AddScoped<IFilesArticleService, FilesArticleService>();
builder.Services.AddScoped<IFilesConfigsiteService, FilesConfigsiteService>();
builder.Services.AddScoped<IFilesBannerService, FilesBannerService>();
builder.Services.AddScoped<IFilesProductCategoryService, FilesProductCategoryService>();
builder.Services.AddScoped<IFilesProductService, FilesProductService>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();