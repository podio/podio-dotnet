using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class StreamObjectV3
    {
        [JsonProperty("activity_groups")]
        public List<ActivityGroup> ActivityGoups { get; set; }

        [JsonProperty("app")]
        public Application App { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }

        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("is_liked")]
        public bool IsLiked { get; set; }

        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        [JsonProperty("link")]
        public string link { get; set; }

        [JsonProperty("org")]
        public Organization Org { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; private set; }

        [JsonProperty("space")]
        public SpaceMicro Space { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("subscribed_count")]
        public int SubscribedCount { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}