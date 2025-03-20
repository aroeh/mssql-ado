using Mssql.Ado.Core.Orchestrations;
using Mssql.Ado.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mssql.Ado.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class RestuarantController(ILogger<RestuarantController> log, IRestuarantOrchestration orchestration) : ControllerBase
{
    private readonly ILogger<RestuarantController> _logger = log;
    private readonly IRestuarantOrchestration _orchestration = orchestration;

    /// <summary>
    /// Get All Restuarants
    /// </summary>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpGet]
    public async Task<IResult> Get()
    {
        _logger.LogInformation("Get all restuarants request received");
        Restuarant[] restuarants = await _orchestration.GetAllRestuarants();

        if (restuarants is null || restuarants.Length == 0)
        {
            return TypedResults.NotFound();
        }

        _logger.LogInformation("Get all restuarants request complete...returning results");
        return TypedResults.Ok(restuarants);
    }

    /// <summary>
    /// Find Restuarants using matching criteria from query strings
    /// </summary>
    /// <param name="search">Object containing properties for search parameters</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost("find")]
    public async Task<IResult> Find([FromBody] SearchCriteria search)
    {
        _logger.LogInformation("Find restuarants request received");
        Restuarant[] restuarants = await _orchestration.FindRestuarants(search.Name, search.Cuisine);

        if (restuarants is null || restuarants.Length == 0)
        {
            return TypedResults.NotFound();
        }

        _logger.LogInformation("Find restuarants request complete...returning results");
        return TypedResults.Ok(restuarants);
    }

    /// <summary>
    /// Get a Restuarant using the provided id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpGet("{id}")]
    public async Task<IResult> Restuarant(string id)
    {
        _logger.LogInformation("Get restuarant request received");
        Restuarant restuarant = await _orchestration.GetRestuarant(id);

        if (restuarant is null || restuarant.Id.Equals(0))
        {
            return TypedResults.NotFound();
        }

        _logger.LogInformation("Get restuarant request complete...returning results");
        return TypedResults.Ok(restuarant);
    }

    /// <summary>
    /// Inserts a new restuarant
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost]
    public async Task<IResult> Post([FromBody] Restuarant restuarant)
    {
        _logger.LogInformation("Add restuarant request received");
        bool success = await _orchestration.InsertRestuarant(restuarant);

        _logger.LogInformation("Add restuarant request complete...returning results");
        return TypedResults.Ok(success);
    }

    /// <summary>
    /// Inserts many new restuarants
    /// </summary>
    /// <param name="restuarants">Restuarant array with many items to insert</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost("bulk")]
    public async Task<IResult> PostMany([FromBody] Restuarant[] restuarants)
    {
        _logger.LogInformation("Add restuarant request received");
        bool success = await _orchestration.InsertRestuarants(restuarants);

        _logger.LogInformation("Add restuarant request complete...returning results");
        return TypedResults.Ok(success);
    }

    /// <summary>
    /// Updates an existing restuarant
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPut]
    public async Task<IResult> Put([FromBody] Restuarant restuarant)
    {
        _logger.LogInformation("Update restuarant request received");
        bool success = await _orchestration.UpdateRestuarant(restuarant);

        _logger.LogInformation("Update restuarant request complete...returning results");
        return TypedResults.Ok(success);
    }
}
