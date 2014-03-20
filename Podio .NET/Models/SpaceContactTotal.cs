using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models
{
    public class SpaceContactTotal
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("space")]
        public SpaceMicro Space { get; set; }
    }
}
