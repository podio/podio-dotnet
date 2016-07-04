using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class FormFieldSettings
    {
        /// <summary>
        ///     See contact area for a list of all supported contact field types
        /// </summary>
        [JsonProperty("contact_field_types", NullValueHandling = NullValueHandling.Ignore)]
        public string[] ContactFieldTypes { get; set; }
    }
}