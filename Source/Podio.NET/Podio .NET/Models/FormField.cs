using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class FormField
    {
        /// <summary>
        ///     The id of the field
        /// </summary>
        [JsonProperty("field_id", NullValueHandling = NullValueHandling.Ignore)]
        public int FieldId { get; set; }

        /// <summary>
        ///     Any settings of the field which depends on the type of the field. See area for more information
        /// </summary>
        [JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
        public FormFieldSettings FormFieldSettings { get; set; }
    }
}