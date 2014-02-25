using Newtonsoft.Json;
using System.Collections.Generic;

namespace PodioAPI.Models.Response
{
    public class FieldConfig
    {
        [JsonProperty("default_value")]
        public object DefaultValue { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("settings")]
        public Dictionary<string, object> Settings { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("mapping")]
        public object Mapping { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("delta")]
        public int Delta { get; set; }

        [JsonProperty("hidden")]
        public bool? Hidden { get; set; }
    }
}
