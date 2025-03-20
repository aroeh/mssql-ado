using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Mssql.Ado.Infrastructure.Constants;
using System.Data;
using System.Reflection;

namespace Mssql.Ado.Infrastructure.DbService;

/// <summary>
/// SQL Helper Class that uses reflection to generically map results into the specified objects
/// The data table column names must correspond to the Model property names in order for the mapping to work
/// So while a neat thing that can be done, it isn't very practical and can easily be broken without very strict monitoring
/// </summary>
public class SqlRefHelpers : ISqlRefHelpers
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public SqlRefHelpers(IConfiguration config)
    {
        configuration = config;

        //connectionString = configuration.GetConnectionString(DataAccessConstants.BudoAdminDb);
    }

    private SqlCommand InstantiateCommand(SqlConnection connection, string storedProcedure, SqlParameter[] sqlParameters = null)
    {
        var command = new SqlCommand(storedProcedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        if (sqlParameters != null && sqlParameters.Length > 0)
        {
            command.Parameters.AddRange(sqlParameters);
        }

        return command;
    }

    private List<Dictionary<string, object>> ExecuteSqlReader(string storedProcedure, SqlParameter[] sqlParameters = null)
    {
        var dataDictionaryCollection = new List<Dictionary<string, object>>();
        using (var connection = new SqlConnection(connectionString))
        {
            var command = InstantiateCommand(connection, storedProcedure, sqlParameters);

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var dataDict = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var propertyName = reader.GetName(i);
                        var propertyValue = reader.GetValue(i);

                        //Checking if the field has a DB Null.  Need to handle this to ensure we can set the value through reflection
                        if (reader.IsDBNull(i))
                        {
                            //if the value has a DBNull, the set a default value based on the field type
                            propertyValue = SetDefaultValue(reader.GetFieldType(i));
                        }

                        dataDict.Add(propertyName, propertyValue);
                    }
                    dataDictionaryCollection.Add(dataDict);
                }
            }

            connection.Close();
        }

        return dataDictionaryCollection;
    }

    private object SetDefaultValue(Type fieldType)
    {
        if (fieldType == typeof(string))
        {
            return default(string);
        }
        else if (fieldType == typeof(int))
        {
            return default(int);
        }
        else if (fieldType == typeof(DateTime))
        {
            return default(DateTime);
        }

        return null;
    }

    /// <summary>
    /// Instantiate an object using reflection
    /// Data from the Database is the source for property names and values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataDictionary"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    private T MapSqlResponse<T>(Dictionary<string, object> dataDictionary, PropertyInfo[] properties)
    {
        var objectData = Activator.CreateInstance(typeof(T));
        foreach (var prop in properties)
        {
            if (dataDictionary.ContainsKey(prop.Name))
            {
                prop.SetValue(objectData, dataDictionary.GetValueOrDefault(prop.Name));
            }
        }

        return (T)objectData;
    }

    /// <summary>
    /// Executes Stored Procedure designed to read multiple rows of data
    /// </summary>
    /// <typeparam name="T">Input Object Type - used to generically map return data</typeparam>
    /// <param name="storedProcedure">name of the stored procedure to be called</param>
    /// <param name="schema">default parameter for the schema</param>
    /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
    /// <returns>IEnumerable collection of generic type</returns>
    public IEnumerable<T> ExecuteReadRows<T>(string storedProcedure, SqlParameter[] sqlParameters = null, string schema = DataAccessConstants.DefaultSchema)
    {
        var dataDictionaryCollection = ExecuteSqlReader($"{schema}.{storedProcedure}", sqlParameters);

        if (dataDictionaryCollection == null || dataDictionaryCollection.Count == 0)
        {
            return null;
        }

        var response = new List<T>();
        var properties = typeof(T).GetProperties();
        for (var i = 0; i < dataDictionaryCollection.Count; i++)
        {
            response.Add(MapSqlResponse<T>(dataDictionaryCollection[i], properties));
        }

        return response;
    }

    /// <summary>
    /// Executes Stored Procedure designed to return a single row of data
    /// </summary>
    /// <typeparam name="T">Input Object Type - used to generically map return data</typeparam>
    /// <param name="storedProcedure">name of the stored procedure to be called</param>
    /// <param name="schema">default parameter for the schema</param>
    /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
    /// <returns>Generic type</returns>
    public T ExecuteReadSingleRow<T>(string storedProcedure, SqlParameter[] sqlParameters = null, string schema = DataAccessConstants.DefaultSchema)
    {
        var dataDictionaryCollection = ExecuteSqlReader($"{schema}.{storedProcedure}", sqlParameters);

        if (dataDictionaryCollection == null || dataDictionaryCollection.Count == 0)
        {
            return default(T);
        }

        var properties = typeof(T).GetProperties();
        var response = MapSqlResponse<T>(dataDictionaryCollection.First(), properties);

        return response;
    }

    /// <summary>
    /// Executes a transactional stored procedure that expects a single id value to be returned
    /// Well suited for creates and inserts where an entity is normally returned
    /// </summary>
    /// <param name="storedProcedure">name of the stored procedure to be called</param>
    /// <param name="schema">default parameter for the schema</param>
    /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
    /// <returns></returns>
    public int ExecuteScalar(string storedProcedure, SqlParameter[] sqlParameters = null, string schema = DataAccessConstants.DefaultSchema)
    {
        var entityId = 0;

        using (var connection = new SqlConnection(connectionString))
        {
            var command = InstantiateCommand(connection, $"{schema}.{storedProcedure}", sqlParameters);

            connection.Open();

            //ExecuteScalar returns a decimal - need to convert to avoid cast exceptions
            entityId = Convert.ToInt32(command.ExecuteScalar());

            connection.Close();
        }

        return entityId;
    }

    /// <summary>
    /// Executes a transactional stored procedure that returns the number of affected rows
    /// Well suited for updates where we only need to know if the update was successful
    /// </summary>
    /// <param name="storedProcedure">name of the stored procedure to be called</param>
    /// <param name="schema">default parameter for the schema</param>
    /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
    /// <returns></returns>
    public bool ExecuteTransaction(string storedProcedure, SqlParameter[] sqlParameters = null, string schema = DataAccessConstants.DefaultSchema)
    {
        var affectedRows = 0;

        using (var connection = new SqlConnection(connectionString))
        {
            var command = InstantiateCommand(connection, $"{schema}.{storedProcedure}", sqlParameters);

            connection.Open();

            affectedRows = command.ExecuteNonQuery();

            connection.Close();
        }

        return affectedRows > 0;
    }

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
    public SqlParameter CreateStructuredParameter<T>(string parameterName, string tableTypeName, IEnumerable<T> valueCollection)
    {
        //Create the DataTable using the type
        var parameterizedData = GetDataTable(tableTypeName);

        //Add Data Rows to the data table
        foreach (var value in valueCollection)
        {
            parameterizedData.Rows.Add(value);
        }

        return new SqlParameter
        {
            ParameterName = parameterName,
            SqlDbType = SqlDbType.Structured,
            TypeName = tableTypeName,
            Value = parameterizedData
        };
    }

    //TODO: see if there's a cleaner way of figuring out which table to create
    private DataTable GetDataTable(string tableTypeName)
    {
        var table = new DataTable();
        switch (tableTypeName)
        {
            case DataAccessConstants.IntCollection:
                table = DataAccessConstants.IntCollectionTable();
                break;
        }

        return table;
    }
}
