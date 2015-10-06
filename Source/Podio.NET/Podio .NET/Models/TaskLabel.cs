using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class TaskLabel
    {
        [JsonProperty("label_id")]
        public int LabelId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}