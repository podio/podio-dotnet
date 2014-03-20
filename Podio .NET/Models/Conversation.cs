using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PodioAPI.Models
{
    public class Conversation
    {
        [JsonProperty(PropertyName = "conversation_id")]
        public int? ConversationId { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "ref")]
        public Reference Ref { get; set; }

        [JsonProperty(PropertyName = "embed")]
        public Embed Embed { get; set; }

        [JsonProperty(PropertyName = "embed_file")]
        public FileAttachment EmbedFile { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty(PropertyName = "files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public List<ConversationMessage> Messages { get; set; }

        [JsonProperty(PropertyName = "participants_full", Required = Required.AllowNull)]
        public List<ConversationParticipant> ParticipantsFull { get; set; }
    }
}
