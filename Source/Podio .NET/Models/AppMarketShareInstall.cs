using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class AppMarketShareInstall
    {
        [JsonProperty("app_id")]
        public int AppId { get; set; }

        [JsonProperty("child_app_ids")]
        public List<int> ChildAppIds { get; set; }
    }
}