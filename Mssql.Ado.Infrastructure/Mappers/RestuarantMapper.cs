using Mssql.Ado.Infrastructure.Constants;
using Mssql.Ado.Shared.Models;
using System.Data;

namespace Mssql.Ado.Infrastructure.Mappers;

internal static class RestuarantMapper
{
    /// <summary>
    /// Maps a DataTable into a collection of Restuarant objects including Address information
    /// </summary>
    /// <param name="table">DataTable from the resulting query or command</param>
    /// <returns>Array of Restuarants</returns>
    internal static Restuarant[] MapRestuarantsAndLocation(DataTable table) => [.. table.AsEnumerable().Select(row => MapRestuarantAndLocation(row))];

    /// <summary>
    /// Maps a DataTable into a collection of Restuarant objects without Address information
    /// </summary>
    /// <param name="table">DataTable from the resulting query or command</param>
    /// <returns>Array of Restuarants</returns>
    internal static Restuarant[] MapRestuarants(DataTable table) => [.. table.AsEnumerable().Select(row => MapRestuarant(row))];

    /// <summary>
    /// Maps a DataRow into a Restuarant object including Address information
    /// </summary>
    /// <param name="row">DataRow from the resulting query or command</param>
    /// <returns>Restuarant</returns>
    internal static Restuarant MapRestuarantAndLocation(DataRow row)
    {
        Restuarant restuarant = MapRestuarant(row);
        restuarant.Address = MapLocation(row);
        return restuarant;
    }

    /// <summary>
    /// Maps a DataRow into a Restuarant object without Address information
    /// </summary>
    /// <param name="row">DataRow from the resulting query or command</param>
    /// <returns>Restuarant</returns>
    internal static Restuarant MapRestuarant(DataRow row)
    {
        return new Restuarant
        {
            Id = row.Field<int>("Id"),
            Name = row.Field<string>("Name") ?? string.Empty,
            CuisineType = row.Field<string>("CuisineType") ?? string.Empty,
            Website = string.IsNullOrWhiteSpace(row.Field<string>("Website")) ? null : new Uri(row.Field<string>("Website")),
            Phone = row.Field<string>("Phone") ?? string.Empty
        };
    }

    /// <summary>
    /// Maps a DataRow into a Location object for Restuarant Address information
    /// </summary>
    /// <param name="row">DataRow from the resulting query or command</param>
    /// <returns>Location</returns>
    internal static Location MapLocation(DataRow row)
    {
        return new Location
        {
            Street = row.Field<string>("Street") ?? string.Empty,
            City = row.Field<string>("City") ?? string.Empty,
            State = row.Field<string>("State") ?? string.Empty,
            ZipCode = row.Field<string>("ZipCode") ?? string.Empty,
            Country = row.Field<string>("Country") ?? string.Empty
        };
    }

    internal static DataTable MapRestuarantsToDataTable(Restuarant[] restuarants)
    {
        DataTable table = DataAccessConstants.RestuarantTypeTable();
        for (int i = 0; i < restuarants.Length; i++)
        {
            DataRow row = table.NewRow();
            row["Name"] = restuarants[i].Name;
            row["CuisineType"] = restuarants[i].CuisineType;
            row["Website"] = restuarants[i].Website?.ToString();
            row["Phone"] = restuarants[i].Phone;
            table.Rows.Add(row);
        }

        return table;
    }

    internal static DataTable MapRestuarantAddressesToDataTable(Restuarant[] restuarants)
    {
        DataTable table = DataAccessConstants.RestuarantLocationTypeTable();
        for (int i = 0; i < restuarants.Length; i++)
        {
            DataRow row = table.NewRow();
            row["RestuarantId"] = restuarants[i].Id;
            row["Street"] = restuarants[i].Address.Street;
            row["City"] = restuarants[i].Address.City;
            row["State"] = restuarants[i].Address.State;
            row["Country"] = restuarants[i].Address.Country;
            row["ZipCode"] = restuarants[i].Address.ZipCode;
            table.Rows.Add(row);
        }

        return table;
    }
}
