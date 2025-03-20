INSERT INTO [dbo].[Restuarants] (Name, CuisineType, Website, Phone)
VALUES ('Monster Cookie', 'Cookie', 'https://www.monstercookies.com', '+1 502.123.4567')

INSERT INTO [dbo].[RestuarantLocation] (RestuarantId, Street, City, State, Country, ZipCode)
VALUES (SCOPE_IDENTITY(), '123 Cookie Avenue', 'Bossville', 'KY', 'United States', '12345-6789')


INSERT INTO [dbo].[Restuarants] (Name, CuisineType, Website, Phone)
VALUES ('Pizza Pals', 'Pizza', 'https://www.pizza.com', '+1 502.456.9786')

INSERT INTO [dbo].[RestuarantLocation] (RestuarantId, Street, City, State, Country, ZipCode)
VALUES (SCOPE_IDENTITY(), '123 Pizza Place', 'Cheese Town', 'KY', 'United States', '12345-6789')
