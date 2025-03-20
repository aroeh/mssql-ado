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