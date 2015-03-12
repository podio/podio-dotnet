using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models.Request
{
    public class AddSpaceMemberRequest
    {
        /// <summary>
        ///     The role of the new users
        /// </summary>
        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

        /// <summary>
        ///     The personalized message to put in the invitation
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     The list of profile ids to invite to the space
        /// </summary>
        [JsonProperty("profiles", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<int> Profiles { get; set; }

        /// <summary>
        ///     The list of users ids to invite
        /// </summary>
        [JsonProperty("users", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<int> Users { get; set; }

        /// <summary>
        ///     The list of mail addresses for new or existing Podio users
        /// </summary>
        [JsonProperty("mails", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Mails { get; set; }

        /// <summary>
        ///     Optionally specify "item" to indicate invite to a specific item
        /// </summary>
        [JsonProperty("context_ref_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContextRefType { get; set; }

        /// <summary>
        ///     Must be set to the item id if source_key is set
        /// </summary>
        [JsonProperty("context_ref_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ContextRefId { get; set; }
    }
}