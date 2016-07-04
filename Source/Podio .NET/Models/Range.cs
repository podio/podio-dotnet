using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Range
    {
        [JsonProperty("max")]
        public float Min { get; set; }

        [JsonProperty("min")]
        public float Max { get; set; }
    }
}