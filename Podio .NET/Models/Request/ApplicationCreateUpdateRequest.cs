using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class ApplicationCreateUpdateRequest
    {
        [JsonProperty("space_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SpaceId { get; set; }

        [JsonProperty(PropertyName = "config", NullValueHandling = NullValueHandling.Ignore)]
        public ApplicationConfiguration Config { get; set; }

        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApplicationField> Fields { get; set; }
    }
}