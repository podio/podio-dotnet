using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Response
{
    public class Range
    {
        [JsonProperty("max")]
        public float Min { get; set; }
        [JsonProperty("min")]
        public float Max { get; set; }  
    }
}
