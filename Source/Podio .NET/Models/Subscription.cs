using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Subscription
    {
        [JsonProperty("started_on")]
        public DateTime StartedOn { get; set; }

        [JsonProperty("notifications")]
        public int Notifications { get; set; }

        [JsonProperty("ref")]
        public Ref Reference { get; set; }
    }
}