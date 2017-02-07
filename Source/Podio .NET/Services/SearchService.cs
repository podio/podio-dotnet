using System.Collections.Generic;
using PodioAPI.Models;

namespace PodioAPI.Services
{
    public class SearchService
    {
        private readonly Podio _podio;

        public SearchService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Searches in all items, statuses, profiles, files, meetings and non-private tasks. The objects will be returned
        ///     sorted descending by the time the object was created.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/search/search-globally-22488 </para>
        /// </summary>
        /// <param name="query">The text to search for.</param>
        /// <param name="limit"> The number of results to return; up to 20 results are returned in one call.</param>
        /// <param name="offset">The rank of the first search result to return (default=0).</param>
        /// <param name="refType">
        ///     The type of objects to search for. Can be one of "item", "task", "conversation", "app", "status",
        ///     "file" and  "profile"
        /// </param>
        /// <returns></returns>
        public List<SearchResult> SearchGlobally(string query, int? limit = null, int offset = 0, string refType = null)
        {
            string url = "/search/";
            dynamic requestData = new
            {
                query = query,
                limit = limit,
                offset = offset,
                ref_type = refType
            };
            return _podio.Post<List<SearchResult>>(url, requestData);
        }

        /// <summary>
        ///     Searches in all items and tasks in the app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/search/search-in-app-4234651 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="refType">
        ///     The type of objects to search for. Can be one of "item", "task", "conversation", "app", "status",
        ///     "file" and  "profile"
        /// </param>
        /// <returns></returns>
        public List<SearchResult> SearchInApp(int appId, string query, int? limit = null, int offset = 0,
            string refType = null)
        {
            string url = string.Format("/search/app/{0}/", appId);
            dynamic requestData = new
            {
                query = query,
                limit = limit,
                offset = offset,
                ref_type = refType
            };
            return _podio.Post<List<SearchResult>>(url, requestData);
        }

        /// <summary>
        ///     Searches in all items and tasks in the app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/search/search-in-application-v2-155196220 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="refType">
        ///     The type of objects to search for. Can be one of "item", "task", "conversation", "app", "status",
        ///     "file" and  "profile"
        /// </param>
        /// <param name="counts"></param>
        /// <param name="highlights"></param>
        /// <param name="searchFields">Comma seperated</param>
        /// <returns></returns>
        public SearchResultV2 SearchInAppV2(int appId, string query, int? limit = null, int offset = 0,
            string refType = null, bool counts = false, bool highlights = false, string searchFields = null)
        {
            string url = string.Format("/search/app/{0}/v2/", appId);
            var requestData = new Dictionary<string, string>()
            {
                {"query", query},
                {"limit", limit.HasValue ? limit.ToString() : "20" },
                {"offset", offset.ToString()},
                {"ref_type", refType},
                {"counts", counts.ToString() },
                {"highlights", highlights.ToString() },
                {"search_fields", searchFields }
            };
         
            return  _podio.Get<SearchResultV2>(url, requestData);
        }

        /// <summary>
        ///     Searches in all items, statuses and non-private tasks in the organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/search/search-in-organization-22487 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="refType">
        ///     The type of objects to search for. Can be one of "item", "task", "conversation", "app", "status",
        ///     "file" and  "profile"
        /// </param>
        /// <returns></returns>
        public List<SearchResult> SearchInOrganization(int orgId, string query, int? limit = null, int offset = 0,
            string refType = null)
        {
            string url = string.Format("/search/org/{0}/", orgId);
            dynamic requestData = new
            {
                query = query,
                limit = limit,
                offset = offset,
                ref_type = refType
            };
            return _podio.Post<List<SearchResult>>(url, requestData);
        }

        /// <summary>
        ///     Searches in all items, statuses and non-private tasks in the space. The objects will be returned sorted descending
        ///     by the time the object had any update.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/search/search-in-space-22479 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="query"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="refType">
        ///     The type of objects to search for. Can be one of "item", "task", "conversation", "app", "status",
        ///     "file" and  "profile"
        /// </param>
        /// <returns></returns>
        public List<SearchResult> SearchInSpace(int spaceId, string query, int? limit = null, int offset = 0,
            string refType = null)
        {
            string url = string.Format("/search/space/{0}/", spaceId);
            dynamic requestData = new
            {
                query = query,
                limit = limit,
                offset = offset,
                ref_type = refType
            };
            return _podio.Post<List<SearchResult>>(url, requestData);
        }
    }
}