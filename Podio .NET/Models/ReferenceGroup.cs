using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class ReferenceGroup
    {
        /// <summary>
        ///     The name of the group; one of {"spaces", "app", "profiles", "created_bys", "created_bys", "tags", "space_contacts",
        ///     "space_members", "auth_clients", "tasks"}
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "data")]
        public JObject Data { get; set; }

        [JsonProperty(PropertyName = "contents")]
        public JArray Contents { get; set; }
    }
}