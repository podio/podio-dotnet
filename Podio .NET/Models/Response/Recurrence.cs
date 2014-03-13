using Newtonsoft.Json;
using System;

namespace PodioAPI.Models.Response
{
    public class Recurrence
    {
        [JsonProperty(PropertyName = "recurrence_id")]
        public int? RecurrenceId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "config")]
        public RecurrenceConfig Config { get; set; }

        [JsonProperty(PropertyName = "step")]
        public int? Step { get; set; }

        [JsonProperty(PropertyName = "until")]
        public DateTime? Until { get; set; }
    }

    public class RecurrenceConfig
    {
        [JsonProperty(PropertyName = "days")]
        public string Days { get; set; }

        [JsonProperty(PropertyName = "repeat_on")]
        public string RepeatOn { get; set; }
    }
}
