using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class ExportFilter : FilterBase
    {
        /// <summary>
        ///     The id of the view to use, 0 means last used view, blank means no view
        /// </summary>
        [JsonProperty("view_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ViewId { get; set; }
    }
}