using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PodioAPI.Models.Response
{
    public class Recurrence
    {
        [JsonProperty(PropertyName = "recurrence_id")]
        public int? RecurrenceId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "config")]
        public Dictionary<string, object> Config { get; set; }

        [JsonProperty(PropertyName = "step")]
        public int? Step { get; set; }

        [JsonProperty(PropertyName = "until")]
        public DateTime? Until { get; set; }
    }
}
