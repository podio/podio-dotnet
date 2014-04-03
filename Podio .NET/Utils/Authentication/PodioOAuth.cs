using Newtonsoft.Json;
using PodioAPI.Models;

namespace PodioAPI.Utils
{
    /// <summary>
    /// Authenication response form API
    /// </summary>
    public class PodioOAuth
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; private set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; private set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; private set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; private set; }

        [JsonProperty(PropertyName = "ref")]
        public Ref Ref { get; private set; }
    }
}
