using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace PodioAPI.Models
{
    public class Item
    {
        [JsonProperty("item_id")]
        public long ItemId { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("app_item_id_formatted")]
        public string AppItemIdFormatted { get; set; }

        [JsonProperty("app_item_id")]
        public int? AppItemId { get; set; }


        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("initial_revision")]
        public ItemRevision InitialRevision { get; set; }

        [JsonProperty("current_revision")]
        public ItemRevision CurrentRevision { get; set; }

        [JsonProperty("fields")]
        public List<ItemField> Fields { get; set; }


        //Extra properties for full item
        [JsonProperty("ratings")]
        public Dictionary<string, object> Ratings { get; set; }

        [JsonProperty("user_ratings")]
        public Dictionary<string, object> UserRatings { get; set; }

        [JsonProperty("last_event_on")]
        public DateTime? LastEventOn { get; set; }

        [JsonProperty("participants")]
        public JObject Participants { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("refs")]
        public JArray Refs { get; set; }

        [JsonProperty("references")]
        public JArray References { get; set; }

        [JsonProperty("linked_account_id")]
        public int? LinkedAccountId { get; set; }

        [JsonProperty("subscribed")]
        public bool? Subscribed { get; set; }

        [JsonProperty("invite")]
        public JToken Invite { get; set; }

        [JsonProperty("is_liked")]
        public bool IsLiked { get; set; }

        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        [JsonProperty("app")]
        public Application App { get; set; }

        [JsonProperty("ref")]
        public Reference Ref { get; set; }

        [JsonProperty("reminder")]
        public Reminder Reminder { get; set; }

        [JsonProperty("recurrence")]
        public Recurrence Recurrence { get; set; }

        [JsonProperty("linked_account_data")]
        public LinkedAccountData LinkedAccountData { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("revisions")]
        public List<ItemRevision> Revisions { get; set; }

        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty("grant")]
        public Grant Grant { get; set; }

        [JsonProperty("file_ids")]
        public List<int> FileIds { get; set; }

        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }

        [JsonProperty("shares")]
        public List<AppMarketShare> Shares { get; set; }

        //When getting item collection
        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        [JsonProperty("task_count")]
        public int TaskCount { get; set; }

        public Item()
        {
            this.Fields = new List<ItemField>();
        }

        public T Field<T>(string externalId)
             where T : ItemField, new()
        {
            var genericField = this.Fields.Find(field => field.ExternalId == externalId);
            return fieldInstance<T>(genericField, externalId);
        }

        public T Field<T>(int fieldId)
            where T : ItemField, new()
        {
            var genericField = this.Fields.Find(field => field.FieldId == fieldId);
            return fieldInstance<T>(genericField, null, fieldId);
        }

        protected T fieldInstance<T>(ItemField genericField, string externalId = null, int? fieldId = null)
            where T : ItemField, new()
        {
            T specificField = new T();
            if (genericField != null)
            {
                foreach (var property in genericField.GetType().GetProperties())
                {
                    var jsonAttribute =
                        ((JsonPropertyAttribute[])property.GetCustomAttributes(typeof(JsonPropertyAttribute), false));
                    if (jsonAttribute.Length > 0)
                        specificField.GetType()
                            .GetProperty(property.Name)
                            .SetValue(specificField, property.GetValue(genericField, null), null);
                }
            }
            else
            {
                specificField.ExternalId = externalId;
                specificField.FieldId = fieldId;
                this.Fields.Add(specificField);
            }
            return specificField;
        }
    }
}