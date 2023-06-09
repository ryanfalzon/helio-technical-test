USE [master]
GO
/****** Object:  Database [PhoneBook]    Script Date: 16/03/2023 13:39:16 ******/
CREATE DATABASE [PhoneBook]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PhoneBook', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PhoneBook.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PhoneBook_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PhoneBook_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PhoneBook] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PhoneBook].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PhoneBook] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PhoneBook] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PhoneBook] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PhoneBook] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PhoneBook] SET ARITHABORT OFF 
GO
ALTER DATABASE [PhoneBook] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PhoneBook] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PhoneBook] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PhoneBook] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PhoneBook] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PhoneBook] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PhoneBook] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PhoneBook] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PhoneBook] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PhoneBook] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PhoneBook] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PhoneBook] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PhoneBook] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PhoneBook] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PhoneBook] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PhoneBook] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PhoneBook] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PhoneBook] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PhoneBook] SET  MULTI_USER 
GO
ALTER DATABASE [PhoneBook] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PhoneBook] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PhoneBook] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PhoneBook] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PhoneBook] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PhoneBook] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PhoneBook] SET QUERY_STORE = OFF
GO
USE [PhoneBook]
GO
/****** Object:  Schema [PhoneBook]    Script Date: 16/03/2023 13:39:16 ******/
CREATE SCHEMA [PhoneBook]
GO
/****** Object:  Table [PhoneBook].[Company]    Script Date: 16/03/2023 13:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[RegistrationDate] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [PhoneBook].[Person]    Script Date: 16/03/2023 13:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [PhoneBook].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/03/2023 13:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [PhoneBook].[Company] ON 

INSERT [PhoneBook].[Company] ([Id], [Name], [RegistrationDate]) VALUES (3, N'Company A', CAST(N'2023-03-16T11:10:47.6460000+00:00' AS DateTimeOffset))
INSERT [PhoneBook].[Company] ([Id], [Name], [RegistrationDate]) VALUES (20, N'Company B', CAST(N'2023-03-16T12:29:00.9550000+00:00' AS DateTimeOffset))
INSERT [PhoneBook].[Company] ([Id], [Name], [RegistrationDate]) VALUES (22, N'Company C', CAST(N'2023-03-16T12:29:00.9550000+00:00' AS DateTimeOffset))
SET IDENTITY_INSERT [PhoneBook].[Company] OFF
GO
SET IDENTITY_INSERT [PhoneBook].[Person] ON 

INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (6, N'Ryan', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (7, N'Luke', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (8, N'Daniel', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (9, N'Petri', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (10, N'Pieter', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (11, N'Jacques', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (12, N'Max', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (13, N'Danielle', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (14, N'Matthew', N'21249200', N'Some Addess', 3)
INSERT [PhoneBook].[Person] ([Id], [Name], [PhoneNumber], [Address], [CompanyId]) VALUES (15, N'Willie', N'21249200', N'Some Addess', 3)
SET IDENTITY_INSERT [PhoneBook].[Person] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230315151659_InitialCreate', N'6.0.15')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230316113817_AddUniqueConstraint', N'6.0.15')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Company_Name]    Script Date: 16/03/2023 13:39:16 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Company_Name] ON [PhoneBook].[Company]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Person_CompanyId]    Script Date: 16/03/2023 13:39:16 ******/
CREATE NONCLUSTERED INDEX [IX_Person_CompanyId] ON [PhoneBook].[Person]
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [PhoneBook].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Company_CompanyId] FOREIGN KEY([CompanyId])
REFERENCES [PhoneBook].[Company] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [PhoneBook].[Person] CHECK CONSTRAINT [FK_Person_Company_CompanyId]
GO
USE [master]
GO
ALTER DATABASE [PhoneBook] SET  READ_WRITE 
GO
