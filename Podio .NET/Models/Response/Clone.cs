using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Response
{
    public class Clone
    {
        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }
        [JsonProperty("values")]
        public Dictionary<string, object> Values { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}
