using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class RatingType
    {
        [JsonProperty("average")]
        public double? Average { get; set; }

        [JsonProperty("counts")]
        //[JsonConverter(typeof(ExpandoObjectConverter))]
        public JObject Count { get; set; }
    }
}