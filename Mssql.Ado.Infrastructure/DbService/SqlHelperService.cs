using Microsoft.Data.SqlClient;
using System.Data;

namespace Mssql.Ado.Infrastructure.DbService;

/// <summary>
/// Helper SQL class to make it easier to run SQL commands
/// </summary>
public class SqlHelperService : ISqlHelperService
{
    private readonly SqlService sqlService = new();

    /// <summary>
    /// Validates that the data table has data before trying to parse through and create objects
    /// </summary>
    /// <param name="table">Data table result from a previous command or query</param>
    /// <returns>Bool value indicating if the data table has data in it</returns>
    public bool DateTableHasData(DataTable table)
    {
        if (table is null || table.Rows is null || table.Rows.Count == 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Validates that the data row has data before trying to parse through and create objects
    /// </summary>
    /// <param name="row">Data row result from a previous command or query</param>
    /// <returns>Bool value indicating if the data row has data in it</returns>
    public bool DateRowHasData(DataRow row)
    {
        if (row is null || row.ItemArray is null || row.ItemArray.Length == 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Executes the specified stored procedure using the provided parameters
    /// Returns multiple items
    /// </summary>
    /// <param name="schema">schema associated to the stored procedure</param>
    /// <param name="storedProcName">name of the stored procedure to run</param>
    /// <param name="sqlParameters">parameters to pass to the stored procedure</param>
    /// <returns>DataTable</returns>
    public DataTable Select(string schema, string storedProcName, SqlParameter[] sqlParameters)
    {
        return sqlService.ExecuteReadRows(storedProcName, sqlParameters, schema);
    }

    /// <summary>
    /// Executes the specified stored procedure
    /// Returns multiple items
    /// </summary>
    /// <param name="schema">schema associated to the stored procedure</param>
    /// <param name="storedProcName">name of the stored procedure to run</param>
    /// <returns>DataTable</returns>
    public DataTable Select(string schema, string storedProcName)
    {
        return sqlService.ExecuteReadRows(storedProcName, schema);
    }

    /// <summary>
    /// Executes the supplied query using the provided parameters
    /// Returns multiple items
    /// </summary>
    /// <param name="query">Text query that will be run</param>
    /// <param name="sqlParameters">parameters that will be used in the query</param>
    /// <returns>DataTable</returns>
    public DataTable Select(string query, SqlParameter[] sqlParameters)
    {
        return sqlService.ExecuteTextCommandReadRows(query, sqlParameters);
    }

    /// <summary>
    /// Executes the sql query
    /// Returns multiple items
    /// </summary>
    /// <param name="query">Text query that will be run</param>
    /// <returns>DataTable</returns>
    public DataTable Select(string query)
    {
        return sqlService.ExecuteTextCommandReadRows(query);
    }

    /// <summary>
    /// Executes the specified stored procedure using the provided parameters
    /// Returns one result
    /// </summary>
    /// <param name="schema">schema associated to the stored procedure</param>
    /// <param name="storedProcName">name of the stored procedure to run</param>
    /// <param name="sqlParameters">parameters to pass to the stored procedure</param>
    /// <returns>DataRow</returns>
    public DataRow SelectOne(string schema, string storedProcName, SqlParameter[] sqlParameters)
    {
        DataTable table = sqlService.ExecuteReadRows(storedProcName, sqlParameters, schema);

        bool hasData = DateTableHasData(table);
        if (!hasData)
        {
            return null;
        }

        // The stored procedure being called should only be returning one result
        return table.Rows[0];
    }

    /// <summary>
    /// Executes the specified stored procedure
    /// Returns one result
    /// </summary>
    /// <param name="schema">schema associated to the stored procedure</param>
    /// <param name="storedProcName">name of the stored procedure to run</param>
    /// <returns>DataRow</returns>
    public DataRow SelectOne(string schema, string storedProcName)
    {
        DataTable table = sqlService.ExecuteReadRows(storedProcName, schema);

        bool hasData = DateTableHasData(table);
        if (!hasData)
        {
            return null;
        }

        // The stored procedure being called should only be returning one result
        return table.Rows[0];
    }

    /// <summary>
    /// Executes the supplied query using the provided parameters
    /// Returns one result
    /// </summary>
    /// <param name="query">Text query that will be run</param>
    /// <param name="sqlParameters">parameters that will be used in the query</param>
    /// <returns>DataRow</returns>
    public DataRow SelectOne(string query, SqlParameter[] sqlParameters)
    {
        DataTable table = sqlService.ExecuteTextCommandReadRows(query, sqlParameters);

        bool hasData = DateTableHasData(table);
        if (!hasData)
        {
            return null;
        }

        // The query being called should only be returning one result
        return table.Rows[0];
    }

    /// <summary>
    /// Executes the sql query
    /// Returns one result
    /// </summary>
    /// <param name="query">Text query that will be run</param>
    /// <returns>DataRow</returns>
    public DataRow SelectOne(string query)
    {
        DataTable table = sqlService.ExecuteTextCommandReadRows(query);

        bool hasData = DateTableHasData(table);
        if (!hasData)
        {
            return null;
        }

        // The query being called should only be returning one result
        return table.Rows[0];
    }

    /// <summary>
    /// Executes the specified insert stored procedure using the provided parameters
    /// Returns integer id of the newly insert record
    /// </summary>
    /// <param name="schema">schema associated to the stored procedure</param>
    /// <param name="storedProcName">name of the stored procedure to run</param>
    /// <param name="sqlParameters">parameters to pass to the stored procedure</param>
    /// <returns>int - the id of the new record</returns>
    public int InsertOne(string schema, string storedProcName, SqlParameter[] sqlParameters)
    {
        return (int)sqlService.ExecuteScalar(storedProcName, sqlParameters, schema);
    }

    /// <summary>
    /// Executes the specified insert stored procedure using the provided parameters
    /// Returns number of records inserted
    /// </summary>
    /// <param name="schema">schema associated to the stored procedure</param>
    /// <param name="storedProcName">name of the stored procedure to run</param>
    /// <param name="sqlParameters">parameters to pass to the stored procedure</param>
    /// <returns>int - number of rows affected</returns>
    public int Insert(string schema, string storedProcName, SqlParameter[] sqlParameters)
    {
        return sqlService.ExecuteNonQuery(storedProcName, sqlParameters, schema);
    }

    ///// <summary>
    ///// Executes the supplied insert statement using the provided parameters
    ///// Returns integer id of the newly insert record
    ///// </summary>
    ///// <param name="transaction"></param>
    ///// <param name="sqlParameters"></param>
    ///// <returns>int - the id of the new record</returns>
    //public int Insert(string transaction, SqlParameter[] sqlParameters)
    //{

    //}

    /// <summary>
    /// Executes the specified update stored procedure using the provided parameters
    /// Returns one result
    /// </summary>
    /// <param name="schema">schema associated to the stored procedure</param>
    /// <param name="storedProcName">name of the stored procedure to run</param>
    /// <param name="sqlParameters">parameters to pass to the stored procedure</param>
    /// <returns>int - number of rows affected</returns>
    public int Update(string schema, string storedProcName, SqlParameter[] sqlParameters)
    {
        return sqlService.ExecuteNonQuery(storedProcName, sqlParameters, schema);
    }

    ///// <summary>
    ///// Executes the supplied update statement using the provided parameters
    ///// Returns one result
    ///// </summary>
    ///// <param name="transaction"></param>
    ///// <param name="sqlParameters"></param>
    ///// <returns>bool</returns>
    //public Task<bool> Update(string transaction, SqlParameter[] sqlParameters)
    //{

    //}
}
