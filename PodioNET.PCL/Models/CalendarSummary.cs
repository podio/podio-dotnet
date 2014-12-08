using Newtonsoft.Json;
using System.Collections.Generic;

namespace PodioAPI.Models
{
    public class CalendarSummary
    {
        [JsonProperty("today")]
        public CalendarSummaryGroup Today { get; set; }

        [JsonProperty("upcoming")]
        public CalendarSummaryGroup Upcoming { get; set; }
    }

    public class CalendarSummaryGroup
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("events")]
        public IEnumerable<CalendarEvent> Events { get; set; }
    }
}
