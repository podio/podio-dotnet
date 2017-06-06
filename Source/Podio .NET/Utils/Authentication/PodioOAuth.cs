using System.Net.Http.Headers;
using Newtonsoft.Json;
using PodioAPI.Models;

namespace PodioAPI.Utils.Authentication
{
    /// <summary>
    ///     Authenication response form API
    /// </summary>
    public class PodioOAuth : OAuth
    {
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "ref")]
        public Ref Ref { get; set; }

        internal override bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(AccessToken);
        }

        internal override void AddAuthorizationHeader(HttpRequestHeaders headers)
        {
            headers.Authorization = new AuthenticationHeaderValue("OAuth2", AccessToken);
        }
    }
}