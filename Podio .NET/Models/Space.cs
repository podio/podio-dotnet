using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Space
    {
        [JsonProperty("space_id")]
        public int SpaceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("url_label")]
        public string UrlLabel { get; set; }

        [JsonProperty("org_id")]
        public int? OrgId { get; set; }

        [JsonProperty("contact_count")]
        public int? ContactCount { get; set; }

        [JsonProperty("members")]
        public int? Members { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("rights")]
        public String[] Rights { get; set; }

        [JsonProperty("post_on_new_app")]
        public bool? PostOnNewApp { get; set; }

        [JsonProperty("post_on_new_member")]
        public bool? PostOnNewMember { get; set; }

        [JsonProperty("subscribed")]
        public bool? Subscribed { get; set; }

        [JsonProperty("privacy")]
        public string Privacy { get; set; }

        [JsonProperty("auto_join")]
        public bool? AutoJoin { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("premium")]
        public bool? Premium { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("last_activity_on")]
        public DateTime? LastActivityOn { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("org")]
        public Organization Org { get; set; }

        [JsonProperty("is_overdue")]
        public bool IsOverdue { get; set; }

        [JsonProperty("owner")]
        public Ref Owner { get; set; }

        [JsonProperty("archived")]
        public bool? Archived { get; set; }

        [JsonProperty("push")]
        public Push Push { get; set; }
    }
}