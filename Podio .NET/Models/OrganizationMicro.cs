using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class OrganizationMicro
    {
        [JsonProperty("org_id")]
        public int OrgId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("url_label")]
        public string UrlLabel { get; set; }
    }
}