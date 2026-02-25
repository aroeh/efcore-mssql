CREATE TABLE [dbo].[Restuarant]
(
	[Id] VARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CuisineType] NVARCHAR(30) NOT NULL, 
    [Website] NVARCHAR(100) NULL, 
    [Phone] NVARCHAR(20) NULL, 
    [Street] NVARCHAR(150) NOT NULL, 
    [City] NVARCHAR(100) NOT NULL, 
    [State] CHAR(2) NOT NULL, 
    [Country] NVARCHAR(100) NOT NULL, 
    [ZipCode] NVARCHAR(10) NOT NULL, 
    CONSTRAINT [PK_Restuarant_Id] PRIMARY KEY ([Id])
)
