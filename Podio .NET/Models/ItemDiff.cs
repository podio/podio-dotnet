using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ItemDiff
    {
        [JsonProperty("field_id")]
        public int? FieldId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("config")]
        public FieldConfig Config { get; set; }

        [JsonProperty("from")]
        public List<Dictionary<string, object>> From { get; set; }

        [JsonProperty("to")]
        public List<Dictionary<string, object>> To { get; set; }
    }
}