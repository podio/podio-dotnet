using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Reminder
    {
        [JsonProperty("reminder_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ReminderId { get; private set; }

        /// <summary>
        ///     Minutes to remind before the due_date
        /// </summary>
        [JsonProperty("remind_delta", NullValueHandling = NullValueHandling.Ignore)]
        public int ReminderDelta { get; set; }
    }
}