using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class Flow
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("flow_id")]
        public int FlowId { get; set; }

        [JsonProperty("execution_count")]
        public int ExecutionCount { get; set; }

        [JsonProperty("ref")]
        public Ref Ref { get; set; }

        [JsonProperty("config")]
        public JObject Config { get; set; }

        [JsonProperty("effects")]
        public List<Effect> effects { get; set; }
    }
}