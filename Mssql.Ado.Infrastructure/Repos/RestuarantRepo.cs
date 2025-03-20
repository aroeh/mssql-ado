using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Mssql.Ado.Infrastructure.Constants;
using Mssql.Ado.Infrastructure.DbService;
using Mssql.Ado.Infrastructure.Mappers;
using Mssql.Ado.Shared.Models;
using System.Data;

namespace Mssql.Ado.Infrastructure.Repos;

public class RestuarantRepo(ILogger<RestuarantRepo> log, ISqlHelperService sql) : IRestuarantRepo
{
    private readonly ILogger<RestuarantRepo> _logger = log;
    private readonly ISqlHelperService _sqlHelper = sql;


    /// <summary>
    /// Returns a collection of all restuarants in the database
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records</returns>
    public Restuarant[] GetAllRestuarants()
    {
        DataTable table = _sqlHelper.Select(DataAccessConstants.DefaultSchema, DataAccessConstants.GetAllRestuarants);

        bool hasData = _sqlHelper.DateTableHasData(table);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return [];
        }

        return RestuarantMapper.MapRestuarantsAndLocation(table);
    }

    /// <summary>
    /// This is an example showing how to write SQL and execute it
    /// Using the stored proc command is the preferred method for security and overall performance
    /// But this method may offer more flexibility if changing the database is difficult
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records</returns>
    public Restuarant[] GetAllRestuarantsByStatement()
    {
        string query = @"SELECT
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
				        ON rl.RestuarantId = r.Id";

        DataTable table = _sqlHelper.Select(query);

        bool hasData = _sqlHelper.DateTableHasData(table);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return [];
        }

        return RestuarantMapper.MapRestuarantsAndLocation(table);
    }

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine.
    /// This could be enhanced to include more criteria like location
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records found matching criteria</returns>
    public Restuarant[] FindRestuarants(string name, string cuisine)
    {
        SqlParameter[] parameters =
        [
            new("@Name", name),
            new("@Cuisine", cuisine)
        ];
        DataTable table = _sqlHelper.Select(DataAccessConstants.DefaultSchema, DataAccessConstants.FindRestuarants, parameters);

        bool hasData = _sqlHelper.DateTableHasData(table);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return [];
        }

        return RestuarantMapper.MapRestuarantsAndLocation(table);
    }

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine using a coded query
    /// 
    /// This is an example showing how to write SQL that takes parameters and execute it
    /// Using the stored proc command is the preferred method for security and overall performance
    /// But this method may offer more flexibility if changing the database is difficult
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records found matching criteria</returns>
    public Restuarant[] FindRestuarantsByStatement(string name, string cuisine)
    {
        string query = @"SELECT
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
                    AND r.CuisineType LIKE '%' + @Cuisine +'%'";

        SqlParameter[] parameters =
        [
            new("@Name", name),
            new("@Cuisine", cuisine)
        ];
        DataTable table = _sqlHelper.Select(query, parameters);

        bool hasData = _sqlHelper.DateTableHasData(table);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return [];
        }

        return RestuarantMapper.MapRestuarantsAndLocation(table);
    }

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    public Restuarant GetRestuarant(string id)
    {
        SqlParameter[] parameters =
        [
            new("@Id", id)
        ];
        DataRow row = _sqlHelper.SelectOne(DataAccessConstants.DefaultSchema, DataAccessConstants.GetRestuarantById, parameters);

        bool hasData = _sqlHelper.DateRowHasData(row);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return new Restuarant();
        }

        return RestuarantMapper.MapRestuarantAndLocation(row);
    }

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    public Restuarant GetRestuarantByStatement(string id)
    {
        string query = @"SELECT
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
                WHERE r.Id = @Id";

        SqlParameter[] parameters =
        [
            new("@Id", id)
        ];
        DataRow row = _sqlHelper.SelectOne(query, parameters);

        bool hasData = _sqlHelper.DateRowHasData(row);

        // if there is no data then return an empty list
        if (!hasData)
        {
            return new Restuarant();
        }

        return RestuarantMapper.MapRestuarantAndLocation(row);
    }

    /// <summary>
    /// Inserts a new Restuarant Record with Location
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>id of the newly inserted restuarant</returns>
    public int InsertRestuarant(Restuarant restuarant)
    {
        _logger.LogInformation("Adding new restuarant");

        SqlParameter[] parameters =
        [
            new("@Name", restuarant.Name),
            new("@Cuisine", restuarant.CuisineType),
            new("@Website", restuarant.Website is null ? DBNull.Value : restuarant.Website.ToString()),
            new("@Phone", restuarant.Phone),
            new("@Street", restuarant.Address.Street),
            new("@City", restuarant.Address.City),
            new("@State", restuarant.Address.State),
            new("@ZipCode", restuarant.Address.ZipCode),
            new("@Country", restuarant.Address.Country)
        ];
        int newRestuarantId = _sqlHelper.InsertOne(DataAccessConstants.DefaultSchema, DataAccessConstants.InsertRestuarant, parameters);

        return newRestuarantId;
    }

    /// <summary>
    /// Inserts many new Restuarant Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Restuarant objects updated with the new id</returns>
    public Restuarant[] InsertRestuarants(Restuarant[] restuarants)
    {
        _logger.LogInformation("Adding new restuarants");

        DataTable table = RestuarantMapper.MapRestuarantsToDataTable(restuarants);

        SqlParameter[] parameters =
        [
            new()
            {
                ParameterName = "@NewRestuarants",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Structured,
                TypeName = DataAccessConstants.RestuarantType,
                Value = table
            }
        ];

        /*
         * For a simple bulk insert operation, the return would not be needed.
         * But, if needing the ids of the newly created objects, then there might need to be some creative
         * solutions to get those values.  The stored procedure in this case does that, but there might be a better way than what is demonstrated here
         */
        DataTable results = _sqlHelper.Select(DataAccessConstants.DefaultSchema, DataAccessConstants.GetAndInsertRestuarants, parameters);

        return RestuarantMapper.MapRestuarants(results);
    }

    /// <summary>
    /// Inserts many new Restuarant Address Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Number of address records inserted</returns>
    public int InsertRestuarantAddresses(Restuarant[] restuarants)
    {
        _logger.LogInformation("Adding new restuarants");

        DataTable table = RestuarantMapper.MapRestuarantAddressesToDataTable(restuarants);

        SqlParameter[] parameters =
        [
            new()
            {
                ParameterName = "@NewAddresses",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Structured,
                TypeName = DataAccessConstants.RestuarantLocationType,
                Value = table
            }
        ];

        /*
         * For a simple bulk insert operation, the return would not be needed.
         * But, if needing the ids of the newly created objects, then there might need to be some creative
         * solutions to get those values.  The stored procedure in this case does that, but there might be a better way than what is demonstrated here
         */
        return _sqlHelper.Insert(DataAccessConstants.DefaultSchema, DataAccessConstants.InsertRestuarantAddresses, parameters);
    }

    /// <summary>
    /// Updates and existing restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>int - number of rows affected</returns>
    public int UpdateRestuarant(Restuarant restuarant)
    {
        _logger.LogInformation("replacing restuarant document");
        SqlParameter[] parameters =
        [
            new("@Id", restuarant.Id),
            new("@Name", restuarant.Name),
            new("@Cuisine", restuarant.CuisineType),
            new("@Website", restuarant.Website is null ? DBNull.Value : restuarant.Website.ToString()),
            new("@Phone", restuarant.Phone),
            new("@Street", restuarant.Address.Street),
            new("@City", restuarant.Address.City),
            new("@State", restuarant.Address.State),
            new("@ZipCode", restuarant.Address.ZipCode),
            new("@Country", restuarant.Address.Country),
        ];
        return _sqlHelper.Update(DataAccessConstants.DefaultSchema, DataAccessConstants.UpdateRestuarant, parameters);
    }
}
