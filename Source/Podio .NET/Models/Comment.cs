using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Comment
    {
        [JsonProperty("comment_id")]
        public int CommentId { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("rich_value")]
        public string RichValue { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("space_id")]
        public object SpaceId { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("ref")]
        public Reference Ref { get; set; }

        [JsonProperty("embed")]
        public Embed Embed { get; set; }

        [JsonProperty("embed_file")]
        public FileAttachment EmbedFile { get; set; }

        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }

        [JsonProperty("is_liked")]
        public bool IsLiked { get; set; }

        [JsonProperty("like_count")]
        public int LikeCount { get; set; }
    }
}