using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PodioAPI.Utils.Authentication
{
    /// <summary>
    ///     ShareFile OAuth
    /// </summary>
    public class ShareFileOAuth : OAuth
    {
        [JsonProperty(PropertyName = "account_id")]
        public string AccountId { get; set; }

        internal override bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(AccountId) && !string.IsNullOrEmpty(AccessToken);
        }

        internal override void AddAuthorizationHeader(HttpRequestHeaders headers)
        {
            headers.Add("X-SF-Authorization", AccountId + " " + AccessToken);
        }
    }
}
