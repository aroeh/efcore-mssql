USE [master]
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Restuarant_Samples')
 DROP DATABASE Restuarant_Samples;
GO


CREATE DATABASE [Restuarant_Samples]
GO



USE [Restuarant_Samples]
GO

/****** Object:  Table [dbo].[Franchise] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.Franchise', 'U') IS NOT NULL
   DROP TABLE [dbo].[Franchise]
GO

CREATE TABLE [dbo].[Franchise]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Established] [datetime] NULL
	CONSTRAINT [PK_Franchise] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [Restuarant_Samples]
GO

/****** Object:  Table [dbo].[Restuarant]    Script Date: 8/17/2024 12:57:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.Restuarant', 'U') IS NOT NULL
   DROP TABLE [dbo].[Restuarant]
GO

CREATE TABLE [dbo].[Restuarant]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [FranchiseId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CuisineType] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](200) NULL,
	[Phone] [nvarchar](20) NOT NULL,
	CONSTRAINT [PK_Restuarant] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
    CONSTRAINT [FK_Franchise_Id] FOREIGN KEY ([FranchiseId]) REFERENCES [dbo].[Franchise]([Id])
) ON [PRIMARY]
GO


USE [Restuarant_Samples]
GO

/****** Object:  Table [dbo].[RestuarantLocation]    Script Date: 8/17/2024 1:04:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.RestuarantLocation', 'U') IS NOT NULL  
   DROP TABLE [dbo].[RestuarantLocation]
GO

CREATE TABLE [dbo].[RestuarantLocation]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RestuarantId] [int] NOT NULL,
	[Street] [nvarchar](200) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[State] [char](2) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[ZipCode] [nvarchar](10) NOT NULL,
	CONSTRAINT [PK_RestuarantLocation] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	CONSTRAINT [FK_Restuarant_Id] FOREIGN KEY ([RestuarantId]) REFERENCES [dbo].[Restuarant]([Id])
) ON [PRIMARY]
GO



USE [Restuarant_Samples]
GO

/****** Object:  Table [dbo].[MenuItem] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.MenuItem', 'U') IS NOT NULL  
   DROP TABLE [dbo].[MenuItem]
GO

CREATE TABLE [dbo].[MenuItem]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Price] [decimal](4,2) NOT NULL
	CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [Restuarant_Samples]
GO

/****** Object:  Table [dbo].[RestuarantMenuItem] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.RestuarantMenuItem', 'U') IS NOT NULL  
   DROP TABLE [dbo].[RestuarantMenuItem]
GO

CREATE TABLE [dbo].[RestuarantMenuItem]
(
	[RestuarantId] [int] NOT NULL,
	[MenuItemId] [int] NOT NULL,
	[RestuarantPrice] [decimal](4,2) NOT NULL,
	[Available] [bit] NOT NULL
	CONSTRAINT [PK_RestuarantMenuItem] PRIMARY KEY 
	(
		[RestuarantId],
        [MenuItemId]
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
    CONSTRAINT [FK_RestuarantMenuItem_Restuarant_Id] FOREIGN KEY ([RestuarantId]) REFERENCES [dbo].[Restuarant]([Id]),
    CONSTRAINT [FK_RestuarantMenuItem_MenuItem_Id] FOREIGN KEY ([MenuItemId]) REFERENCES [dbo].[MenuItem]([Id])
) ON [PRIMARY]
GO
