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