using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Via
    {
        [JsonProperty(PropertyName = "auth_client_id")]
        public int? AuthClientId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "display")]
        public bool Display { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}