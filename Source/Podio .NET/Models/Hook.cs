using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Hook
    {
        [JsonProperty("hook_id")]
        public int HookId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_on")]
        public DateTime? Createdon { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }
    }
}