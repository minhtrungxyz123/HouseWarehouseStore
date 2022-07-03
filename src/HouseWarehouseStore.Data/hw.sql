USE [master]
GO
/****** Object:  Database [HouseWarehouseStore]    Script Date: 7/3/2022 11:07:00 AM ******/
CREATE DATABASE [HouseWarehouseStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HouseWarehouseStore', FILENAME = N'/var/opt/mssql/data/HouseWarehouseStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HouseWarehouseStore_log', FILENAME = N'/var/opt/mssql/data/HouseWarehouseStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HouseWarehouseStore] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HouseWarehouseStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HouseWarehouseStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HouseWarehouseStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HouseWarehouseStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HouseWarehouseStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HouseWarehouseStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HouseWarehouseStore] SET  MULTI_USER 
GO
ALTER DATABASE [HouseWarehouseStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HouseWarehouseStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HouseWarehouseStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HouseWarehouseStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HouseWarehouseStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HouseWarehouseStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HouseWarehouseStore', N'ON'
GO
ALTER DATABASE [HouseWarehouseStore] SET QUERY_STORE = OFF
GO
USE [HouseWarehouseStore]
GO
/****** Object:  User [dockerr]    Script Date: 7/3/2022 11:07:01 AM ******/
CREATE USER [dockerr] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [docker]    Script Date: 7/3/2022 11:07:01 AM ******/
CREATE USER [docker] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [aspnet]    Script Date: 7/3/2022 11:07:01 AM ******/
CREATE USER [aspnet] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [dockerr]
GO
ALTER ROLE [db_owner] ADD MEMBER [aspnet]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [aspnet]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [aspnet]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [aspnet]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [aspnet]
GO
ALTER ROLE [db_datareader] ADD MEMBER [aspnet]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [aspnet]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [aspnet]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [aspnet]
GO
/****** Object:  Schema [HangFire]    Script Date: 7/3/2022 11:07:01 AM ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Schema [minhtrung_SQLLogin_1]    Script Date: 7/3/2022 11:07:01 AM ******/
CREATE SCHEMA [minhtrung_SQLLogin_1]
GO
/****** Object:  Table [dbo].[Abouts]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Abouts](
	[AboutID] [varchar](36) NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Image] [nvarchar](500) NULL,
	[CoverImage] [nvarchar](500) NULL,
	[Body] [nvarchar](max) NULL,
	[Sort] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Abouts] PRIMARY KEY CLUSTERED 
(
	[AboutID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [varchar](36) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[Address] [nvarchar](max) NULL,
	[Sex] [nvarchar](max) NULL,
	[Age] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Albums]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Albums](
	[AlbumID] [varchar](36) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ListImage] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](500) NULL,
	[Body] [nvarchar](max) NULL,
	[Sort] [int] NOT NULL,
	[Home] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Albums] PRIMARY KEY CLUSTERED 
(
	[AlbumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticleCategories]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleCategories](
	[ArticleCategoryId] [varchar](36) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Url] [nvarchar](500) NULL,
	[CategorySort] [int] NOT NULL,
	[CategoryActive] [bit] NOT NULL,
	[ParentId] [varchar](36) NULL,
	[ShowHome] [bit] NOT NULL,
	[ShowMenu] [bit] NOT NULL,
	[Slug] [nvarchar](100) NULL,
	[Hot] [bit] NOT NULL,
	[TitleMeta] [nvarchar](100) NULL,
	[DescriptionMeta] [nvarchar](max) NULL,
 CONSTRAINT [PK_ArticleCategories] PRIMARY KEY CLUSTERED 
(
	[ArticleCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[Id] [varchar](36) NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[View] [int] NOT NULL,
	[ArticleCategoryId] [varchar](36) NOT NULL,
	[Active] [bit] NOT NULL,
	[Hot] [bit] NOT NULL,
	[Home] [bit] NOT NULL,
	[Url] [nvarchar](300) NULL,
	[TitleMeta] [nvarchar](100) NULL,
	[KeyWord] [nvarchar](500) NULL,
	[DescriptionMetaTitle] [nvarchar](500) NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banners]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banners](
	[BannerID] [varchar](36) NOT NULL,
	[BannerName] [nvarchar](100) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[GroupId] [int] NULL,
	[Url] [nvarchar](500) NULL,
	[Soft] [int] NOT NULL,
	[CoverImage] [nvarchar](500) NULL,
	[Title] [nvarchar](100) NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_Banners] PRIMARY KEY CLUSTERED 
(
	[BannerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[RecordID] [varchar](36) NOT NULL,
	[CartID] [nvarchar](max) NOT NULL,
	[ProductID] [varchar](36) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Count] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Color] [nvarchar](max) NULL,
	[Size] [nvarchar](max) NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Collections]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collections](
	[CollectionID] [varchar](36) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Quantity] [int] NOT NULL,
	[Factory] [nvarchar](500) NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Sort] [int] NOT NULL,
	[Hot] [bit] NOT NULL,
	[Home] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[TitleMeta] [nvarchar](100) NULL,
	[Content] [nvarchar](max) NULL,
	[StatusProduct] [bit] NOT NULL,
	[BarCode] [nvarchar](50) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Collections] PRIMARY KEY CLUSTERED 
(
	[CollectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[ColorID] [varchar](36) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[NameColor] [nvarchar](max) NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[ColorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConfigSites]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigSites](
	[ConfigSiteID] [varchar](36) NOT NULL,
	[Facebook] [nvarchar](500) NULL,
	[GooglePlus] [nvarchar](500) NULL,
	[Youtube] [nvarchar](500) NULL,
	[Linkedin] [nvarchar](500) NULL,
	[Twitter] [nvarchar](500) NULL,
	[GoogleAnalytics] [nvarchar](4000) NULL,
	[LiveChat] [nvarchar](4000) NULL,
	[GoogleMap] [nvarchar](4000) NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[ContactInfo] [nvarchar](max) NULL,
	[FooterInfo] [nvarchar](max) NULL,
	[Hotline] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[CoverImage] [nvarchar](500) NULL,
	[SaleOffProgram] [nvarchar](max) NULL,
	[nameShopee] [nvarchar](100) NULL,
	[urlWeb] [nvarchar](100) NULL,
	[FbMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_ConfigSites] PRIMARY KEY CLUSTERED 
(
	[ConfigSiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactID] [varchar](36) NOT NULL,
	[Fullname] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](300) NOT NULL,
	[Mobile] [bigint] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](100) NOT NULL,
	[Body] [nvarchar](4000) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [varchar](36) NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[Path] [varchar](500) NULL,
	[Extension] [varchar](50) NULL,
	[MimeType] [varchar](255) NULL,
	[Size] [decimal](15, 2) NOT NULL,
	[CollectionId] [varchar](36) NULL,
	[ArticleId] [varchar](36) NULL,
	[ConfigSiteId] [varchar](36) NULL,
	[BannerId] [varchar](36) NULL,
	[ProductCategoryId] [varchar](36) NULL,
	[ProductId] [varchar](36) NULL,
	[AdminId] [varchar](36) NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[MemberId] [varchar](36) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Fullname] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](200) NULL,
	[Mobile] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[HomePage] [nvarchar](200) NULL,
	[Active] [bit] NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[ConfirmEmail] [bit] NOT NULL,
	[token] [nvarchar](max) NULL,
	[LockAccount] [bit] NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotiId] [varchar](36) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[NotiHeader] [nvarchar](max) NULL,
	[NotiBody] [nvarchar](max) NULL,
	[IsRead] [bit] NULL,
	[Url] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[ProductId] [varchar](36) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[OrderId] [varchar](36) NOT NULL,
	[Color] [nvarchar](450) NOT NULL,
	[Size] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC,
	[Size] ASC,
	[Color] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[MaDonHang] [nvarchar](50) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[Payment] [bit] NOT NULL,
	[TypePay] [int] NOT NULL,
	[Transport] [int] NOT NULL,
	[TransportDate] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
	[OrderMemberId] [varchar](36) NULL,
	[Viewed] [bit] NOT NULL,
	[ThanhToanTruoc] [int] NOT NULL,
	[Id] [varchar](36) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Body] [nvarchar](200) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Fullname] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](10) NULL,
	[Mobile] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[ProductCategorieID] [varchar](36) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Image] [nvarchar](500) NOT NULL,
	[CoverImage] [nvarchar](500) NOT NULL,
	[Url] [nvarchar](500) NULL,
	[Soft] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Home] [bit] NOT NULL,
	[ParentId] [varchar](36) NULL,
	[TitleMeta] [nvarchar](100) NULL,
	[DescriptionMeta] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[ProductCategorieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductLikes]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductLikes](
	[ProductLikeID] [varchar](36) NOT NULL,
	[ProductID] [varchar](36) NOT NULL,
	[MemberId] [varchar](36) NOT NULL,
	[ProductsProductID] [varchar](36) NULL,
 CONSTRAINT [PK_ProductLikes] PRIMARY KEY CLUSTERED 
(
	[ProductLikeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [varchar](36) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[ProductCategorieID] [varchar](36) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Factory] [nvarchar](501) NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[SaleOff] [decimal](18, 0) NOT NULL,
	[QuyCach] [nvarchar](500) NULL,
	[Sort] [int] NOT NULL,
	[Hot] [bit] NOT NULL,
	[Home] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[TitleMeta] [nvarchar](100) NULL,
	[DescriptionMeta] [nvarchar](max) NULL,
	[GiftInfo] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[StatusProduct] [bit] NOT NULL,
	[CollectionID] [varchar](36) NOT NULL,
	[BarCode] [nvarchar](50) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[CreateBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductSizeColors]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSizeColors](
	[Id] [varchar](36) NOT NULL,
	[ProductID] [varchar](36) NOT NULL,
	[ColorID] [varchar](36) NOT NULL,
	[SizeID] [varchar](36) NOT NULL,
	[ProductsProductID] [varchar](36) NULL,
 CONSTRAINT [PK_ProductSizeColors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewProducts]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewProducts](
	[Id] [varchar](36) NOT NULL,
	[ProductId] [varchar](36) NOT NULL,
	[NumberStart] [int] NOT NULL,
	[userID] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[StatusReview] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReviewProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sizes]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sizes](
	[SizeID] [varchar](36) NOT NULL,
	[SizeProduct] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sizes] PRIMARY KEY CLUSTERED 
(
	[SizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TagProducts]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagProducts](
	[TagID] [varchar](36) NOT NULL,
	[ProductID] [varchar](36) NOT NULL,
 CONSTRAINT [PK_TagProducts] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagID] [varchar](36) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Soft] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Uploads]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Uploads](
	[Id] [varchar](36) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Uploads] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserVouchers]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserVouchers](
	[id] [varchar](36) NOT NULL,
	[MaDonHang] [nvarchar](50) NULL,
	[SumHD] [decimal](18, 2) NOT NULL,
	[Code] [nvarchar](6) NOT NULL,
 CONSTRAINT [PK_UserVouchers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Videos]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[Id] [varchar](36) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Image] [nvarchar](500) NULL,
	[VideoLink] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[Home] [bit] NOT NULL,
	[Soft] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vouchers]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[Id] [varchar](36) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](6) NOT NULL,
	[Type] [bit] NOT NULL,
	[Condition] [bit] NOT NULL,
	[PriceUp] [decimal](18, 0) NOT NULL,
	[PriceDown] [decimal](18, 0) NOT NULL,
	[SumUse] [int] NOT NULL,
	[ReductionMax] [decimal](18, 0) NOT NULL,
	[Active] [bit] NOT NULL,
	[Value] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Key] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
	[ExpireAt] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CX_HangFire_Counter]    Script Date: 7/3/2022 11:07:01 AM ******/
CREATE CLUSTERED INDEX [CX_HangFire_Counter] ON [HangFire].[Counter]
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [bigint] NOT NULL,
	[StateId] [bigint] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [varchar](36) NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Queue] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [bigint] NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](200) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 7/3/2022 11:07:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [bigint] NOT NULL,
	[JobId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Admins] ([Id], [Username], [Password], [Active], [Role], [Image], [FullName], [CreateDate], [Address], [Sex], [Age], [Position], [Email]) VALUES (N'04C4E8DF-971D-4BB9-8D79-9F1A3EC287F6', N'admin', N'AQAAAAEAACcQAAAAEAY/MjFqkX4sjCiuoWNPpJlL7c4oN3gbMuzF5KBZ7H97ERJH9TZNG63NvK1HHM/J8A==', 1, N'Admin', N'1', N'1', CAST(N'2022-01-01T00:00:00.000' AS DateTime), N'1', N'1', N'1', N'1', N'1')
