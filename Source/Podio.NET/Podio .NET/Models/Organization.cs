using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Organization
    {
        [JsonProperty("org_id")]
        public int OrgId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("logo")]
        public int? Logo { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("url_label")]
        public string UrlLabel { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("sales_agent_id")]
        public int SalesAgentId { get; set; }

        [JsonProperty("created_on")]
        public string CreatedOn { get; set; }

        [JsonProperty("domains")]
        public string[] Domains { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("rank")]
        public int? Rank { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("spaces")]
        public List<Space> Spaces { get; set; }

        [JsonProperty("grants_count")]
        public int? GrantsCount { get; set; }

        [JsonProperty("segment")]
        public string Segment { get; set; }

        [JsonProperty("segment_size")]
        public int? SegmentSize { get; set; }
    }
}