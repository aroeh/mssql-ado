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