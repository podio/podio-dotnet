using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ApplicationRevision
    {
        [JsonProperty("revision")]
        public string Revision { get; set; }
    }
}