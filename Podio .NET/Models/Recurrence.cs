using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Recurrence
    {
        [JsonProperty(PropertyName = "recurrence_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? RecurrenceId { get; set; }

        /// <summary>
        ///     The name of the recurrence, "weekly", "monthly" or "yearly"
        /// </summary>
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "config", NullValueHandling = NullValueHandling.Ignore)]
        public RecurrenceConfig Config { get; set; }

        /// <summary>
        ///     The step size, 1 or more
        /// </summary>
        [JsonProperty(PropertyName = "step", NullValueHandling = NullValueHandling.Ignore)]
        public int? Step { get; set; }

        /// <summary>
        ///     The latest date the recurrence should take place
        /// </summary>
        [JsonProperty(PropertyName = "until", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Until { get; set; }
    }

    public class RecurrenceConfig
    {
        /// <summary>
        ///     List of weekdays ("monday", "tuesday", etc) (for "weekly")
        /// </summary>
        [JsonProperty(PropertyName = "days", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Days { get; set; }

        /// <summary>
        ///     When to repeat, "day_of_week" or "day_of_month" (for "monthly")
        /// </summary>
        [JsonProperty(PropertyName = "repeat_on", NullValueHandling = NullValueHandling.Ignore)]
        public string RepeatOn { get; set; }
    }
}