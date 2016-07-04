using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ByLine
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "avatar_type")]
        public string AvatarType { get; set; }

        [JsonProperty(PropertyName = "avatar_id")]
        public int? AvatarId { get; set; }

        [JsonProperty(PropertyName = "image")]
        public Image Image { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "avatar")]
        public int? Avatar { get; set; }
    }
}