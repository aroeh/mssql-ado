﻿using Mssql.Ado.Shared.Models;

namespace Mssql.Ado.Infrastructure.Repos;

public interface IRestuarantRepo
{
    /// <summary>
    /// Returns a collection of all restuarants in the database
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records</returns>
    Restuarant[] GetAllRestuarants();

    /// <summary>
    /// This is an example showing how to write SQL and execute it
    /// Using the stored proc command is the preferred method for security and overall performance
    /// But this method may offer more flexibility if changing the database is difficult
    /// </summary>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records</returns>
    Restuarant[] GetAllRestuarantsByStatement();

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine.
    /// This could be enhanced to include more criteria like location
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records found matching criteria</returns>
    Restuarant[] FindRestuarants(string name, string cuisine);

    /// <summary>
    /// Simple method for finding restuarants by name and type of cuisine using a coded query
    /// This could be enhanced to include more criteria like location
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Collection of available restuarant records.  Returns empty array if there are no records found matching criteria</returns>
    Restuarant[] FindRestuarantsByStatement(string name, string cuisine);

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    Restuarant GetRestuarant(string id);

    /// <summary>
    /// Retrieves a restuarant record based on the matching id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Restuarant record if found.  Returns new Restuarant if not found</returns>
    Restuarant GetRestuarantByStatement(string id);

    /// <summary>
    /// Inserts a new Restuarant Record with Location
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>id of the newly inserted restuarant</returns>
    int InsertRestuarant(Restuarant restuarant);

    /// <summary>
    /// Inserts many new Restuarant Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Restuarant objects updated with the new id</returns>
    Restuarant[] InsertRestuarants(Restuarant[] restuarants);

    /// <summary>
    /// Inserts many new Restuarant Address Records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>Number of address records inserted</returns>
    int InsertRestuarantAddresses(Restuarant[] restuarants);

    /// <summary>
    /// Updates and existing restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>int - number of rows affected</returns>
    int UpdateRestuarant(Restuarant restuarant);
}
