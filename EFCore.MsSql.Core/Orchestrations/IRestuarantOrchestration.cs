﻿using EFCore.MsSql.Shared.Models;

namespace EFCore.MsSql.Core.Orchestrations;

public interface IRestuarantOrchestration
{
    /// <summary>
    /// Retrieves all Restuarant from the database
    /// </summary>
    /// <returns>Array of Restuarant objects</returns>
    Task<Restuarant[]> GetAllRestuarants();

    /// <summary>
    /// Retrieves all Restuarant from the database matching search criteria
    /// </summary>
    /// <param name="name">Search Parameter on the Restuarant Name</param>
    /// <param name="cuisine">Search Parameter on the Restuarant CuisineType</param>
    /// <returns>Array of Restuarant objects</returns>
    Task<Restuarant[]> FindRestuarants(string name, string cuisine);

    /// <summary>
    /// Retrieves a Restuarant from the database by id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>bool indicating the success of the operation</returns>
    Task<Restuarant> GetRestuarant(string id);

    /// <summary>
    /// Inserts a new Restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>bool indicating the success of the operation</returns>
    Task<bool> InsertRestuarant(Restuarant restuarant);

    /// <summary>
    /// Inserts many new Restuarant records
    /// </summary>
    /// <param name="restuarants">Array of new restuarant objects to add</param>
    /// <returns>bool indicating the success of the operation</returns>
    Task<bool> InsertRestuarants(Restuarant[] restuarants);

    /// <summary>
    /// Updates a Restuarant record
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>bool indicating the success of the operation</returns>
    Task<bool> UpdateRestuarant(Restuarant restuarant);
}
