using Mssql.Ado.Infrastructure.Repos;
using Mssql.Ado.Shared.Models;
using Microsoft.Extensions.Logging;

namespace Mssql.Ado.Core.Orchestrations;

public class RestuarantOrchestration(ILogger<RestuarantOrchestration> log, IRestuarantRepo repo) : IRestuarantOrchestration
{
    private readonly ILogger<RestuarantOrchestration> _logger = log;
    private readonly IRestuarantRepo _repo = repo;

    /// <summary>
    /// Retrieves all Restuarant from the database
    /// </summary>
    /// <returns>Array of Restuarant objects</returns>
    public Task<Restuarant[]> GetAllRestuarants()
    {
        _logger.LogInformation("Initiating get all restuarants");
        Restuarant[] restuarants = _repo.GetAllRestuarants();

        return Task.FromResult(restuarants);
    }

    /// <summary>
    /// Retrieves all Restuarant from the database using a query coded on the API
    /// </summary>
    /// <returns>Array of Restuarant objects</returns>
    public Task<Restuarant[]> GetAllRestuarantsUsingQuery()
    {
        _logger.LogInformation("Initiating get all restuarants using query");
        Restuarant[] restuarants = _repo.GetAllRestuarantsByStatement();

        return Task.FromResult(restuarants);
    }

    /// <summary>
    /// Retrieves all Restuarant from the database matching search criteria
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Array of Restuarant objects</returns>
    public Task<Restuarant[]> FindRestuarants(string name, string cuisine)
    {
        _logger.LogInformation("Initiating find restuarants");

        // call the data layer using a stored procedure
        Restuarant[] restuarants = _repo.FindRestuarants(name, cuisine);

        // call the data layer using a query
        //List<Restuarant> restuarants = _repo.FindRestuarantsByStatement(name, cuisine);

        return Task.FromResult(restuarants);
    }

    /// <summary>
    /// Retrieves all Restuarant from the database matching search criteria using a query coded on the API
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Array of Restuarant objects</returns>
    public Task<Restuarant[]> FindRestuarantsUsingQuery(string name, string cuisine)
    {
        _logger.LogInformation("Initiating find restuarants using query");
        Restuarant[] restuarants = _repo.FindRestuarantsByStatement(name, cuisine);

        return Task.FromResult(restuarants);
    }

    /// <summary>
    /// Retrieves a Restuarant from the database by id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Restuarant</returns>
    public Task<Restuarant> GetRestuarant(string id)
    {
        _logger.LogInformation("Initiating get restuarant by id");
        Restuarant restuarant = _repo.GetRestuarant(id);

        if (restuarant == null || restuarant.Id.Equals(0))
        {
            return Task.FromResult(new Restuarant());
        }

        return Task.FromResult(restuarant);
    }

    /// <summary>
    /// Inserts a new Restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>bool indicating the success of the operation</returns>
    public Task<bool> InsertRestuarant(Restuarant restuarant)
    {
        _logger.LogInformation("Adding new restuarant");
        int newRestuarantId = _repo.InsertRestuarant(restuarant);

        _logger.LogInformation("Checking insert operation result");
        // we could return the id as part of the result and there are many cases where you might need to do that
        // but depending on the scenario that could potentially expose data you might not want a client seeing
        return Task.FromResult(newRestuarantId > 0);
    }

    /// <summary>
    /// Inserts many new Restuarant records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>bool indicating the success of the operation</returns>
    public Task<bool> InsertRestuarants(Restuarant[] restuarants)
    {
        _logger.LogInformation("Adding new restuarants");

        /*
         * A problem has been artificially created here to demonstrate one possibility of needing to insert data into multiple tables
         * where input for one insert is dependent on the output of another insert operation
         * 
         * This method first inserts several restuarant records and then gets the new records back.
         * The ids from the output will be mapped so the next insert for the addresses can proceed
         */
        Restuarant[] newRestuarants = _repo.InsertRestuarants(restuarants);

        for (int i = 0; i < restuarants.Length; i++)
        {
            // compare the input values to the newly added values in the output.
            // If there's a match, then set the id into the input
            int id = 0;

            // if you can't use linq then you can match using another loop
            for (int j = 0; j < newRestuarants.Length; j++)
            {
                if (restuarants[i].Name.Equals(newRestuarants[j].Name, StringComparison.OrdinalIgnoreCase)
                    && restuarants[i].CuisineType.Equals(newRestuarants[j].CuisineType, StringComparison.OrdinalIgnoreCase)
                    && restuarants[i].Phone.Equals(newRestuarants[j].Phone, StringComparison.OrdinalIgnoreCase)
                    )
                {
                    // set the id and end this loop
                    id = newRestuarants[j].Id;
                    break;
                }
            }

            restuarants[i].Id = id;

            // Can also use linq to match if allowed and preferred
            //Restuarant match = newRestuarants.Where(n => restuarants[i].Name.Equals(n.Name, StringComparison.OrdinalIgnoreCase)
            //        && restuarants[i].CuisineType.Equals(n.CuisineType, StringComparison.OrdinalIgnoreCase)
            //        && restuarants[i].Phone.Equals(n.Phone, StringComparison.OrdinalIgnoreCase)
            //        ).FirstOrDefault() ?? new Restuarant();
            //id = match.Id;
        }

        int addressesAdded = _repo.InsertRestuarantAddresses(restuarants);

        _logger.LogInformation("Checking insert operation result");
        return Task.FromResult(newRestuarants.Length > 0 && addressesAdded > 0);
    }

    /// <summary>
    /// Updates a Restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>bool indicating the success of the operation</returns>
    public Task<bool> UpdateRestuarant(Restuarant restuarant)
    {
        _logger.LogInformation("Updating restuarant");
        int rowsAffected = _repo.UpdateRestuarant(restuarant);

        _logger.LogInformation("Checking update operation result");
        return Task.FromResult(rowsAffected > 0);
    }
}
