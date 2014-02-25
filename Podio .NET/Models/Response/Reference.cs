using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PodioAPI.Models.Response
{
    public class Reference
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(PropertyName = "data")]
        public Dictionary<string, object> Data { get; set; }

        [JsonProperty(PropertyName = "created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty(PropertyName = "created_via")]
        public Via CreatedVia { get; set; }
    }
}
