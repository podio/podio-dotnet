using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models
{
    public class Cause
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Config { get; set; }
    }
}
