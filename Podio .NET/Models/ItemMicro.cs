using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ItemMicro
    {
        [JsonProperty("app_item_id")]
        public int AppItemId { get; set; }

        [JsonProperty("item_id")]
        public int ItemId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }
    }
}