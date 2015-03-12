using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class ViewCreateUpdateRequest
    {
        /// <summary>
        ///     The name of the new view.
        /// </summary>
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     True if the view is private to the creator, otherwise it is public (defaults to false).
        /// </summary>
        [JsonProperty(PropertyName = "private", NullValueHandling = NullValueHandling.Ignore)]
        public bool Private { get; set; }

        /// <summary>
        ///     How the sorting should be, see the area for details.
        /// </summary>
        [JsonProperty(PropertyName = "sort_by", NullValueHandling = NullValueHandling.Ignore)]
        public string SortBy { get; set; }

        /// <summary>
        ///     True if sorting should be descending, false otherwise.
        /// </summary>
        [JsonProperty(PropertyName = "sort_desc", NullValueHandling = NullValueHandling.Ignore)]
        public bool SortDesc { get; set; }

        /// <summary>
        ///     The filters on the view.
        /// </summary>
        [JsonProperty(PropertyName = "filters", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Filters { get; set; }

        /// <summary>
        ///     The layout used, see the app area for details.
        /// </summary>
        [JsonProperty(PropertyName = "layout", NullValueHandling = NullValueHandling.Ignore)]
        public string Layout { get; set; }

        /// <summary>
        ///     Any specific settings for fields.
        /// </summary>
        [JsonProperty(PropertyName = "fields", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Fields { get; set; }
    }
}