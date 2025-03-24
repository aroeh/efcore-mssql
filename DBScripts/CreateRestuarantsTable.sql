USE [Restuarant_Samples]
GO

/****** Object:  Table [dbo].[Restuarant]    Script Date: 8/17/2024 12:57:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Restuarant]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CuisineType] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](200) NULL,
	[Phone] [nvarchar](20) NOT NULL,
	CONSTRAINT [PK_Restuarant] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


