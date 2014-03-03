using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Response
{
    public class ItemFieldMicro
    {
        [JsonProperty("field_id")]
        public int? FieldId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("config")]
        public FieldConfig Config { get; set; }
    }
}
