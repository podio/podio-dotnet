using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class UserMail
    {
        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
}