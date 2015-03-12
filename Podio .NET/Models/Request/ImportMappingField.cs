using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class ImportMappingField
    {
        /// <summary>
        ///     The id of the field in the app
        /// </summary>
        [JsonProperty("field_id")]
        public int FieldId { get; set; }

        /// <summary>
        ///     True of the values for the field is unique, false otherwise
        /// </summary>
        [JsonProperty("unique")]
        public bool Unique { get; set; }

        /// <summary>
        ///     Which value should be mapped for the field, depends on the type of field
        /// </summary>
        [JsonProperty("value")]
        public dynamic Value { get; set; }
    }
}