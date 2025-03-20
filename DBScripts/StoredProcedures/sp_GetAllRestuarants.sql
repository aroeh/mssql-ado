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