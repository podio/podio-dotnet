﻿using PodioAPI.Models;
using System.Collections.Generic;

namespace PodioAPI.Services
{
    public class SearchService
    {
        private Podio _podio;
        public SearchService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Searches in all items, statuses, profiles, files, meetings and non-private tasks. The objects will be returned sorted descending by the time the object was created.
        /// <para>Podio API Reference: https://developers.podio.com/doc/search/search-globally-22488 </para>
        /// </summary>
        /// <param name="query">The text to search for.</param>
        /// <param name="limit"> The number of results to return; up to 20 results are returned in one call.</param>
        /// <param name="offset">The rank of the first search result to return (default=0).</param>
        /// <returns></returns>
        public List<SearchResult> SearchGlobally(string query, int? limit = null, int offset = 0)
        {
            string url = "/search/";
            dynamic requestData = new
            {
                query = query,
                limit = limit,
                offset = offset
            };
            return _podio.Post<List<SearchResult>>(url, requestData);
        }

        /// <summary>
        /// Searches in all items and tasks in the app.
        /// <para>Podio API Reference: https://developers.podio.com/doc/search/search-in-app-4234651 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<SearchResult> SearchInApp(int appId, string query, int? limit = null, int offset = 0)
        {
            string url = string.Format("/search/app/{0}/", appId);
            dynamic requestData = new
            {
                query = query,
                limit = limit,
                offset = offset
            };
            return _podio.Post<List<SearchResult>>(url, requestData);
        }

        /// <summary>
        /// Searches in all items, statuses and non-private tasks in the organization.
        /// <para>Podio API Reference: https://developers.podio.com/doc/search/search-in-organization-22487 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<SearchResult> SearchInOrganization(int orgId, string query, int? limit = null, int offset = 0)
        {
            string url = string.Format("/search/org/{0}/", orgId);
            dynamic requestData = new
            {
                query = query,
                limit = limit,
                offset = offset
            };
            return _podio.Post<List<SearchResult>>(url, requestData);
        }

        /// <summary>
        /// Searches in all items, statuses and non-private tasks in the space. The objects will be returned sorted descending by the time the object had any update.
        /// <para>Podio API Reference: https://developers.podio.com/doc/search/search-in-space-22479 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<SearchResult> SearchInSpace(int spaceId, string query, int? limit = null, int offset = 0)
        {
            string url = string.Format("/search/space/{0}/", spaceId);
            dynamic requestData = new
            {
                query = query,
                limit = limit,
                offset = offset
            };
            return _podio.Post<List<SearchResult>>(url, requestData);
        }
    }
}
