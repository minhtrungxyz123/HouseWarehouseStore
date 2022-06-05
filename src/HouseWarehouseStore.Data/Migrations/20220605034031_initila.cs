using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseWarehouseStore.Data.Migrations
{
    public partial class initila : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HangFire");

            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    AboutID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.AboutID);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "AggregatedCounter",
                schema: "HangFire",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_CounterAggregated", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    AlbumID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ListImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Home = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.AlbumID);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    View = table.Column<int>(type: "int", nullable: false),
                    ArticleCategoryId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Hot = table.Column<bool>(type: "bit", nullable: false),
                    Home = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TitleMeta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KeyWord = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DescriptionMetaTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCategory",
                columns: table => new
                {
                    ArticleCategoryId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategorySort = table.Column<int>(type: "int", nullable: false),
                    CategoryActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    ShowHome = table.Column<bool>(type: "bit", nullable: false),
                    ShowMenu = table.Column<bool>(type: "bit", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Hot = table.Column<bool>(type: "bit", nullable: false),
                    TitleMeta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescriptionMeta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategory", x => x.ArticleCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    BannerID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    BannerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    GroupId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Soft = table.Column<int>(type: "int", nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.BannerID);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    RecordID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    CartID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.RecordID);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    CollectionID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Factory = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Hot = table.Column<bool>(type: "bit", nullable: false),
                    Home = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    TitleMeta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusProduct = table.Column<bool>(type: "bit", nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.CollectionID);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ColorID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ColorID);
                });

            migrationBuilder.CreateTable(
                name: "ConfigSite",
                columns: table => new
                {
                    ConfigSiteID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GooglePlus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Youtube = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Linkedin = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GoogleAnalytics = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    LiveChat = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    GoogleMap = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hotline = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SaleOffProgram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nameShopee = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    urlWeb = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FbMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigSite", x => x.ConfigSiteID);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Mobile = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                });

            migrationBuilder.CreateTable(
                name: "Counter",
                schema: "HangFire",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Path = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Size = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Extension = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MimeType = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CollectionId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hash",
                schema: "HangFire",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Field = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_Hash", x => new { x.Key, x.Field });
                });

            migrationBuilder.CreateTable(
                name: "Job",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StateId = table.Column<long>(type: "bigint", nullable: true),
                    StateName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvocationData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arguments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobParameter",
                schema: "HangFire",
                columns: table => new
                {
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_JobParameter", x => new { x.JobId, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "JobQueue",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Queue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    FetchedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_JobQueue", x => new { x.Queue, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "List",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_List", x => new { x.Key, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomePage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(N'')"),
                    ConfirmEmail = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockAccount = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment = table.Column<bool>(type: "bit", nullable: false),
                    TypePay = table.Column<int>(type: "int", nullable: false),
                    Transport = table.Column<int>(type: "int", nullable: false),
                    TransportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderMemberId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    Viewed = table.Column<bool>(type: "bit", nullable: false),
                    ThanhToanTruoc = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "(N'')"),
                    Body = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(N'')"),
                    Fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(N'')"),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false, defaultValueSql: "(N'')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    OrderId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "((0))"),
                    Color = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.OrderId, x.ProductId, x.Size, x.Color });
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategorieID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Soft = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Home = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    TitleMeta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescriptionMeta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategorieID);
                });

            migrationBuilder.CreateTable(
                name: "ProductLike",
                columns: table => new
                {
                    ProductLikeID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ProductID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    MemberId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ProductsProductID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLike", x => x.ProductLikeID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategorieID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Factory = table.Column<string>(type: "nvarchar(501)", maxLength: 501, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    SaleOff = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    QuyCach = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    Hot = table.Column<bool>(type: "bit", nullable: false),
                    Home = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    TitleMeta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescriptionMeta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiftInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusProduct = table.Column<bool>(type: "bit", nullable: false),
                    CollectionID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'admin')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizeColor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ProductID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ColorID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    SizeID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ProductsProductID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizeColor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewProduct",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ProductId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    NumberStart = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusReview = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schema",
                schema: "HangFire",
                columns: table => new
                {
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_Schema", x => x.Version);
                });

            migrationBuilder.CreateTable(
                name: "Server",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastHeartbeat = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                schema: "HangFire",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_Set", x => new { x.Key, x.Value });
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    SizeID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    SizeProduct = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.SizeID);
                });

            migrationBuilder.CreateTable(
                name: "State",
                schema: "HangFire",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangFire_State", x => new { x.JobId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Soft = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "TagProduct",
                columns: table => new
                {
                    TagID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ProductID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagProduct", x => new { x.ProductID, x.TagID });
                });

            migrationBuilder.CreateTable(
                name: "Uploads",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserVoucher",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    MaDonHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SumHD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVoucher", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Home = table.Column<bool>(type: "bit", nullable: false),
                    Soft = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Type = table.Column<bool>(type: "bit", nullable: false),
                    Condition = table.Column<bool>(type: "bit", nullable: false),
                    PriceUp = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PriceDown = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    SumUse = table.Column<int>(type: "int", nullable: false),
                    ReductionMax = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_AggregatedCounter_ExpireAt",
                schema: "HangFire",
                table: "AggregatedCounter",
                column: "ExpireAt",
                filter: "([ExpireAt] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleCategoryId",
                table: "Article",
                column: "ArticleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductID",
                table: "Cart",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "CX_HangFire_Counter",
                schema: "HangFire",
                table: "Counter",
                column: "Key")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Hash_ExpireAt",
                schema: "HangFire",
                table: "Hash",
                column: "ExpireAt",
                filter: "([ExpireAt] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Job_ExpireAt",
                schema: "HangFire",
                table: "Job",
                column: "ExpireAt",
                filter: "([ExpireAt] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Job_StateName",
                schema: "HangFire",
                table: "Job",
                column: "StateName",
                filter: "([StateName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_List_ExpireAt",
                schema: "HangFire",
                table: "List",
                column: "ExpireAt",
                filter: "([ExpireAt] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_MemberId",
                table: "ProductLike",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_ProductsProductID",
                table: "ProductLike",
                column: "ProductsProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CollectionID",
                table: "Products",
                column: "CollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategorieID",
                table: "Products",
                column: "ProductCategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeColors_ColorID",
                table: "ProductSizeColor",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeColors_ProductsProductID",
                table: "ProductSizeColor",
                column: "ProductsProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeColors_SizeID",
                table: "ProductSizeColor",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewProducts_ProductId",
                table: "ReviewProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Server_LastHeartbeat",
                schema: "HangFire",
                table: "Server",
                column: "LastHeartbeat");

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Set_ExpireAt",
                schema: "HangFire",
                table: "Set",
                column: "ExpireAt",
                filter: "([ExpireAt] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_HangFire_Set_Score",
                schema: "HangFire",
                table: "Set",
                columns: new[] { "Key", "Score" });

            migrationBuilder.CreateIndex(
                name: "IX_TagProducts_TagID",
                table: "TagProduct",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AggregatedCounter",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "ArticleCategory");

            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "ConfigSite");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Counter",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Hash",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Job",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "JobParameter",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "JobQueue",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "List",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductLike");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductSizeColor");

            migrationBuilder.DropTable(
                name: "ReviewProduct");

            migrationBuilder.DropTable(
                name: "Schema",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Server",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Set",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "State",
                schema: "HangFire");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "TagProduct");

            migrationBuilder.DropTable(
                name: "Uploads");

            migrationBuilder.DropTable(
                name: "UserVoucher");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Voucher");
        }
    }
}
