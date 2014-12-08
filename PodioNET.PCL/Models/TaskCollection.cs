﻿using Newtonsoft.Json;
using System.Collections.Generic;

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
