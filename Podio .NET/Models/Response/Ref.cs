using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public partial class Ref
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }
    }
}
