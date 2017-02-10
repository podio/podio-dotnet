using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace PodioAPI.Models.Request
{
    public class Formula
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public class Grouping
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("sub_value", NullValueHandling = NullValueHandling.Ignore)]
        public string SubValue { get; set; }
    }

    public class Filter
    {
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public class ItemCalculateRequest
    {
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int Limit { get; set; }

        [JsonProperty("aggregation", NullValueHandling = NullValueHandling.Ignore)]
        public string Aggregation { get; set; }

        [JsonProperty("formula", NullValueHandling = NullValueHandling.Ignore)]
        public List<Formula> Formula { get; set; }

        [JsonProperty("groupings", NullValueHandling = NullValueHandling.Ignore)]
        public List<Grouping> Groupings { get; set; }

        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public Object Filters { get; set; }
    }
}