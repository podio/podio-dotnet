using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class TaskReport
    {
        [JsonProperty("reassigned")]
        public TaskType Reassigned { get; set; }

        [JsonProperty("own")]
        public TaskType Own { get; set; }
    }

    public class TaskType
    {
        [JsonProperty("completed_yesterday")]
        public int CompletedYesterday { get; set; }

        [JsonProperty("upcoming")]
        public int Upcoming { get; set; }

        [JsonProperty("later")]
        public int Later { get; set; }

        [JsonProperty("tomorrow")]
        public int Tomorrow { get; set; }

        [JsonProperty("today")]
        public int Today { get; set; }

        [JsonProperty("overdue")]
        public int Overdue { get; set; }
    }
}