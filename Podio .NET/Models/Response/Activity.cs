using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public class Activity
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("activity_type")]
        public string ActivityType { get; set; }

        [JsonProperty("data")]
        public Dictionary<string,object> Data { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }
    }
}
