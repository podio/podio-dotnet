using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class CalendarEvent
    {
        [JsonProperty("ref_type")]
        public string RefType { get; set; }

        [JsonProperty("ref_id")]
        public string RefId { get; set; }

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

        [JsonProperty("start_utc")]
        public DateTime? StartUtc { get; set; }

        [JsonProperty("end_utc")]
        public DateTime? EndUtc { get; set; }

        [JsonProperty("app")]
        public Application App { get; set; }

        [JsonProperty("color")]
        public Application Color { get; set; }
    }
}