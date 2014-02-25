using System;
using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public class User
    {
        [JsonProperty("user_id")]
        public int? UserId { get; set; }

        [JsonProperty("profile_id")]
        public int? ProfileId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("avatar")]
        public int? Avatar { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("flags")]
        public String[] Flags { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("profile")]
        public Contact Profile { get; set; }

        [JsonProperty("mails")]
        public UserMail Mails { get; set; }
    }
}
