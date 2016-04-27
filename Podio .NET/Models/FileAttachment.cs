using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class FileAttachment
    {
        [JsonProperty("file_id")]
        public int FileId { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("perma_link")]
        public string PermaLink { get; set; }

        [JsonProperty("thumbnail_link")]
        public string ThumbnailLink { get; set; }

        [JsonProperty("hosted_by")]
        public string HostedBy { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("mimetype")]
        public string MimeType { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("context")]
        public JToken Context { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("rights")]
        public String[] rights { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("replaces")]
        public List<FileAttachment> Replaces { get; set; }
    }
}