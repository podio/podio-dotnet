using Newtonsoft.Json;
using System.Collections.Generic;

namespace PodioAPI.Utils
{
    public class PodioCollection<T>
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "filtered")]
        public int Filtered { get; set; }

        [JsonProperty(PropertyName = "items")]
        public IEnumerable<T> Items { get; set; }
    }
}
