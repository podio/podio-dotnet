using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Status
    {
        [JsonProperty("status_id")]
        public int StatusId { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("rich_value")]
        public string RichValue { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("ratings")]
        public Dictionary<string, object> Ratings { get; set; }

        [JsonProperty("subscribed")]
        public bool? Subscribed { get; set; }

        [JsonProperty("user_ratings")]
        public Dictionary<string, object> UserRatings { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("embed")]
        public Embed Embed { get; set; }

        [JsonProperty("embed_file")]
        public FileAttachment EmbedFile { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("conversations")]
        public List<Conversation> Conversations { get; set; }

        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }

        [JsonProperty("shares")]
        public List<AppMarketShare> Shares { get; set; }

        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }
    }
}