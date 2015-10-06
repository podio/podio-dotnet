using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class AutoTask
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        ///     Only need to set the user_id property on Application create and update.
        /// </summary>
        [JsonProperty("responsible")]
        public List<User> Responsible { get; set; }
    }
}