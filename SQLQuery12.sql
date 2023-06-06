USE [master]
GO
/****** Object:  Database [bleems]    Script Date: 06-Jun-23 4:10:33 PM ******/
CREATE DATABASE [bleems]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bleems', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\bleems.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'bleems_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\bleems_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [bleems] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bleems].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bleems] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bleems] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bleems] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bleems] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bleems] SET ARITHABORT OFF 
GO
ALTER DATABASE [bleems] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [bleems] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bleems] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bleems] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bleems] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bleems] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bleems] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bleems] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bleems] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bleems] SET  DISABLE_BROKER 
GO
ALTER DATABASE [bleems] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bleems] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bleems] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bleems] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bleems] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bleems] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [bleems] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bleems] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [bleems] SET  MULTI_USER 
GO
ALTER DATABASE [bleems] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bleems] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bleems] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bleems] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [bleems] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [bleems] SET QUERY_STORE = OFF
GO
USE [bleems]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 06-Jun-23 4:10:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [numeric](18, 3) NULL,
	[Photo] [nvarchar](max) NULL,
	[CategoryId] [int] NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 06-Jun-23 4:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProductCategoryView]    Script Date: 06-Jun-23 4:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductCategoryView] AS
SELECT p.Id AS ProductId, p.Name AS ProductName, p.Description AS ProductDescription, p.Price AS ProductPrice, c.CategoryName
FROM dbo.Product p
JOIN dbo.Category c ON p.CategoryId = c.Id;
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([Id], [CategoryName]) VALUES (1, N'Flowers')
GO
INSERT [dbo].[Category] ([Id], [CategoryName]) VALUES (2, N'Choclates')
GO
INSERT [dbo].[Category] ([Id], [CategoryName]) VALUES (3, N'Gifts')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Photo], [CategoryId], [DateAdded]) VALUES (15, N'a new prod', N'desc', CAST(85.000 AS Numeric(18, 3)), N'E://', 1, CAST(N'2023-07-06T00:23:47.100' AS DateTime))
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Photo], [CategoryId], [DateAdded]) VALUES (16, N'random', N'random', CAST(99.000 AS Numeric(18, 3)), N'D://', 2, CAST(N'2022-01-06T00:56:23.750' AS DateTime))
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Photo], [CategoryId], [DateAdded]) VALUES (17, N'test', N'test', CAST(45.000 AS Numeric(18, 3)), N'C:\Users\hatim.raja\source\repos\bleemsMVC\bleemsMVC\wwwroot\ContractData\uploads\', 1, CAST(N'2023-06-06T13:34:51.157' AS DateTime))
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [Photo], [CategoryId], [DateAdded]) VALUES (18, N'test', N'test', CAST(45.000 AS Numeric(18, 3)), N'C:\Users\hatim.raja\source\repos\bleemsMVC\bleemsMVC\wwwroot\ProductData\uploads\logo manal 3000_3000 (1).png', 2, CAST(N'2023-06-06T13:40:49.143' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
/****** Object:  StoredProcedure [dbo].[GetItemDetails]    Script Date: 06-Jun-23 4:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetItemDetails]
    @ProductId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ProductName NVARCHAR(50);
    DECLARE @ProductDescription NVARCHAR(MAX);
    DECLARE @ProductPrice NUMERIC(18, 3);
    DECLARE @Currency NVARCHAR(3) = 'KWD';

    SELECT @ProductName = Name, @ProductDescription = Description, @ProductPrice = Price
    FROM dbo.Product
    WHERE Id = @ProductId;

    SELECT @ProductName AS ProductName, @ProductDescription AS ProductDescription, 
           CONCAT(@ProductPrice, ' ', @Currency) AS ProductPriceWithCurrency;
END;
GO
USE [master]
GO
ALTER DATABASE [bleems] SET  READ_WRITE 
GO
