using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class FieldConfig
    {
        [JsonProperty("default_value", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultValue { get; private set; }

        /// <summary>
        ///     The description of the field, shown to the user when inserting and editing.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
        public JObject Settings { get; set; }

        [JsonProperty("required", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Required { get; set; }

        /// <summary>
        ///     The mapping of the field, one of "meeting_time", "meeting_participants", "meeting_agenda" and "meeting_location".
        /// </summary>
        [JsonProperty("mapping", NullValueHandling = NullValueHandling.Ignore)]
        public string Mapping { get; set; }

        /// <summary>
        ///     Label of the field, This is required on Application create.
        /// </summary>
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Visible { get; set; }

        /// <summary>
        ///     An integer indicating the order of the field compared to other fields
        /// </summary>
        [JsonProperty("delta", NullValueHandling = NullValueHandling.Ignore)]
        public int? Delta { get; set; }

        [JsonProperty("hidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Hidden { get; set; }
    }
}