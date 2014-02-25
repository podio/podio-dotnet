using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PodioAPI.Models.Response
{
    public class ItemField
    {
        [JsonProperty("field_id")]
        public int FieldId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("values")]
        public List<Dictionary<string,object>> Values { get; set; }

        [JsonProperty("config")]
        public FieldConfig Config { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
