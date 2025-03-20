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