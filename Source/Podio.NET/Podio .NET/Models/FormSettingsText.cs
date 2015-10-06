using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class FormSettingsText
    {
        /// <summary>
        ///     A heading to display when the webform is not embedded (OPTIONAL)
        /// </summary>
        [JsonProperty("heading", NullValueHandling = NullValueHandling.Ignore)]
        public string Heading { get; set; }

        /// <summary>
        ///     An explanatory text to display when the webform is not embedded (OPTIONAL)
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        ///     The text for the submit button
        /// </summary>
        [JsonProperty("submit", NullValueHandling = NullValueHandling.Ignore)]
        public string Submit { get; set; }

        /// <summary>
        ///     The text when the form was successfully submitted
        /// </summary>
        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public string Success { get; set; }
    }
}