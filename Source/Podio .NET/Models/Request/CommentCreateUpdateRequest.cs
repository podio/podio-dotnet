using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class CommentCreateUpdateRequest
    {
        /// <summary>
        ///     The comment to be made.
        /// </summary>
        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        ///     The external id of the comment, if any
        /// </summary>
        [JsonProperty(PropertyName = "external_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalId { get; set; }

        /// <summary>
        ///     Temporary files that have been uploaded and should be attached to this comment.
        /// </summary>
        [JsonProperty(PropertyName = "file_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> FileIds { get; set; }

        /// <summary>
        ///     The id of an embedded link that has been created with the Add an embed operation in the Embed area.
        /// </summary>
        [JsonProperty(PropertyName = "embed_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? EmbedId { get; set; }

        /// <summary>
        ///     The url to be attached.
        /// </summary>
        [JsonProperty(PropertyName = "embed_url", NullValueHandling = NullValueHandling.Ignore)]
        public string EmbedUrl { get; set; }
    }
}