USE [master]
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Samples')
 DROP DATABASE Samples;
GO


CREATE DATABASE [Samples]
GO



USE [Samples]
GO

/****** Object:  Table [dbo].[Restuarants]    Script Date: 8/17/2024 12:57:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.Restuarants', 'U') IS NOT NULL  
   DROP TABLE [dbo].[Restuarants]
GO

CREATE TABLE [dbo].[Restuarants]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CuisineType] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](200) NULL,
	[Phone] [nvarchar](20) NOT NULL,
	CONSTRAINT [PK_Restuarants] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [Samples]
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
	CONSTRAINT [FK_Restuarant_Id] FOREIGN KEY ([RestuarantId]) REFERENCES [dbo].[Restuarants]([Id])
) ON [PRIMARY]
GO


INSERT INTO [dbo].[Restuarants] (Name, CuisineType, Website, Phone)
VALUES ('Monster Cookie', 'Cookie', 'https://www.monstercookies.com', '+1 502.123.4567')

INSERT INTO [dbo].[RestuarantLocation] (RestuarantId, Street, City, State, Country, ZipCode)
VALUES (SCOPE_IDENTITY(), '123 Cookie Avenue', 'Bossville', 'KY', 'United States', '12345-6789')


INSERT INTO [dbo].[Restuarants] (Name, CuisineType, Website, Phone)
VALUES ('Pizza Pals', 'Pizza', 'https://www.pizza.com', '+1 502.456.9786')

INSERT INTO [dbo].[RestuarantLocation] (RestuarantId, Street, City, State, Country, ZipCode)
VALUES (SCOPE_IDENTITY(), '123 Pizza Place', 'Cheese Town', 'KY', 'United States', '12345-6789')
GO


USE [Samples]
GO

IF TYPE_ID(N'dbo.RestuarantType') IS NOT NULL
   DROP TYPE [dbo].[RestuarantType]
GO

CREATE TYPE [dbo].[RestuarantType] AS TABLE
(
    [Id] INT NULL,
    [Name] VARCHAR(50) NULL,
    [CuisineType] VARCHAR(50) NULL,
    [Website] NVARCHAR(200) NULL,
    [Phone] NVARCHAR(20) NULL
)
GO


USE [Samples]
GO

IF TYPE_ID(N'dbo.RestuarantLocationType') IS NOT NULL
   DROP TYPE [dbo].[RestuarantLocationType]
GO

CREATE TYPE [dbo].[RestuarantLocationType] AS TABLE
(
    [Id] INT NULL,
    [RestuarantId] INT NULL,
    [Street] NVARCHAR(200) NULL,
    [City] NVARCHAR(100) NULL,
    [State] CHAR(2) NULL,
    [Country] NVARCHAR(100) NULL,
    [ZipCode] NVARCHAR(10) NULL
)
GO


USE [Samples]
GO

IF OBJECT_ID('dbo.sp_FindRestuarants', 'P') IS NOT NULL  
   DROP PROCEDURE [dbo].[sp_FindRestuarants];  
GO

CREATE PROCEDURE [dbo].[sp_FindRestuarants]
(
    @Name       NVARCHAR(50),
    @Cuisine    NVARCHAR(50)
)
AS
	BEGIN
		SELECT
			r.Id,
            r.Name,
            r.CuisineType,
            r.Website,
            r.Phone,
            rl.Street,
            rl.City,
            rl.[State],
            rl.ZipCode,
            rl.Country
		FROM [dbo].[Restuarants] r
			INNER JOIN [dbo].[RestuarantLocation] rl
				ON rl.RestuarantId = r.Id
        WHERE r.Name LIKE '%' + @Name +'%'
            AND r.CuisineType LIKE '%' + @Cuisine +'%'
	END
GO


USE [Samples]
GO

IF OBJECT_ID('dbo.sp_GetAllRestuarants', 'P') IS NOT NULL  
   DROP PROCEDURE [dbo].[sp_GetAllRestuarants];  
GO

CREATE PROCEDURE [dbo].[sp_GetAllRestuarants]
AS
	BEGIN
		SELECT
			r.Id,
            r.Name,
            r.CuisineType,
            r.Website,
            r.Phone,
            rl.Street,
            rl.City,
            rl.[State],
            rl.ZipCode,
            rl.Country
		FROM [dbo].[Restuarants] r
			INNER JOIN [dbo].[RestuarantLocation] rl
				ON rl.RestuarantId = r.Id
	END
GO


USE [Samples]
GO

IF OBJECT_ID('dbo.sp_GetRestuarantById', 'P') IS NOT NULL  
   DROP PROCEDURE [dbo].[sp_GetRestuarantById];  
GO

CREATE PROCEDURE [dbo].[sp_GetRestuarantById]
(
    @Id INT
)
AS
	BEGIN
		SELECT
			r.Id,
            r.Name,
            r.CuisineType,
            r.Website,
            r.Phone,
            rl.Street,
            rl.City,
            rl.[State],
            rl.ZipCode,
            rl.Country
		FROM [dbo].[Restuarants] r
			INNER JOIN [dbo].[RestuarantLocation] rl
				ON rl.RestuarantId = r.Id
        WHERE r.Id = @Id
	END
GO


USE [Samples]
GO

IF OBJECT_ID('dbo.sp_InsertRestuarant', 'P') IS NOT NULL  
   DROP PROCEDURE [dbo].[sp_InsertRestuarant];  
GO

CREATE PROCEDURE [dbo].[sp_InsertRestuarant]
(
    @Name       NVARCHAR(50),
    @Cuisine    NVARCHAR(50),
    @Website    NVARCHAR(200),
    @Phone      NVARCHAR(20),
    @Street     NVARCHAR(200),
    @City       NVARCHAR(100),
    @State      CHAR(2),
    @ZipCode    NVARCHAR(10),
    @Country    NVARCHAR(100)
)
AS
	BEGIN
		DECLARE @newRestuarantId INT
        
        -- insert a new record to the Restuarants table
        INSERT INTO [dbo].[Restuarants] (Name, CuisineType, Website, Phone)
        VALUES (@Name, @Cuisine, @Website, @Phone)

        SET @newRestuarantId = SCOPE_IDENTITY()

        -- insert a new record to the location table
        INSERT INTO [dbo].[RestuarantLocation] (RestuarantId, Street, City, State, Country, ZipCode)
        VALUES (SCOPE_IDENTITY(), @Street, @City, @State, @Country, @ZipCode)

        -- return the new resturarant id
        SELECT @newRestuarantId
	END
GO


USE [Samples]
GO

IF OBJECT_ID('dbo.sp_InsertRestuarants', 'P') IS NOT NULL  
   DROP PROCEDURE [dbo].[sp_InsertRestuarants];  
GO

CREATE PROCEDURE [dbo].[sp_InsertRestuarants]
(
    @NewRestuarants AS [dbo].[RestuarantType] READONLY
)
AS
	BEGIN
		DECLARE @output TABLE (Id INT)
        
        -- insert a new record to the Restuarants table
        INSERT INTO [dbo].[Restuarants] ([Name], [CuisineType], [Website], [Phone])
        OUTPUT INSERTED.[Id] INTO @output
        SELECT [Name], [CuisineType], [Website], [Phone] FROM @NewRestuarants

        -- select the ids from the output table as the return result set
        SELECT [Id] FROM @output
	END
GO


USE [Samples]
GO

IF OBJECT_ID('dbo.sp_InsertRestuarantAddresses', 'P') IS NOT NULL  
   DROP PROCEDURE [dbo].[sp_InsertRestuarantAddresses];  
GO

CREATE PROCEDURE [dbo].[sp_InsertRestuarantAddresses]
(
    @NewAddresses AS [dbo].[RestuarantLocationType] READONLY
)
AS
	BEGIN
		DECLARE @output TABLE (Id INT)
        
        -- insert a new record to the Restuarants Location table
        INSERT INTO [dbo].[RestuarantLocation] ([RestuarantId], [Street], [City], [State], [Country], [ZipCode])
        SELECT [RestuarantId], [Street], [City], [State], [Country], [ZipCode] FROM @NewAddresses

        -- select the ids from the output table as the return result set
        SELECT @@ROWCOUNT
	END
GO


USE [Samples]
GO

IF OBJECT_ID('dbo.sp_UpdateRestuarant', 'P') IS NOT NULL  
   DROP PROCEDURE [dbo].[sp_UpdateRestuarant];  
GO

CREATE PROCEDURE [dbo].[sp_UpdateRestuarant]
(
    @Id         INT,
    @Name       NVARCHAR(50),
    @Cuisine    NVARCHAR(50),
    @Website    NVARCHAR(200),
    @Phone      NVARCHAR(20),
    @Street     NVARCHAR(200),
    @City       NVARCHAR(100),
    @State      CHAR(2),
    @ZipCode    NVARCHAR(10),
    @Country    NVARCHAR(100)
)
AS
	BEGIN
		-- Update a Restuarant record
        UPDATE [dbo].[Restuarants]
        SET
            [Name] = @Name,
            [CuisineType] = @Cuisine,
            [Website] = @Website,
            [Phone] = @Phone
        WHERE Id = @Id

        -- Update a location record
        UPDATE [dbo].[RestuarantLocation]
        SET
            [Street] = @Street,
            [City] = @City,
            [State] = @State,
            [Country] = @Country,
            [ZipCode] = @ZipCode
        WHERE RestuarantId = @Id

        -- return the number of impacted rows
        SELECT @@ROWCOUNT
	END
GO