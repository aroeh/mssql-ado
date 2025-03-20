using Microsoft.Data.SqlClient;
using Mssql.Ado.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mssql.Ado.Infrastructure.DbService;

public interface ISqlRefHelpers
{
    /// <summary>
    /// Executes Stored Procedure designed to read multiple rows of data
    /// </summary>
    /// <typeparam name="T">Input Object Type - used to generically map return data</typeparam>
    /// <param name="storedProcedure">name of the stored procedure to be called</param>
    /// <param name="schema">default parameter for the schema</param>
    /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
    /// <returns>IEnumerable collection of generic type</returns>
    IEnumerable<T> ExecuteReadRows<T>(string storedProcedure, SqlParameter[] sqlParameters = null, string schema = DataAccessConstants.DefaultSchema);

    /// <summary>
    /// Executes Stored Procedure designed to return a single row of data
    /// </summary>
    /// <typeparam name="T">Input Object Type - used to generically map return data</typeparam>
    /// <param name="storedProcedure">name of the stored procedure to be called</param>
    /// <param name="schema">default parameter for the schema</param>
    /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
    /// <returns>Generic type</returns>
    T ExecuteReadSingleRow<T>(string storedProcedure, SqlParameter[] sqlParameters = null, string schema = DataAccessConstants.DefaultSchema);

    /// <summary>
    /// Executes a transactional stored procedure that expects a single id value to be returned
    /// Well suited for creates and inserts where an entity is normally returned
    /// </summary>
    /// <param name="storedProcedure">name of the stored procedure to be called</param>
    /// <param name="schema">default parameter for the schema</param>
    /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
    /// <returns></returns>
    int ExecuteScalar(string storedProcedure, SqlParameter[] sqlParameters = null, string schema = DataAccessConstants.DefaultSchema);

    /// <summary>
    /// Executes a transactional stored procedure that returns the number of affected rows
    /// Well suited for updates where we only need to know if the update was successful
    /// </summary>
    /// <param name="storedProcedure">name of the stored procedure to be called</param>
    /// <param name="schema">default parameter for the schema</param>
    /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
    /// <returns></returns>
    bool ExecuteTransaction(string storedProcedure, SqlParameter[] sqlParameters = null, string schema = DataAccessConstants.DefaultSchema);

    /// <summary>
    /// Uses a specified type and model input to create a SqlParameter that uses
    /// a DataTable as the input parameter.
    /// Best used when needing to pass in a parameterized table to a stored proc
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameterName"></param>
    /// <param name="tableTypeName"></param>
    /// <param name="valueCollection"></param>
    /// <returns></returns>
    SqlParameter CreateStructuredParameter<T>(string parameterName, string tableTypeName, IEnumerable<T> valueCollection);
}