INSERT [dbo].[Admins] ([Id], [Username], [Password], [Active], [Role], [Image], [FullName], [CreateDate], [Address], [Sex], [Age], [Position], [Email]) VALUES (N'd63e5db2-3e03-4006-b294-0d51c7c38003', N'haidang', N'AQAAAAEAACcQAAAAEG4aMdjHAcxvXKrh5FVf4FdsWlF4/tcMpRL3/xoaLQsNGKALVBICfAvD5P0ze6sA4A==', 1, N'Admin', N'd63e5db2-3e03-4006-b294-0d51c7c38003', N'Lê Văn tư', CAST(N'2022-07-03T09:07:24.000' AS DateTime), N'15', N'123', N'1', N'123', N'123')
INSERT [dbo].[Admins] ([Id], [Username], [Password], [Active], [Role], [Image], [FullName], [CreateDate], [Address], [Sex], [Age], [Position], [Email]) VALUES (N'f2652991-1447-476d-b291-15cd7ba21ffd', N'trungpv', N'AQAAAAEAACcQAAAAECbpzufPQ7udyzG7wtC3eJoNhGApj1L1+UDvvLxMC3lJmMXsppSaHlV8zRxRgBF/6w==', 1, N'Admin', N'f2652991-1447-476d-b291-15cd7ba21ffd', N'Phạm Văn Trung', CAST(N'2022-06-29T18:55:22.000' AS DateTime), N'an dương', N'Nam', N'23', N'Quản lý', N'trungphamvanvn@gmail.com')
GO
INSERT [dbo].[ArticleCategories] ([ArticleCategoryId], [CategoryName], [Url], [CategorySort], [CategoryActive], [ParentId], [ShowHome], [ShowMenu], [Slug], [Hot], [TitleMeta], [DescriptionMeta]) VALUES (N'37ae580e-c556-4314-96e8-b97c2a442c4e', N'234', N'https://localhost:5100/ArticleCategory/Create', 23423, 0, NULL, 1, 1, N'1', 1, N'2342', N'<p>2342</p>')
INSERT [dbo].[ArticleCategories] ([ArticleCategoryId], [CategoryName], [Url], [CategorySort], [CategoryActive], [ParentId], [ShowHome], [ShowMenu], [Slug], [Hot], [TitleMeta], [DescriptionMeta]) VALUES (N'e01a6652-04dd-4603-9597-f9a8c3de3696', N'kinh điển', N'https://localhost:5100/ArticleCategory/Create', 1, 1, NULL, 1, 1, N'1', 1, N'kinh điển', N'<p>kinh điển<br></p>')
GO
INSERT [dbo].[Banners] ([BannerID], [BannerName], [Width], [Height], [Active], [GroupId], [Url], [Soft], [CoverImage], [Title], [Content]) VALUES (N'8cd6851e-e8bb-4eb1-b7bb-392016ef8309', N'tttt', 335, 243, 1, 4, N'https://localhost:5100/ArticleCategory/Create', 1, NULL, N'ểtrt', N'<p>7867867</p>')
GO
INSERT [dbo].[Collections] ([CollectionID], [Name], [Description], [Image], [Body], [Quantity], [Factory], [Price], [Sort], [Hot], [Home], [Active], [TitleMeta], [Content], [StatusProduct], [BarCode], [CreateDate], [CreateBy]) VALUES (N'd345c3d3-802d-4ba2-bd9c-6758ef622974', N'657', N'567', N'd345c3d3-802d-4ba2-bd9c-6758ef622974', N'<p>657</p>', 567, N'567', CAST(567 AS Decimal(18, 0)), 1, 1, 1, 1, N'45645', N'567', 0, N'567', CAST(N'2022-06-19T17:53:24.0000000' AS DateTime2), N'04C4E8DF-971D-4BB9-8D79-9F1A3EC287F6')
GO
INSERT [dbo].[Colors] ([ColorID], [Code], [NameColor]) VALUES (N'49acfb21-460e-4e9f-9216-6ab8fa92f504', N'#ff8a9b', N'đỏ')
GO
INSERT [dbo].[ConfigSites] ([ConfigSiteID], [Facebook], [GooglePlus], [Youtube], [Linkedin], [Twitter], [GoogleAnalytics], [LiveChat], [GoogleMap], [Title], [Description], [ContactInfo], [FooterInfo], [Hotline], [Email], [CoverImage], [SaleOffProgram], [nameShopee], [urlWeb], [FbMessage]) VALUES (N'a722d762-c5d3-49bc-97c2-c7cc8bd6277a', N'qưe', NULL, N'https://localhost:5100/ConfigSite/Create', N'https://localhost:5100/ConfigSite/Create', N'https://localhost:5100/ConfigSite/Create', N'5464', N'546', N'567', N'456', N'546', N'4546', N'45645', N'qưe', N'minhtrungxyz123@gmail.com', N'a722d762-c5d3-49bc-97c2-c7cc8bd6277a', N'45645', N'546', N'qưe', N'567')
INSERT [dbo].[ConfigSites] ([ConfigSiteID], [Facebook], [GooglePlus], [Youtube], [Linkedin], [Twitter], [GoogleAnalytics], [LiveChat], [GoogleMap], [Title], [Description], [ContactInfo], [FooterInfo], [Hotline], [Email], [CoverImage], [SaleOffProgram], [nameShopee], [urlWeb], [FbMessage]) VALUES (N'da9ce3cf-2ec3-4f7b-8069-9f91767ae753', N'rty', NULL, N'https://localhost:5100/ConfigSite/Create', N'https://localhost:5100/ConfigSite/Create', N'https://localhost:5100/ConfigSite/Create', N'rty', N'rty', N'yuiyu', N'yyyy', N'rty', N'rty', N'rtyrt', N'qưe', N'minhtrungxyz123@gmail.com', N'da9ce3cf-2ec3-4f7b-8069-9f91767ae753', N'rtyrty', N'qưe', N'rtyrt', N'yui')
GO
INSERT [dbo].[Contacts] ([ContactID], [Fullname], [Address], [Mobile], [Email], [Subject], [Body], [CreateDate]) VALUES (N'6ec6e91a-227b-4ea9-a67c-9f05d8a797d8', N'Lê Văn tư', N'15', 34545, N'minhtrungxyz123@gmail.com', N'345345', N'<p>345345</p>', CAST(N'2022-06-09T12:35:44.4850000' AS DateTime2))
INSERT [dbo].[Contacts] ([ContactID], [Fullname], [Address], [Mobile], [Email], [Subject], [Body], [CreateDate]) VALUES (N'99dbb87b-203f-4078-b5b8-56c36b1067cc', N'Lê Văn cường', N'15', 12312, N'minhtrungxyz123@gmail.com', N'123123', N'<p>123</p>', CAST(N'2022-06-09T12:32:56.2380000' AS DateTime2))
GO
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'196647b6-ee2a-414b-926f-0a9e6e0fea08', N'133008564461847521tx0nr4kpghb.jfif', N'/uploads/2022/06/28', N'jfif', N'image/jpeg', CAST(5161.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, N'1')
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'1c9ef20f-9545-4ce3-9122-e93c3b10a435', N'132999240378473517z41siwr4oia.jpg', N'/uploads/2022/06/17', N'jpg', NULL, CAST(21688.00 AS Decimal(15, 2)), NULL, NULL, NULL, N'8cd6851e-e8bb-4eb1-b7bb-392016ef8309', NULL, NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'32e5d2e0-4ac9-4fb9-bf99-a4947f53de64', N'133001096070311261db14ilidcpz.jpg', N'/uploads/2022/06/19', N'jpg', NULL, CAST(48403.00 AS Decimal(15, 2)), N'd345c3d3-802d-4ba2-bd9c-6758ef622974', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'42d2b64d-4f61-4cb3-946c-66777c0e76ff', N'133000158662873919oxe2al31321.jpg', N'/uploads/2022/06/18', N'jpg', NULL, CAST(9245.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, N'456', NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'5f3f07d1-f4ba-451e-97d5-435d19260c86', N'133012871932274375acymadidqts.jpg', N'/uploads/2022/07/03', N'jpg', NULL, CAST(249532.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, N'f2652991-1447-476d-b291-15cd7ba21ffd')
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'6d5cf043-2575-4167-a9dc-ca0ab793ba70', N'133000997572135999svjwwiizc0b.jpg', N'/uploads/2022/06/19', N'jpg', NULL, CAST(48403.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, N'ttttrrt', NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'72b9ccde-7804-450c-bd33-645f286850ab', N'133000997562863791itfgtuiywpy.jpg', N'/uploads/2022/06/19', N'jpg', NULL, CAST(104522.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, N'630df2ad-83b0-4cd7-ae77-9191d1f46e1e', NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'77a0f8ba-eb98-47c8-9f5d-aaff1d98c6b5', N'132996755079192856gwctc3bqyci.jpg', N'/uploads/2022/06/14', N'jpg', NULL, CAST(89576.00 AS Decimal(15, 2)), NULL, NULL, N'da9ce3cf-2ec3-4f7b-8069-9f91767ae753', NULL, NULL, NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'7c3dd429-c58f-4bb8-8154-3823d8921e64', N'133000802651796081ux5yjnijehz.jpg', N'/uploads/2022/06/19', N'jpg', NULL, CAST(9245.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, N'32423', NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'7fe63421-388e-47fd-a593-77bcc8d95d29', N'133000802640657677sk1gvbeec0m.jpg', N'/uploads/2022/06/19', N'jpg', NULL, CAST(156974.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, N'0d92c6e8-72e4-4849-8a5a-b8a493084b82', NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'a04ef279-495d-4763-b214-2ed5087fc150', N'133000150822724914x1yeusgwqec.jpg', N'/uploads/2022/06/18', N'jpg', NULL, CAST(104522.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, N'aaf89752-7d8b-41e1-96d4-dc262d685065', NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'bb3bbdee-1f28-4027-8676-c2c25a85304e', N'133001311678918280dpckid4rjlv.jpg', N'/uploads/2022/06/19', N'jpg', NULL, CAST(211530.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, N'b2799301-605b-4997-ac66-fc80e4c21396', NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'df743c4b-1242-44a5-9f02-c633f0929fa3', N'132996754916596471x5bxwzjxm20.jfif', N'/uploads/2022/06/14', N'jfif', NULL, CAST(4705.00 AS Decimal(15, 2)), NULL, NULL, N'a722d762-c5d3-49bc-97c2-c7cc8bd6277a', NULL, NULL, NULL, NULL)
INSERT [dbo].[Files] ([Id], [FileName], [Path], [Extension], [MimeType], [Size], [CollectionId], [ArticleId], [ConfigSiteId], [BannerId], [ProductCategoryId], [ProductId], [AdminId]) VALUES (N'f1b709fb-3c98-44c2-bc08-6151541e6c60', N'133012876617154019yknuvyv3xu5.jpg', N'/uploads/2022/07/03', N'jpg', NULL, CAST(211530.00 AS Decimal(15, 2)), NULL, NULL, NULL, NULL, NULL, NULL, N'd63e5db2-3e03-4006-b294-0d51c7c38003')
GO
INSERT [dbo].[ProductCategories] ([ProductCategorieID], [Name], [Image], [CoverImage], [Url], [Soft], [Active], [Home], [ParentId], [TitleMeta], [DescriptionMeta], [Body]) VALUES (N'0d92c6e8-72e4-4849-8a5a-b8a493084b82', N'32423', N'0d92c6e8-72e4-4849-8a5a-b8a493084b82', N'32423', N'3242', 3242, 1, 1, NULL, N'234', N'23432', N'<p>23423</p>')
INSERT [dbo].[ProductCategories] ([ProductCategorieID], [Name], [Image], [CoverImage], [Url], [Soft], [Active], [Home], [ParentId], [TitleMeta], [DescriptionMeta], [Body]) VALUES (N'630df2ad-83b0-4cd7-ae77-9191d1f46e1e', N'ttttrrt', N'630df2ad-83b0-4cd7-ae77-9191d1f46e1e', N'ttttrrt', N'45645', 54, 0, 0, N'0d92c6e8-72e4-4849-8a5a-b8a493084b82', N'tttttt', N'45645', N'<p>45645</p>')
GO
INSERT [dbo].[Products] ([ProductID], [Name], [Description], [Image], [Body], [ProductCategorieID], [Quantity], [Factory], [Price], [SaleOff], [QuyCach], [Sort], [Hot], [Home], [Active], [TitleMeta], [DescriptionMeta], [GiftInfo], [Content], [StatusProduct], [CollectionID], [BarCode], [CreateDate], [CreateBy]) VALUES (N'b2799301-605b-4997-ac66-fc80e4c21396', N'2342', N'32', N'b2799301-605b-4997-ac66-fc80e4c21396', N'<p>234</p>', N'0d92c6e8-72e4-4849-8a5a-b8a493084b82', 324, N'234', CAST(32 AS Decimal(18, 0)), CAST(324 AS Decimal(18, 0)), N'23', 324, 1, 1, 1, N'324', N'324', N'<p>234</p>', N'<p>234</p>', 1, N'd345c3d3-802d-4ba2-bd9c-6758ef622974', N'234', CAST(N'2022-06-19T23:52:44.0000000' AS DateTime2), N'04C4E8DF-971D-4BB9-8D79-9F1A3EC287F6')
GO
INSERT [dbo].[Sizes] ([SizeID], [SizeProduct]) VALUES (N'1ac50ed6-66c8-4b37-b14a-91d13421b2cf', N'xl')
INSERT [dbo].[Sizes] ([SizeID], [SizeProduct]) VALUES (N'70413a2f-af16-46dc-aca5-47fdec08d0ff', N'x')
GO
INSERT [dbo].[Vouchers] ([Id], [Name], [Code], [Type], [Condition], [PriceUp], [PriceDown], [SumUse], [ReductionMax], [Active], [Value]) VALUES (N'050f3e79-fc6e-4dca-a469-c53c5a6e7ee2', N'trung', N'HSX8UC', 1, 1, CAST(234 AS Decimal(18, 0)), CAST(23423 AS Decimal(18, 0)), 3, CAST(2323 AS Decimal(18, 0)), 1, CAST(23423.00 AS Decimal(18, 2)))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Articles_ArticleCategoryId]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_Articles_ArticleCategoryId] ON [dbo].[Articles]
(
	[ArticleCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Carts_ProductID]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_Carts_ProductID] ON [dbo].[Carts]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OrderDetails_ProductId]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_ProductId] ON [dbo].[OrderDetails]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductLikes_MemberId]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductLikes_MemberId] ON [dbo].[ProductLikes]
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductLikes_ProductsProductID]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductLikes_ProductsProductID] ON [dbo].[ProductLikes]
(
	[ProductsProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Products_CollectionID]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_Products_CollectionID] ON [dbo].[Products]
