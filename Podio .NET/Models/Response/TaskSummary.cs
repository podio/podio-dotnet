using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Response
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
