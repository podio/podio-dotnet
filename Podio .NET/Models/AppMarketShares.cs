using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class AppMarketShares
    {
        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("shares")]
        public List<AppMarketShare> Shares { get; set; }
    }
}