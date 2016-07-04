using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Form
    {
        /// <summary>
        ///     The id of the form
        /// </summary>
        [JsonProperty("form_id", NullValueHandling = NullValueHandling.Ignore)]
        public int FormId { get; set; }

        /// <summary>
        ///     The id of the app the form belongs to
        /// </summary>
        [JsonProperty("app_id", NullValueHandling = NullValueHandling.Ignore)]
        public int AppId { get; set; }

        /// <summary>
        ///     The id of the space the form belongs to
        /// </summary>
        [JsonProperty("space_id", NullValueHandling = NullValueHandling.Ignore)]
        public int SpaceId { get; set; }

        /// <summary>
        ///     Either "active" or "disabled"
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        /// <summary>
        ///     The settings of the form
        /// </summary>
        [JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
        public FormSettings Settings { get; set; }

        /// <summary>
        ///     The list of domains where the form can be used
        /// </summary>
        [JsonProperty("domains", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Domains { get; set; }

        /// <summary>
        ///     True if attachments are allowed, false otherwise
        /// </summary>
        [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool Attachments { get; set; }

        /// <summary>
        ///     The ids for each active field
        /// </summary>
        [JsonProperty("field_ids", NullValueHandling = NullValueHandling.Ignore)]
        public int[] fieldIds { get; set; }

        /// <summary>
        ///     The id and any form specific settings for each active field
        /// </summary>
        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<FormField> Fields { get; set; }
    }
}