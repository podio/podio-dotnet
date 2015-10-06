using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ApplicationConfig
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("usage")]
        public string Usage { get; set; }

        [JsonProperty("external_id")]
        public object ExternalId { get; set; }

        [JsonProperty("icon_id")]
        public int IconId { get; set; }

        [JsonProperty("allow_edit")]
        public bool AllowEdit { get; set; }

        [JsonProperty("default_view")]
        public string DefaultView { get; set; }

        [JsonProperty("allow_attachments")]
        public bool AllowAttachments { get; set; }

        [JsonProperty("allow_comments")]
        public bool AllowComments { get; set; }

        [JsonProperty("allow_create")]
        public bool AllowCreate { get; set; }

        [JsonProperty("silent_creates")]
        public bool SilentCreates { get; set; }

        [JsonProperty("silent_edits")]
        public bool SilentEdits { get; set; }

        [JsonProperty("disable_notifications")]
        public bool DisableNotifications { get; set; }

        [JsonProperty("fivestar")]
        public bool Fivestar { get; set; }

        [JsonProperty("fivestar_label")]
        public string FivestarLabel { get; set; }

        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("thumbs")]
        public bool Thumbs { get; set; }

        [JsonProperty("thumbs_label")]
        public string ThumbsLabel { get; set; }

        [JsonProperty("rsvp")]
        public bool Rsvp { get; set; }

        [JsonProperty("rsvp_label")]
        public string RsvpLabel { get; set; }

        [JsonProperty("yesno")]
        public bool Yesno { get; set; }

        [JsonProperty("yesno_label")]
        public string YesnoLabel { get; set; }

        [JsonProperty("tasks")]
        public List<AutoTask> Tasks { get; set; }
    }
}