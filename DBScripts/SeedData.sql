DECLARE
    @FranchiseId INT,
    @RestuarantId INT,
    @MenuItemId INT


INSERT INTO [dbo].[Franchise] ([Name], [Description], [Established])
VALUES ('Mister Smalls Place', 'Home of our favorite Big Organge Buddy', '2013-01-04')
SET @FranchiseId = SCOPE_IDENTITY()

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Boss Steaks', 'Steakhouse', 'https://www.bosssteaks.com', '+1 502.876.2135')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Grill Place', 'Bossville', 'KY', 'United States', '12345-6789')

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Chill Shakes', 'Ice Cream', 'https://www.chillshakes.com', '+1 502.876.2136')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Shake Place', 'Bossville', 'KY', 'United States', '12345-6789')



INSERT INTO [dbo].[Franchise] ([Name], [Description], [Established])
VALUES ('Scampers', 'Belly Rubs All Around', '2013-03-12')
SET @FranchiseId = SCOPE_IDENTITY()

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Fella''s Kibbles', 'Americana', 'https://www.kibbles.com', '+1 502.462.2135')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Kibble Court', 'Nap Town', 'KY', 'United States', '12345-6789')

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Belly Rubs Original', 'Barbecue', 'https://www.bellyrubsbbq.com', '+1 502.462.2136')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Smoke Lane', 'Nap Town', 'KY', 'United States', '12345-6789')



INSERT INTO [dbo].[Franchise] ([Name], [Description], [Established])
VALUES ('Pepper Patty', 'The Fluffiest Boy', '2015-06-18')
SET @FranchiseId = SCOPE_IDENTITY()

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Pepper Patty', 'Burger', 'https://www.pepperpatty.com', '+1 502.315.2135')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Pepper Yell St', 'Fluffville', 'KY', 'United States', '12345-6789')

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Fluffy Burger', 'Burger', 'https://www.fluffyburger.com', '+1 502.315.2136')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Fluffy Lane', 'Fluffville', 'KY', 'United States', '12345-6789')



INSERT INTO [dbo].[Franchise] ([Name], [Description], [Established])
VALUES ('Cookie Crumbles', 'Oreo''s famous head bonks', '2019-07-04')
SET @FranchiseId = SCOPE_IDENTITY()

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Monster Cookie', 'Cookie', 'https://www.monstercookies.com', '+1 502.123.4567')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Cookie Avenue', 'Dough Town', 'KY', 'United States', '12345-6789')

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Double Stuffs', 'Cookie', 'https://www.dblstuffcookies.com', '+1 502.123.2136')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Double Park Place', 'Dough Town', 'KY', 'United States', '12345-6789')



INSERT INTO [dbo].[Franchise] ([Name], [Description], [Established])
VALUES ('Sammy Boops', 'Sammy Gonna Boop Ya', '2015-09-22')
SET @FranchiseId = SCOPE_IDENTITY()

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Darth Boops', 'Coffee', 'https://www.darthboops.com', '+1 502.654.4567')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Boop St', 'Sammy City', 'KY', 'United States', '12345-6789')

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Cake Boops', 'Cakes', 'https://www.cakeboops.com', '+1 502.654.2136')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Layer Lane', 'Sammy City', 'KY', 'United States', '12345-6789')



INSERT INTO [dbo].[Franchise] ([Name], [Description], [Established])
VALUES ('Diana''s Window Shop', 'Even the glass is yummy', '2018-05-17')
SET @FranchiseId = SCOPE_IDENTITY()

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Nana''s Brunch', 'Deli', 'https://www.nanasdeli.com', '+1 502.975.9786')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Deli Square', 'Glasston', 'KY', 'United States', '12345-6789')

INSERT INTO [dbo].[Restuarant] ([FranchiseId], [Name], [CuisineType], [Website], [Phone])
VALUES (@FranchiseId, 'Sundae Windows', 'Ice Cream', 'https://www.nanasicecream.com', '+1 502.456.9786')
SET @RestuarantId = SCOPE_IDENTITY()

INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
VALUES (@RestuarantId, '123 Clearglass Blvd', 'Glasston', 'KY', 'United States', '12345-6789')
