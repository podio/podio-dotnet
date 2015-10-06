using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class FlowAttribute
    {
        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("attribute_id")]
        public string AttributeId { get; set; }

        [JsonProperty("label")]
        public string Label { get; private set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("substitutions")]
        public Dictionary<string, string> Substitutions { get; private set; }
    }
}