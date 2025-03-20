using System.Data;

namespace Mssql.Ado.Infrastructure.Constants;

internal static class DataAccessConstants
{
    internal const string DefaultSchema = "dbo";
    internal const string MsSqlConn = "MsSqlConn";

    #region Stored Procedure Names

    internal const string GetAllRestuarants = "sp_GetAllRestuarants";
    internal const string FindRestuarants = "sp_FindRestuarants";
    internal const string GetRestuarantById = "sp_GetRestuarantById";
    internal const string InsertRestuarant = "sp_InsertRestuarant";
    internal const string InsertRestuarants = "sp_InsertRestuarants";
    internal const string GetAndInsertRestuarants = "sp_GetAndInsertRestuarants";
    internal const string InsertRestuarantAddresses = "sp_InsertRestuarantAddresses";
    internal const string UpdateRestuarant = "sp_UpdateRestuarant";

    #endregion Stored Procedure Names

    #region Table Types

    internal const string IntCollection = "[dbo].[IntCollection]";
    internal const string RestuarantType = "[dbo].[RestuarantType]";
    internal const string RestuarantLocationType = "[dbo].[RestuarantLocationType]";

    #endregion Table Types

    #region Data Tables

    internal static DataTable IntCollectionTable()
    {
        DataTable table = new();
        table.Columns.Add("Id");

        return table;
    }

    internal static DataTable RestuarantTypeTable()
    {
        DataTable table = new();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("CuisineType", typeof(string));
        table.Columns.Add("Website", typeof(string));
        table.Columns.Add("Phone", typeof(string));

        return table;
    }

    internal static DataTable RestuarantLocationTypeTable()
    {
        DataTable table = new();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("RestuarantId", typeof(int));
        table.Columns.Add("Street", typeof(string));
        table.Columns.Add("City", typeof(string));
        table.Columns.Add("State", typeof(string));
        table.Columns.Add("Country", typeof(string));
        table.Columns.Add("ZipCode", typeof(string));

        return table;
    }

    #endregion Data Tables
}
