using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class Action
    {
        [JsonProperty("action_id")]
        public int? ActionId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
