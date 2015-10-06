using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class Widget
    {
        [JsonProperty("widget_id")]
        public int? WidgetId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("config")]
        public JObject Config { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("data")]
        public JToken Data { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("ref")]
        public Reference Ref { get; set; }

        [JsonProperty("allowed_refs")]
        public string[] AllowedRefs { get; set; }

        [JsonProperty("cols")]
        public int Cols { get; set; }

        [JsonProperty("rows")]
        public int Rows { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}