using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class SearchReferencesRequest
    {
        /// <summary>
        ///     The target use of the references: must be one of {"task_reference", "task_responsible", "alert", "conversation",
        ///     "conversation_presence", "grant", "item_field", "item_created_by", "item_created_via", "item_tags", "global_nav"}
        /// </summary>
        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; set; }

        /// <summary>
        ///     Any target-specific extra parameters
        /// </summary>
        [JsonProperty("target_params", NullValueHandling = NullValueHandling.Ignore)]
        public TargetParams TargetParams { get; set; }

        /// <summary>
        ///     The text to search for (optional).
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        /// <summary>
        ///     The maximum number of results to return (optional).
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }
    }

    public class TargetParams
    {
        /// <summary>
        ///     if target = "item_field". Id of the field to search for. Currently only fields of type "app" and "contact" are
        ///     supported.
        /// </summary>
        [JsonProperty("field_id", NullValueHandling = NullValueHandling.Ignore)]
        public int FieldId { get; set; }

        /// <summary>
        ///     if target = "item_field". A list of item id's that should be excluded from the result.
        /// </summary>
        [JsonProperty("not_item_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> NotItemIds { get; set; }

        /// <summary>
        ///     if  target = "item_created_by", "item_created_via" or "item_tags". Id of the app to search for.
        /// </summary>
        [JsonProperty("app_id", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> AppId { get; set; }
    }
}