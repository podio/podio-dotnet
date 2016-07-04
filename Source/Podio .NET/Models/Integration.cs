using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class Integration
    {
        [JsonProperty(PropertyName = "integration_id")]
        public int? IntegrationId { get; set; }

        [JsonProperty(PropertyName = "app_id")]
        public int? AppId { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "silent")]
        public bool? Silent { get; set; }

        [JsonProperty(PropertyName = "config")]
        public JToken Config { get; set; }

        [JsonProperty(PropertyName = "mapping")]
        public JToken Mapping { get; set; }

        [JsonProperty(PropertyName = "updating")]
        public bool? Updating { get; set; }

        [JsonProperty(PropertyName = "last_updated_on")]
        public DateTime? LastUpdatedOn { get; set; }

        [JsonProperty(PropertyName = "created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public ByLine CreatedBy { get; set; }
    }
}