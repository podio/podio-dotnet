using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Push
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}