using System;
using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class FilterBase
    {
        /// <summary>
        ///     The sort order to use
        /// </summary>
        [JsonProperty("sort_by", NullValueHandling = NullValueHandling.Ignore)]
        public string SortBy { get; set; }

        /// <summary>
        ///     True to sort descending, false otherwise
        /// </summary>
        [JsonProperty("sort_desc", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SortDesc { get; set; }

        /// <summary>
        ///     The filters to apply
        /// </summary>
        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public Object Filters { get; set; }

        /// <summary>
        ///     The maximum number of items to return
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }

        /// <summary>
        ///     The offset into the returned items
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        /// <summary>
        ///   Space Id (To be used with Platform API)
        /// </summary>
        [JsonProperty("space_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SpaceId { get; set; }
    }
}