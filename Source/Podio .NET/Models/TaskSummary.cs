using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class TaskSummary
    {
        [JsonProperty("other")]
        public TaskCollection Other { get; set; }

        [JsonProperty("today")]
        public TaskCollection Today { get; set; }

        [JsonProperty("overdue")]
        public TaskCollection Overdue { get; set; }
    }
}