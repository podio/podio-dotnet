using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class StreamObject
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("last_update_on")]
        public DateTime? LastUpdateOn { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }

        [JsonProperty("comments_allowed")]
        public bool? CommentsAllowed { get; set; }

        [JsonProperty("user_ratings")]
        public JObject UserRatings { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("app")]
        public Application App { get; set; }

        [JsonProperty("space")]
        public Space Space { get; set; }

        [JsonProperty("org")]
        public Organization Organization { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty("activity")]
        public List<Activity> Activity { get; set; }
    }
}