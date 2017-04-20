using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace PodioAPI.Utils.Authentication
{
    public abstract class OAuth
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        internal abstract bool IsAuthenticated();
        internal abstract void AddAuthorizationHeader(HttpRequestHeaders headers);
    }
}
