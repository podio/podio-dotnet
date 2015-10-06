using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class Notifications
    {
        [JsonProperty("notifications")]
        public List<Notification> Notification { get; set; }
    }

    public class Notification
    {
        [JsonProperty("notification_id")]
        public int? NotificationId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public JToken Data { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("viewed_on")]
        public DateTime ViewedOn { get; set; }

        [JsonProperty("subscription_id")]
        public int? SubscriptionId { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("starred")]
        public bool? Starred { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}