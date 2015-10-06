using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ConversationParticipant
    {
        [JsonProperty(PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public ByLine CreatedBy { get; set; }
    }
}