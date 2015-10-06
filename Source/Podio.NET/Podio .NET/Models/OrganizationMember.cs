using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class OrganizationMember
    {
        [JsonProperty(PropertyName = "profile")]
        public Contact Profile { get; set; }

        [JsonProperty(PropertyName = "admin")]
        public bool? Admin { get; set; }

        [JsonProperty(PropertyName = "employee")]
        public bool? Employee { get; set; }

        [JsonProperty(PropertyName = "space_memberships")]
        public int? SpaceMemberships { get; set; }

        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        [JsonProperty(PropertyName = "spaces")]
        public List<Space> Contact { get; set; }
    }
}