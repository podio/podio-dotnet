using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public partial class Ref
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public dynamic Id { get; set; }
    }
}