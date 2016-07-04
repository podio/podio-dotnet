using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class FormSettings
    {
        /// <summary>
        ///     True if captcha is enabled, false otherwise
        /// </summary>
        [JsonProperty("captcha", NullValueHandling = NullValueHandling.Ignore)]
        public bool Captcha { get; set; }

        /// <summary>
        ///     The texts used for the form
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public FormSettingsText Text { get; set; }

        /// <summary>
        ///     The colors for the form in the form "#xxxxxx"
        /// </summary>
        [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        /// <summary>
        ///     The theme to use, for a list of valid themes see the area
        /// </summary>
        [JsonProperty("theme", NullValueHandling = NullValueHandling.Ignore)]
        public string Theme { get; set; }

        /// <summary>
        ///     Optional inline css to include in the form
        /// </summary>
        [JsonProperty("css", NullValueHandling = NullValueHandling.Ignore)]
        public string Css { get; set; }
    }
}