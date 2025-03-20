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