using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class UserStatus
    {
        [JsonProperty("inbox_new")]
        public int inboxNew { get; set; }

        [JsonProperty("calendar_code")]
        public string calenderCode { get; set; }

        [JsonProperty("message_unread_count")]
        public int MessageUnreadCount { get; set; }

        [JsonProperty("mailbox")]
        public string mailBox { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("profile")]
        public Contact Profile { get; set; }

        [JsonProperty("properties")]
        public JToken Properties { get; set; }
    }
}