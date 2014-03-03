using Newtonsoft.Json;
using PodioAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Request
{
    public class ItemCreateUpdateRequest
    {
        [JsonProperty(PropertyName = "external_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalId { get; set; }

        /// <summary>
        /// Only used for update item
        /// </summary>
        [JsonProperty(PropertyName = "revision", NullValueHandling = NullValueHandling.Ignore)]
        public int? Revision { get; set; }

        [JsonProperty(PropertyName = "fields", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<IDictionary<string, object>> Fields { get; set; }

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
    }
}

