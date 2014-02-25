using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public class ApplicationField
    {
        [JsonProperty(PropertyName = "values")]
        public List<Dictionary<string,object>> Values { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "field_id")]
        public int FieldId { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
    }

    public class AppValues
    {
        [JsonProperty(PropertyName = "fields")]
        public List<ApplicationField> Fields { get; set; }

        [JsonProperty(PropertyName = "created_bys")]
        public List<ByLine> CreatedBys { get; set; }

        [JsonProperty(PropertyName = "created_vias")]
        public List<Via> CreatedVias { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }
    }
}
