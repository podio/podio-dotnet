using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Reminder
    {
        [JsonProperty("reminder_id")]
        public int ReminderId { get; private set; }

        [JsonProperty("reminder_delta")]
        public int ReminderDelta { get; set; }
    }
}
