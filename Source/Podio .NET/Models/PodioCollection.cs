using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Utils
{
    public class PodioCollection<T>
    {
        /// <summary>
        ///     Total number of items in the app
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        ///     Count of items with the current filter applied
        /// </summary>
        [JsonProperty(PropertyName = "filtered")]
        public int Filtered { get; set; }

        [JsonProperty(PropertyName = "items")]
        public IEnumerable<T> Items { get; set; }
    }
}