using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class BulkDeleteRequest
    {
        /// <summary>
        ///     An explicit list of item ids to be deleted
        /// </summary>
        [JsonProperty(PropertyName = "item_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> ItemIds { get; set; }

        /// <summary>
        ///     The id of the saved view whose items are to be deleted. Ignored if "item_ids" is given.
        /// </summary>
        [JsonProperty(PropertyName = "view_id", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> ViewId { get; set; }

        /// <summary>
        ///     The filters to apply. Ignored if "item_ids" is given
        /// </summary>
        [JsonProperty(PropertyName = "filters", NullValueHandling = NullValueHandling.Ignore)]
        public Object Filters { get; set; }
    }
}