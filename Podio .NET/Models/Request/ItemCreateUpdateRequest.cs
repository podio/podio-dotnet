using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models.Request
{
    public class ItemCreateUpdateRequest
    {
        [JsonProperty(PropertyName = "external_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalId { get; set; }

        /// <summary>
        ///     Only used for update item
        /// </summary>
        [JsonProperty(PropertyName = "revision", NullValueHandling = NullValueHandling.Ignore)]
        public int? Revision { get; set; }

        [JsonProperty(PropertyName = "fields", NullValueHandling = NullValueHandling.Ignore)]
        public JArray Fields { get; set; }

        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; }

        [JsonProperty(PropertyName = "file_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> FileIds { get; set; }

        [JsonProperty(PropertyName = "reminder", NullValueHandling = NullValueHandling.Ignore)]
        public Reminder Reminder { get; set; }

        [JsonProperty(PropertyName = "recurrence", NullValueHandling = NullValueHandling.Ignore)]
        public Recurrence Recurrence { get; set; }

        [JsonProperty(PropertyName = "linked_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? LinkedAccountId { get; set; }

        [JsonProperty(PropertyName = "ref", NullValueHandling = NullValueHandling.Ignore)]
        public Reference Ref { get; set; }

        /// <summary>
        /// To use with the Platform API
        /// </summary>
        [JsonProperty(PropertyName = "space_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SpaceId { get; set; }
    }
}