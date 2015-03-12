using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Conversation
    {
        [JsonProperty(PropertyName = "unread_count")]
        public int? UnreadCount { get; set; }

        [JsonProperty(PropertyName = "presence")]
        public Presence Presence { get; set; }

        [JsonProperty(PropertyName = "participants", Required = Required.AllowNull)]
        public List<User> Participants { get; set; }

        [JsonProperty(PropertyName = "excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty(PropertyName = "push")]
        public Push Push { get; set; }

        [JsonProperty(PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "last_event_on")]
        public DateTime LastEventOn { get; set; }

        [JsonProperty(PropertyName = "conversation_id")]
        public int? ConversationId { get; set; }

        [JsonProperty(PropertyName = "starred")]
        public bool Starred { get; set; }

        [JsonProperty(PropertyName = "unread")]
        public bool Unread { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "ref")]
        public Ref Ref { get; set; }

        [JsonProperty(PropertyName = "embed")]
        public Embed Embed { get; set; }

        [JsonProperty(PropertyName = "embed_file")]
        public FileAttachment EmbedFile { get; set; }

        [JsonProperty(PropertyName = "files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public List<ConversationMessage> Messages { get; set; }
    }
}