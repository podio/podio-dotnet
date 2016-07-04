using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class RowPreview
    {
        [JsonProperty("fields")]
        public List<PreviewField> Fields { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }

    public class PreviewField
    {
        [JsonProperty("field_id")]
        public int? FieldId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("config")]
        public FieldConfig Config { get; set; }

        [JsonProperty("values")]
        public JToken Values { get; set; }
    }
}