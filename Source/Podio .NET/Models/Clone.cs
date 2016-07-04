using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class Clone
    {
        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty("values")]
        public JToken Values { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}