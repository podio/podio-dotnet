using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class Reference
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "id")]
        public long? Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; private set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; private set; }

        [JsonProperty(PropertyName = "created_on")]
        public DateTime? CreatedOn { get; private set; }

        [JsonProperty(PropertyName = "created_by")]
        public ByLine CreatedBy { get; private set; }

        [JsonProperty(PropertyName = "created_via")]
        public Via CreatedVia { get; private set; }

        [JsonProperty(PropertyName = "data")]
        public JObject Data { get; private set; }

        [JsonProperty("accessor_count")]
        public int AccessorCount { get; set; }
    }
}