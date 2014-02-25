using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public partial class Ref
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}
