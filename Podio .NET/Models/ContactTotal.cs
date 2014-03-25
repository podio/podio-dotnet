using Newtonsoft.Json;
using System.Collections.Generic;

namespace PodioAPI.Models
{
    public class ContactTotal
    {
        [JsonProperty("count")]
        public int TotalCount { get; set; }

        [JsonProperty("orgs")]
        public List<OrganizationContactTotal> Orgs { get; set; }
    }
}
