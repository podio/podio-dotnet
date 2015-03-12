using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ActivityGroup
    {
        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }

        [JsonProperty("authors")]
        public IEnumerable<ByLine> Authors { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("group_id")]
        public int GroupId { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }
    }
}