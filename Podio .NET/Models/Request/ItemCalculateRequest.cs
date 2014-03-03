using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Request
{
    public class Formula
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string value { get; set; }
    }

    public class Grouping
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public int value { get; set; }

        [JsonProperty("sub_value", NullValueHandling = NullValueHandling.Ignore)]
        public string sub_value { get; set; }
    }

    public class Filter
    {

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string key { get; set; }

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public string values { get; set; }
    }

    public class ItemCalculateRequest
    {
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int limit { get; set; }

        [JsonProperty("aggregation", NullValueHandling = NullValueHandling.Ignore)]
        public string aggregation { get; set; }

        [JsonProperty("formula", NullValueHandling = NullValueHandling.Ignore)]
        public List<Formula> formula { get; set; }

        [JsonProperty("groupings", NullValueHandling = NullValueHandling.Ignore)]
        public List<Grouping> groupings { get; set; }

        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public List<Filter> filters { get; set; }
    }
}
