using Newtonsoft.Json;
using System.Collections.Generic;

namespace PodioAPI.Models.Response
{
    public class AutoTask
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("responsible")]
        public List<User> Responsible { get; set; }
    }
}
