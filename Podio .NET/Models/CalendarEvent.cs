using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class CalendarEvent
    {
        [JsonProperty("ids")]
        public int? Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("start")]
        public DateTime? Start { get; set; }

        [JsonProperty("end")]
        public DateTime? End { get; set; }
    }
}
