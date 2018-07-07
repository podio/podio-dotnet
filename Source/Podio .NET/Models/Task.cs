using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Task
    {
        [JsonProperty("task_id")]
        public int TaskId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("group")]
        public object Group { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("private")]
        public bool? Private { get; set; }

        [JsonProperty("due_on")]
        public DateTime? DueOn { get; set; }

        [JsonProperty("due_date")]
        public DateTime? DueDate { get; set; }

        [JsonProperty("due_time")]
        public TimeSpan? DueTime { get; set; }

        [JsonProperty("is_liked")]
        public bool IsLiked { get; set; }

        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("presence")]
        public Presence Presence { get; set; }

        [JsonProperty("push")]
        public Push Push { get; set; }

        [JsonProperty("space_id")]
        public int? SpaceId { get; set; }

        [JsonProperty("link")]
        public string link { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("completed_on")]
        public DateTime? CompletedOn { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("ref")]
        public Reference Reference { get; set; }

        [JsonProperty("created_by")]
        public ByLine created_by { get; set; }

        [JsonProperty("completed_by")]
        public ByLine CompletedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("deleted_via")]
        public Via DeletedVia { get; set; }

        [JsonProperty("completed_via")]
        public Via CompletedVia { get; set; }

        [JsonProperty("responsible")]
        public User Responsible { get; set; }

        [JsonProperty("reminder")]
        public Reminder Reminder { get; set; }

        [JsonProperty("recurrence")]
        public Recurrence Recurrence { get; set; }

        [JsonProperty("labels")]
        public List<TaskLabel> Labels { get; set; }

        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }
    }
}