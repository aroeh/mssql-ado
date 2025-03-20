using Microsoft.Data.SqlClient;
using System.Data;

namespace Mssql.Ado.Infrastructure.DbService;

public interface ISqlHelperService
{
    /// <summary>
    /// Validates that the data table has data before trying to parse through and create objects
    /// </summary>
    /// <param name="table">Data table result from a previous command or query</param>
    /// <returns>Bool value indicating if the data table has data in it</returns>
    bool DateTableHasData(DataTable table);

    /// <summary>
    /// Validates that the data row has data before trying to parse through and create objects
    /// </summary>
    /// <param name="row">Data row result from a previous command or query</param>
    /// <returns>Bool value indicating if the data row has data in it</returns>
    bool DateRowHasData(DataRow row);

    /// <summary>
    /// Executes the specified stored procedure using the provided parameters
    /// Returns multiple items
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="storedProcName"></param>
    /// <param name="sqlParameters"></param>
    /// <returns>DataTable</returns>
    DataTable Select(string schema, string storedProcName, SqlParameter[] sqlParameters);

    /// <summary>
    /// Executes the specified stored procedure
    /// Returns multiple items
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="storedProcName"></param>
    /// <returns>DataTable</returns>
    DataTable Select(string schema, string storedProcName);

    /// <summary>
    /// Executes the supplied query using the provided parameters
    /// Returns multiple items
    /// </summary>
    /// <param name="query"></param>
    /// <param name="sqlParameters"></param>
    /// <returns>DataTable</returns>
    DataTable Select(string query, SqlParameter[] sqlParameters);

    /// <summary>
    /// Executes the sql query
    /// Returns multiple items
    /// </summary>
    /// <param name="query"></param>
    /// <returns>DataTable</returns>
    DataTable Select(string query);

    /// <summary>
    /// Executes the specified stored procedure using the provided parameters
    /// Returns one result
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="storedProcName"></param>
    /// <param name="sqlParameters"></param>
    /// <returns>DataRow</returns>
    DataRow SelectOne(string schema, string storedProcName, SqlParameter[] sqlParameters);

    /// <summary>
    /// Executes the specified stored procedure
    /// Returns one result
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="storedProcName"></param>
    /// <returns>DataRow</returns>
    DataRow SelectOne(string schema, string storedProcName);

    /// <summary>
    /// Executes the supplied query using the provided parameters
    /// Returns one result
    /// </summary>
    /// <param name="query"></param>
    /// <param name="sqlParameters"></param>
    /// <returns>DataRow</returns>
    DataRow SelectOne(string query, SqlParameter[] sqlParameters);

    /// <summary>
    /// Executes the sql query
    /// Returns one result
    /// </summary>
    /// <param name="query"></param>
    /// <returns>DataRow</returns>
    DataRow SelectOne(string query);

    /// <summary>
    /// Executes the specified insert stored procedure using the provided parameters
    /// Returns one result
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="storedProcName"></param>
    /// <param name="sqlParameters"></param>
    /// <returns>int - the id of the new record</returns>
    int InsertOne(string schema, string storedProcName, SqlParameter[] sqlParameters);

    /// <summary>
    /// Executes the specified insert stored procedure using the provided parameters
    /// Returns number of records inserted
    /// </summary>
    /// <param name="schema">schema associated to the stored procedure</param>
    /// <param name="storedProcName">name of the stored procedure to run</param>
    /// <param name="sqlParameters">parameters to pass to the stored procedure</param>
    /// <returns>int - number of rows affected</returns>
    int Insert(string schema, string storedProcName, SqlParameter[] sqlParameters);

    ///// <summary>
    ///// Executes the supplied insert statement using the provided parameters
    ///// Returns one result
    ///// </summary>
    ///// <param name="transaction"></param>
    ///// <param name="sqlParameters"></param>
    ///// <returns>int - the id of the new record</returns>
    //Task<int> Insert(string transaction, SqlParameter[] sqlParameters);

    /// <summary>
    /// Executes the specified update stored procedure using the provided parameters
    /// Returns one result
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="storedProcName"></param>
    /// <param name="sqlParameters"></param>
    /// <returns>int - number of rows affected</returns>
    int Update(string schema, string storedProcName, SqlParameter[] sqlParameters);

    ///// <summary>
    ///// Executes the supplied update statement using the provided parameters
    ///// Returns one result
    ///// </summary>
    ///// <param name="transaction"></param>
    ///// <param name="sqlParameters"></param>
    ///// <returns>bool</returns>
    //Task<bool> Update(string transaction, SqlParameter[] sqlParameters);
}
