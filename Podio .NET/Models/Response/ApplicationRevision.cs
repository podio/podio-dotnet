using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public class ApplicationRevision
    {
        [JsonProperty("revision")]
        public string Revision { get; set; }
    }
}
