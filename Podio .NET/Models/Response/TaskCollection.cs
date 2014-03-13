using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Response
{
    public class TaskCollection
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }
    }  
}
