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