(
	[CollectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Products_ProductCategorieID]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_Products_ProductCategorieID] ON [dbo].[Products]
(
	[ProductCategorieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductSizeColors_ColorID]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductSizeColors_ColorID] ON [dbo].[ProductSizeColors]
(
	[ColorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductSizeColors_ProductsProductID]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductSizeColors_ProductsProductID] ON [dbo].[ProductSizeColors]
(
	[ProductsProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductSizeColors_SizeID]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductSizeColors_SizeID] ON [dbo].[ProductSizeColors]
(
	[SizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReviewProducts_ProductId]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReviewProducts_ProductId] ON [dbo].[ReviewProducts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TagProducts_TagID]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_TagProducts_TagID] ON [dbo].[TagProducts]
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_AggregatedCounter_ExpireAt]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_AggregatedCounter_ExpireAt] ON [HangFire].[AggregatedCounter]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Hash_ExpireAt]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Hash_ExpireAt] ON [HangFire].[Hash]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Job_ExpireAt]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_ExpireAt] ON [HangFire].[Job]
(
	[ExpireAt] ASC
)
INCLUDE([StateName]) 
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Job_StateName]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_StateName] ON [HangFire].[Job]
(
	[StateName] ASC
)
WHERE ([StateName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_List_ExpireAt]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_List_ExpireAt] ON [HangFire].[List]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Server_LastHeartbeat]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Server_LastHeartbeat] ON [HangFire].[Server]
(
	[LastHeartbeat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Set_ExpireAt]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_ExpireAt] ON [HangFire].[Set]
(
	[ExpireAt] ASC
)
WHERE ([ExpireAt] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Set_Score]    Script Date: 7/3/2022 11:07:03 AM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_Score] ON [HangFire].[Set]
(
	[Key] ASC,
	[Score] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Collections] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Members] ADD  DEFAULT (N'') FOR [Role]
GO
ALTER TABLE [dbo].[Members] ADD  DEFAULT (CONVERT([bit],(0))) FOR [ConfirmEmail]
GO
ALTER TABLE [dbo].[Members] ADD  DEFAULT (CONVERT([bit],(0))) FOR [LockAccount]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [OrderId]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [Address]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [Email]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [Fullname]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'') FOR [Mobile]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((1)) FOR [Sort]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (N'admin') FOR [CreateBy]
GO
ALTER TABLE [dbo].[Vouchers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Active]
GO
USE [master]
GO
ALTER DATABASE [HouseWarehouseStore] SET  READ_WRITE 
GO
