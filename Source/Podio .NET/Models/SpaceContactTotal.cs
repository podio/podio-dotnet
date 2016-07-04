using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class SpaceContactTotal
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("space")]
        public SpaceMicro Space { get; set; }
    }
}