using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PodioAPI.Models
{
    public class SearchResult
    {
       
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("app")]
        public Application App { get; set; }

        [JsonProperty("org")]
        public OrganizationMicro Org { get; set; }

        [JsonProperty("space")]
        public SpaceMicro Space { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("highlight")]

        public JToken Highlight { get; set; }
    }

   
}