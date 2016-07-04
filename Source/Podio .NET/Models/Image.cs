using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Image
    {
        [JsonProperty("hosted_by")]
        public string HostedBy { get; set; }

        [JsonProperty("hosted_by_humanized_name")]
        public string HostedByHumanizedName { get; set; }

        [JsonProperty("thumbnail_link")]
        public string ThumbnailLink { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("file_id")]
        public int FileId { get; set; }

        [JsonProperty("link_target")]
        public string LinkTarget { get; set; }
    }
}