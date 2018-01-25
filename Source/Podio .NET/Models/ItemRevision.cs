using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class ItemRevision
    {
        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("item_revision_id")]
        public long? ItemRevisionId { get; set; }

        [JsonProperty("app_revision")]
        public int? AppRevision { get; set; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_via")]
        public Via CreatedVia { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
