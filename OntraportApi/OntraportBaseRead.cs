﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using HanumanInstitute.OntraportApi.Models;

namespace HanumanInstitute.OntraportApi
{
    /// <summary>
    /// Provides common API endpoints for all objects with read methods.
    /// </summary>
    /// <typeparam name="T">The data object type deriving from ApiObject.</typeparam>
    public abstract class OntraportBaseRead<T> : IOntraportBaseRead<T>
        where T : ApiObject
    {
        protected OntraportHttpClient ApiRequest { get; }
        protected string EndpointSingular { get; }
        protected string EndpointPlural { get; }

        public OntraportBaseRead(OntraportHttpClient apiRequest, string endpointSingular, string endpointPlural)
        {
            ApiRequest = apiRequest;
            EndpointSingular = endpointSingular;
            EndpointPlural = endpointPlural;
        }

        /// <summary>
        /// Retrieves all the information for an existing object.
        /// </summary>
        /// <param name="id">The ID of the specific object.</param>
        /// <returns>The selected object.</returns>
        public async Task<T> SelectAsync(int id)
        {
            var query = new Dictionary<string, object?>
            {
                { "id", id }
            };

            var json = await ApiRequest.GetAsync<JObject>(
                EndpointSingular, query).ConfigureAwait(false);
            return await OnParseSelectAsync(json).ConfigureAwait(false);
        }

        /// <summary>
        /// When overriden in a derived class, allows custom parsing of SelectAsync response.
        /// </summary>
        /// <param name="json">The JSON data to parse.</param>
        /// <returns>A T or object derived from it.</returns>
        protected virtual async Task<T> OnParseSelectAsync(JObject json) =>
            await CreateApiObjectAsync(JsonData(json)).ConfigureAwait(false);

        /// <summary>
        /// Retrieves a collection of objects based on a set of parameters.
        /// </summary>
        /// <param name="searchOptions">The search options.</param>
        /// <returns>A list of objects matching the query.</returns>
        public Task<IList<T>> SelectAsync(ApiSearchOptions searchOptions) => SelectAsync(searchOptions, null, null, null);

        /// <summary>
        /// Retrieves a collection of objects based on a set of parameters.
        /// </summary>
        /// <param name="searchOptions">The search options.</param>
        /// <param name="sortOptions">The sort options.</param>
        /// <param name="externs">If you have a relationship between your object and another object, you may want to include the data from a related field in your results. Each external field is listed in the format {object}//{field}.</param>
        /// <param name="listFields">A string array of the fields which should be returned in your results.</param>
        /// <returns>A list of objects matching the query.</returns>
        public async Task<IList<T>> SelectAsync(
            ApiSearchOptions? searchOptions = null, ApiSortOptions? sortOptions = null,
            IEnumerable<string>? externs = null, IEnumerable<string>? listFields = null)
        {
            var query = new Dictionary<string, object?>()
                .AddSearchOptions(searchOptions)
                .AddSortOptions(sortOptions)
                .AddIfHasValue("externs", externs)
                .AddIfHasValue("listFields", externs);

            var json = await ApiRequest.GetAsync<JObject>(
                $"{EndpointPlural}", query).ConfigureAwait(false);
            return await OnParseSelectMultipleAsync(json).ConfigureAwait(false);
        }

        /// <summary>
        /// When overriden in a derived class, allows custom parsing of SelectMultipleAsync response.
        /// </summary>
        /// <param name="json">The JSON data to parse.</param>
        /// <returns>A List<T> or object derived from it.</returns>
        protected virtual async Task<IList<T>> OnParseSelectMultipleAsync(JObject json) =>
            await Task.WhenAll(
                JsonData(json).Children().Select(x => CreateApiObjectAsync(x))).ConfigureAwait(false);

        /// <summary>
        /// Retrieves the field meta data for the specified object type.
        /// </summary>
        /// <returns>A ResponseMetaData object.</returns>
        public async Task<ResponseMetadata> GetMetadataAsync()
        {
            var query = new Dictionary<string, object?>
            {
                { "format", "byId" }
            };

            var json = await ApiRequest.GetAsync<JObject>(
                $"{EndpointPlural}/meta", query).ConfigureAwait(false);
            return await OnParseGetMetadataAsync(json).ConfigureAwait(false);
        }

        /// <summary>
        /// When overriden in a derived class, allows custom parsing of SelectMetadataAsync response.
        /// </summary>
        /// <param name="json">The JSON data to parse.</param>
        /// <returns>A ResponseMetadata or object derived from it.</returns>
        /// <exception cref="NullReferenceException">Response data could not be parsed.</exception>
        protected virtual async Task<ResponseMetadata> OnParseGetMetadataAsync(JObject json) =>
            await Task.Run(() => JsonData(json).First?.First?.ToObject<ResponseMetadata>()).ConfigureAwait(false)
                ?? throw new NullReferenceException(Properties.Resources.ResponseDataNull);

        /// <summary>
        /// Retrieves information about a collection of objects, such as the number of objects that match the given criteria.
        /// </summary>
        /// <param name="searchOptions">The search options.</param>
        /// <returns>A ResponseCollectionInfo object.</returns>
        public async Task<ResponseCollectionInfo> GetCollectionInfoAsync(ApiSearchOptions? searchOptions = null)
        {
            var query = new Dictionary<string, object?>()
                .AddSearchOptions(searchOptions);

            var json = await ApiRequest.GetAsync<JObject>(
                $"{EndpointPlural}/getInfo", query).ConfigureAwait(false);
            return OnParseGetCollectionInfo(json);
        }

        /// <summary>
        /// When overriden in a derived class, allows custom parsing of SelectCollectionInfoAsync response.
        /// </summary>
        /// <param name="json">The JSON data to parse.</param>
        /// <returns>A ResponseCollectionInfo or object derived from it.</returns>
        /// <exception cref="NullReferenceException">Response data could not be parsed.</exception>
        protected virtual ResponseCollectionInfo OnParseGetCollectionInfo(JObject json) =>
            JsonData(json).ToObject<ResponseCollectionInfo>()!;

        /// <summary>
        /// Creates an instance of the ApiObject of type T and parses data into it.
        /// </summary>
        /// <param name="json">The JSON data to feed into the object..</param>
        /// <returns>A new ApiObject of type T.</returns>
        /// <exception cref="NullReferenceException">Response data could not be parsed.</exception>
        protected virtual async Task<T> CreateApiObjectAsync(JToken? json)
        {
            if (json != null)
            {
                var result = (T)Activator.CreateInstance(typeof(T), null)!;
                await Task.Run(() =>
                {
                    result.Data = json.ToObject<IDictionary<string, string?>>() ??
                        throw new NullReferenceException(Properties.Resources.ResponseDataNull);
                }).ConfigureAwait(false);
                return result;
            }
            return default!;
        }

        /// <summary>
        /// Returns the content of json["data"] and throws exceptions if it's null.
        /// </summary>
        /// <param name="json">The Json to retrieve data for.</param>
        /// <returns>The content of json["data"].</returns>
        /// <exception cref="ArgumentNullException">json was null, or json["data"] was null.</exception>
        protected static JToken JsonData(JObject? json)
        {
            json.CheckNotNull(nameof(json));
            var data = json!["data"];
            data.CheckNotNull("json[\"data\"]");
            return data!;
        }
    }
}
