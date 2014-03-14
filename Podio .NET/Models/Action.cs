using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Action
    {
        [JsonProperty("action_id")]
        public int? ActionId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public Dictionary<string,object> Data { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
