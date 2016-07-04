using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class LinkedAccountData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "info")]
        public string Info { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}