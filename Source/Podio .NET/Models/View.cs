using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class View
    {
        [JsonProperty("view_id")]
        public string ViewId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("sort_by")]
        public string SortBy { get; set; }

        [JsonProperty("sort_desc")]
        public string SortDesc { get; set; }

        [JsonProperty("filters")]
        public JArray Filters { get; set; }

        [JsonProperty("fields")]
        public JObject Fields { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("layout")]
        public string Layout { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("filter_id")]
        public string FilterId { get; set; }

        [JsonProperty("items")]
        public int Items { get; set; }
    }
}