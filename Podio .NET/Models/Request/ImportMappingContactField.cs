using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class ImportMappingContactField
    {
        /// <summary>
        ///     The key for the field on the contact
        /// </summary>
        [JsonProperty("field_key")]
        public string FieldKey { get; set; }

        /// <summary>
        ///     True of the value for the field is unique, false otherwise
        /// </summary>
        [JsonProperty("unique")]
        public bool Unique { get; set; }

        /// <summary>
        ///     The id of the column to be used for the given field
        /// </summary>
        [JsonProperty("column_id")]
        public string ColumnId { get; set; }
    }
}