using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ApplicationConfiguration
    {
        /// <summary>
        ///     The type of the app, either "standard" or "meeting"
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        ///     Name of the application. This is required on Application create.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     The name of each item in an app. This is required on Application create.
        /// </summary>
        [JsonProperty("item_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemName { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        ///     The usage information of the app.
        /// </summary>
        [JsonProperty("usage", NullValueHandling = NullValueHandling.Ignore)]
        public string Usage { get; set; }

        [JsonProperty("external_id", NullValueHandling = NullValueHandling.Ignore)]
        public object ExternalId { get; set; }

        [JsonProperty("icon_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? IconId { get; set; }

        /// <summary>
        ///     The icon for the app. This is required on Application create.
        /// </summary>
        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("allow_edit", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowEdit { get; set; }

        /// <summary>
        ///     The default view of the app items on the app main page. Possible values: badge, table, stream, calendar or card
        /// </summary>
        [JsonProperty("default_view", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultView { get; set; }

        [JsonProperty("allow_attachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowAttachments { get; set; }

        [JsonProperty("allow_comments", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowComments { get; set; }

        [JsonProperty("allow_create", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowCreate { get; set; }

        [JsonProperty("silent_creates", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SilentCreates { get; set; }

        [JsonProperty("silent_edits", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SilentEdits { get; set; }

        [JsonProperty("disable_notifications", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisableNotifications { get; set; }

        [JsonProperty("fivestar", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Fivestar { get; set; }

        [JsonProperty("fivestar_label", NullValueHandling = NullValueHandling.Ignore)]
        public string FivestarLabel { get; set; }

        [JsonProperty("approved", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Approved { get; set; }

        [JsonProperty("thumbs", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Thumbs { get; set; }

        [JsonProperty("thumbs_label", NullValueHandling = NullValueHandling.Ignore)]
        public string ThumbsLabel { get; set; }

        [JsonProperty("rsvp", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Rsvp { get; set; }

        [JsonProperty("rsvp_label", NullValueHandling = NullValueHandling.Ignore)]
        public string RsvpLabel { get; set; }

        [JsonProperty("yesno", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Yesno { get; set; }

        [JsonProperty("yesno_label", NullValueHandling = NullValueHandling.Ignore)]
        public string YesnoLabel { get; set; }

        [JsonProperty("tasks", NullValueHandling = NullValueHandling.Ignore)]
        public List<AutoTask> Tasks { get; set; }
    }
}