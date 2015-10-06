using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class FilterOptions : FilterBase
    {
        /// <summary>
        ///     True if the view should be remembered, false otherwise
        /// </summary>
        [JsonProperty("remember", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Remember { get; set; }
    }
}