using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace PodioAPI.Models.Response
{
    public class Notification
    {
        [JsonProperty("notification_id")]
        public int? NotificationId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public Dictionary<string,object> Data { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("text")]
        public string  Text { get; set; }

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
