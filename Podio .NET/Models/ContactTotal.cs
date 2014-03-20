using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
