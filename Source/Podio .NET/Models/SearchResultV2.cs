using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PodioAPI.Models
{
    public class SearchResultV2
    {

        [JsonProperty("results")]
        public IEnumerable<SearchResult> Results { get; set; }

        [JsonProperty("counts")]
        public JToken Counts { get; set; }
    }
}
