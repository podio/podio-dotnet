using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class BulkDeletionStatus
    {
        /// <summary>
        ///     List of item ids that have been deleted at this point
        /// </summary>
        [JsonProperty(PropertyName = "deleted")]
        public List<int> Deleted { get; set; }

        /// <summary>
        ///     List of remaining item ids to be deleted
        /// </summary>
        [JsonProperty(PropertyName = "pending")]
        public List<int> Pending { get; set; }
    }
}