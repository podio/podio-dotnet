using Newtonsoft.Json;
using PodioAPI.Models;

namespace PodioAPI.Utils.Authentication
{
    /// <summary>
    ///     Authenication response form API
    /// </summary>
    public class PodioOAuth
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "ref")]
        public Ref Ref { get; set; }
    }
}