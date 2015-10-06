using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class SpaceMember
    {
        [JsonProperty("employee")]
        public bool? Employee { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("invited_on")]
        public DateTime? InvitedOn { get; set; }

        [JsonProperty("started_on")]
        public DateTime? started_on { get; set; }

        [JsonProperty("ended_on")]
        public DateTime? ended_on { get; set; }

        [JsonProperty("grants")]
        public int? Grant { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("profile")]
        public Contact Contact { get; set; }

        [JsonProperty("space")]
        public Space Space { get; set; }
    }
}