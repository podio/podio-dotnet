using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Grant
    {
        [JsonProperty("grant_id")]
        public int grantId { get; set; }

        [JsonProperty("ref_type")]
        public string refType { get; set; }

        [JsonProperty("ref_id")]
        public int refId { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("ref")]
        public Ref Reference { get; set; }
    }
}