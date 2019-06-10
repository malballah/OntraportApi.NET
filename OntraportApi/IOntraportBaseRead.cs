﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmergenceGuardian.OntraportApi.Models;

namespace EmergenceGuardian.OntraportApi
{
    /// <summary>
    /// Provides common API endpoints for all objects with read methods.
    /// </summary>
    /// <typeparam name="T">The data object type deriving from ApiObject.</typeparam>
    public interface IOntraportBaseRead<T> 
        where T : ApiObject
    {
        /// <summary>
        /// Retrieves all the information for an existing object.
        /// </summary>
        /// <param name="id">The ID of the specific object.</param>
        /// <returns>The selected object.</returns>
        Task<T> SelectAsync(int id);

        /// <summary>
        /// Retrieves a collection of objects based on a set of parameters.
        /// </summary>
        /// <param name="searchOptions">The search options.</param>
        /// <param name="sortOptions">The sort options.</param>
        /// <param name="externs">If you have a relationship between your object and another object, you may want to include the data from a related field in your results. Each external field is listed in the format {object}//{field}.</param>
        /// <param name="listFields">A string array of the fields which should be returned in your results.</param>
        /// <returns>A list of objects matching the query.</returns>
        Task<IList<T>> SelectMultipleAsync(ApiSearchOptions searchOptions = null, ApiSortOptions sortOptions = null, IEnumerable<string> externs = null, IEnumerable<string> listFields = null);

        /// <summary>
        /// Retrieves the field meta data for the specified object type.
        /// </summary>
        /// <returns>A ResponseMetaData object.</returns>
        Task<ResponseMetadata> GetMetadataAsync();

        /// <summary>
        /// Retrieves information about a collection of objects, such as the number of objects that match the given criteria.
        /// </summary>
        /// <param name="searchOptions">The search options.</param>
        /// <returns>A ResponseCollectionInfo object.</returns>
        Task<ResponseCollectionInfo> GetCollectionInfoAsync(ApiSearchOptions searchOptions = null);
    }
}
