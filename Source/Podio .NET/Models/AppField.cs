using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class AppField
    {
        [JsonProperty("field_id")]
        public int FieldId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("config")]
        public FieldConfig Config { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}