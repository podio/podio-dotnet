using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public class Item
    {
        [JsonProperty("item_id")]
        public int ItemId { get; set; }

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
        public Dictionary<string, object> Participants { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("refs")]
        public List<Dictionary<string, object>> Refs { get; set; }

        [JsonProperty("references")]
        public Dictionary<string, object> References { get; set; }

        [JsonProperty("linked_account_id")]
        public int? LinkedAccountId { get; set; }

        [JsonProperty("subscribed")]
        public bool? Subscribed { get; set; }

        [JsonProperty("invite")]
        public Dictionary<string, object> Invite { get; set; }


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

        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }

        [JsonProperty("shares")]
        public List<AppMarketShare> Shares { get; set; }

        //When getting item collection
        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        [JsonProperty("task_count")]
        public int TaskCount { get; set; }

        public T Field<T>(string externalId) where T: new()
        {
            var fieldToConvert = this.Fields.Where(x => x.ExternalId == externalId);
            if (fieldToConvert.Any())
            {
                var field = fieldToConvert.First();
                var f = field.Values.First();
                return f.ToObject<T>();
            }
            return default(T);
        }
    }
}
