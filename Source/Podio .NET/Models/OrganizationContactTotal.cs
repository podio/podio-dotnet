using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class OrganizationContactTotal
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("org")]
        public OrganizationMicro Org { get; set; }

        [JsonProperty("spaces")]
        public List<SpaceContactTotal> Spaces { get; set; }
    }
}