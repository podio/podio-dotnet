using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Response
{
   public class ItemCalculate
    {
       [JsonProperty("total")]
       public double? Total { get; set; }

       [JsonProperty("groups")]
       List<CalculationGroup> CalculationGroups { get; set; }

    }
   public class CalculationGroup
   {
       [JsonProperty("groups")]
       public List<ValueGroup> ValueGroups { get; set; }

       [JsonProperty("count")]
       public double? Count { get; set; }
   }
    public class ValueGroup
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
