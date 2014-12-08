using Newtonsoft.Json;
using System.Collections.Generic;

namespace PodioAPI.Models
{
    public class CreatedGrant
    {
        [JsonProperty("invitable")]
        public List<User> Profiles { get; set; }
    }
}
