using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Embed
    {
        [JsonProperty("embed_id")]
        public int EmbedId { get; set; }

        [JsonProperty("original_url")]
        public string OriginalUrl { get; set; }

        [JsonProperty("resolved_url")]
        public string ResolvedUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("provider_name")]
        public string ProviderName { get; set; }

        [JsonProperty("embed_html")]
        public string EmbedHtml { get; set; }

        [JsonProperty("embed_height")]
        public int? EmbedHeight { get; set; }

        [JsonProperty("embed_width")]
        public int? EmbedWidth { get; set; }

        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }
    }
}