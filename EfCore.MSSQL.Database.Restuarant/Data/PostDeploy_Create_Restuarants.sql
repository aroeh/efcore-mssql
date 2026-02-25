/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DECLARE @Restuarants TABLE
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
    [ZipCode] NVARCHAR(10) NOT NULL
)

INSERT INTO @Restuarants ([Id], [Name], [CuisineType], [Website], [Phone], [Street], [City], [State], [Country], [ZipCode])
VALUES
    ('62c00cc7f6f6456b94998a5c714a23a1', 'Monster Cookie', 'Cookie', 'https://www.monstercookies.com/', '+1 502.123.4567', '123 Cookie Avenue', 'Bossville', 'KY', 'United States', '12345-6789'),
    ('f12d33b559e4477d84682a5ac2ad8f65', 'Pizza Pals', 'Pizza', 'https://www.pizza.com/', '+1 502.456.9786', '123 Pizza Place', 'Cheese Town', 'KY', 'United States', '12345-6789'),
    ('60fdc0e6976141c79d2f8037baf2dcbf', 'Donut Worry, Be Happy', 'Pastry', 'https://www.awesomedonuts.com/', '+1 502.648.5773', '123 Donut Way', 'Tasty', 'KY', 'United States', '12345'),
    ('7a9a986a3a4a4e61b0a3df01570f3fd8', 'Taco Time', 'Mexican', 'https://www.timefortacos.com/', '+1 502.123.6736', '123 Queso Parkway', 'Tortilla', 'KY', 'United States', '12345')

MERGE INTO [dbo].[Restuarant] AS tgt
USING (SELECT [Id], [Name], [CuisineType], [Website], [Phone], [Street], [City], [State], [Country], [ZipCode] FROM @Restuarants)
    AS src([Id], [Name], [CuisineType], [Website], [Phone], [Street], [City], [State], [Country], [ZipCode])
    ON tgt.[Id] = src.[Id]
WHEN MATCHED
    THEN 
        UPDATE
        SET
            [Name] = src.[Name],
            [CuisineType] = src.[CuisineType],
            [Website] = src.[Website],
            [Phone] = src.[Phone],
            [Street] =  src.[Street],
            [City] =  src.[City],
            [State] = src.[State],
            [Country] =  src.[Country],
            [ZipCode] = src.[ZipCode]
WHEN NOT MATCHED
    THEN
        INSERT ([Id], [Name], [CuisineType], [Website], [Phone], [Street], [City], [State], [Country], [ZipCode])
        VALUES (src.[Id], src.[Name], src.[CuisineType], src.[Website], src.[Phone], src.[Street], src.[City], src.[State], src.[Country], src.[ZipCode]);