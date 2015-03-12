using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class TaskCollection
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }
    }
}