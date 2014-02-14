using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodioAPI.Utils;
using System.Net;
using System.IO;
using System.Web;
using PodioAPI.Services;
using PodioAPI.Exceptions;
using PodioAPI.Models.Response;
using PodioAPI.Models.Request;

namespace PodioAPI
{
    public class Podio
    {
        protected string ClientId { get; private set; }
        protected string ClientSecret { get; private set; }
        public PodioOAuth OAuth { get; private set; }
        private IAuthStore AuthStore { get; set; }
        public int RateLimit { get; private set; }
        public int RateLimitRemaining { get; private set; }
        protected string ApiUrl { get; private set; }

        /// <summary>
        /// Initialize the podio class with Client ID and Client Secret
        /// <para>Get the Client ID and Client Secret from here: https://developers.podio.com/api-key </para>
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <param name="debug"></param>
        /// <param name="authStore"></param>
        public Podio(string clientId, string clientSecret, IAuthStore authStore = null)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            ApiUrl = "https://api.podio.com:443";

            if (authStore != null)
                AuthStore = authStore;
            else
                AuthStore = new SessionStore();

            OAuth = AuthStore.Get();
        }

        #region Authentication
        /// <summary> Authenticate with username and password 
        /// <para>Podio API Reference: https://developers.podio.com/authentication/username_password </para>
        /// </summary> 
        public PodioOAuth AuthenicateWithApp(int appId, string appToken)
        {
            var authRequest = new Dictionary<string, string>(){
                   {"app_id", appId.ToString()},
                   {"app_token", appToken}
                };
            return Authenticate("app", authRequest);
        }

        /// <summary> Authenticate as an App (with AppId and AppSecret).
        /// <para>suitable in situations where you only need data from a single app and do not wish authenticate as a specific user</para>
        /// <para>Podio API Reference: https://developers.podio.com/authentication/username_password </para>
        /// </summary> 
        public PodioOAuth AuthenicateWithPassword(string username, string password)
        {
            var authRequest = new Dictionary<string, string>(){
                   {"username", username},
                   {"password", password}
                };
            return Authenticate("password", authRequest);
        }

        /// <summary> Authenticate with an authorization code
        /// <para>Suitable in situations where you only need data from a single app and do not wish authenticate as a specific user</para>
        /// <para>Podio API Reference: https://developers.podio.com/authentication/server_side </para>
        /// </summary> 
        public PodioOAuth AuthenicateWithAuthorizationCode(string authorizationCode, string redirectUri)
        {
            var authRequest = new Dictionary<string, string>(){
                   {"code", authorizationCode},
                   {"redirect_uri", redirectUri}
                };
            return Authenticate("authorization_code", authRequest);
        }

        /// <summary> Refresh the Access Token.
        /// <para>When the access token expires, you can use this method to refresh your access, and gain another access_token</para>
        /// <para>Podio API Reference: https://developers.podio.com/authentication </para>
        /// </summary> 
        public PodioOAuth RefreshAccessToken()
        {
            var authRequest = new Dictionary<string, string>(){
                   {"refresh_token", OAuth.RefreshToken}
                };
            return Authenticate("refresh_token", authRequest);
        }

        private PodioOAuth Authenticate(string grantType, Dictionary<string, string> attributes)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            switch (grantType)
            {
                case "password":
                    data["grant_type"] = "password";
                    data["username"] = attributes["username"];
                    data["password"] = attributes["password"];
                    break;
                case "refresh_token":
                    data["grant_type"] = "refresh_token";
                    data["refresh_token"] = attributes["refresh_token"];
                    break;
                case "authorization_code":
                    data["grant_type"] = "authorization_code";
                    data["code"] = attributes["code"];
                    data["redirect_uri"] = "redirect_uri";
                    break;
                case "app":
                    data["grant_type"] = "app";
                    data["app_id"] = attributes["app_id"];
                    data["app_token"] = attributes["app_token"];
                    break;
            }
            data["client_id"] = ClientId;
            data["client_secret"] = ClientSecret;

            Dictionary<string, object> options = new Dictionary<string, object>(){
                {"oauth_request",true}
            };

            PodioOAuth podioOAuth = (PodioOAuth)Post<PodioOAuth>("/oauth/token", data, options);
            OAuth = podioOAuth;
            AuthStore.Set(podioOAuth);
            return podioOAuth;
        }

        /// <summary>
        /// Constructs the full url to Podio's authorization endpoint (To get AuthorizationCode in server-side flow)
        /// <para>The redirectUri must be on the same domain as the domain you specified when you applied for your API Key</para>
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public string GetAuthorizeUrl(string redirectUri)
        {
            string authorizeUrl = "https://podio.com/oauth/authorize?response_type=code&client_id={0}&redirect_uri={1}";
            return String.Format(authorizeUrl, this.ClientId, HttpUtility.UrlEncode(redirectUri));
        }

        public bool IsAuthenticated()
        {
            return (this.OAuth != null && !string.IsNullOrEmpty(this.OAuth.AccessToken));
        }
        #endregion

        #region Request Helper

        internal object Get<T>(string url, dynamic attributes = null, dynamic options = null)
        {
            return Request<T>(RequestMethod.GET, url, attributes, options);
        }

        internal object Post<T>(string url, dynamic attributes = null, dynamic options = null)
        {
            return Request<T>(RequestMethod.POST, url, attributes, options);
        }

        internal object Put<T>(string url, dynamic attributes = null, dynamic options = null)
        {
            return Request<T>(RequestMethod.PUT, url, attributes);
        }

        internal object Delete<T>(string url, dynamic attributes = null, dynamic options = null)
        {
            return Request<T>(RequestMethod.DELETE, url, attributes);
        }

        private object Request<T>(RequestMethod requestMethod, string url, dynamic attributes, dynamic options = null)
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            var data = new List<string>();
            string httpMethod = "GET";
            string originalUrl = this.ApiUrl + url;
            url = this.ApiUrl + url;

            if (string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientSecret))
            {
                throw new Exception("ClientId and ClientSecret is not set");
            }

            switch (requestMethod.ToString())
            {
                case "GET":
                    httpMethod = "GET";
                    requestHeaders["Content-type"] = "application/x-www-form-urlencoded";
                    if (attributes != null)
                    {
                        var query = EncodeAttributes(attributes);
                        url = url + "?" + query;
                    }
                    requestHeaders["Content-length"] = "0";
                    break;
                case "DELETE":
                    httpMethod = "DELETE";
                    requestHeaders["Content-type"] = "application/x-www-form-urlencoded";
                    if (attributes != null)
                    {
                        var query = EncodeAttributes(attributes);
                        url = url + "?" + query;
                    }
                    requestHeaders["Content-length"] = "0";
                    break;
                case "POST":
                    httpMethod = "POST";
                    if (options != null && options.ContainsKey("upload") && options["upload"])
                    {
                        requestHeaders["Content-type"] = "multipart/form-data";
                        data.Add("file");
                    }
                    else if (options != null && options.ContainsKey("oauth_request") && options["oauth_request"])
                    {
                        data.Add("oauth");
                        requestHeaders["Content-type"] = "application/x-www-form-urlencoded";
                    }
                    else
                    {
                        data.Add("post");
                        requestHeaders["Content-type"] = "application/json";
                    }
                    break;
                case "PUT":
                    httpMethod = "PUT";
                    data.Add("put");
                    requestHeaders["Content-type"] = "application/json";
                    break;
            }

            if (OAuth != null && !string.IsNullOrEmpty(OAuth.AccessToken))
            {
                requestHeaders["Authorization"] = "OAuth2 " + OAuth.AccessToken;
                if (options != null && options.ContainsKey("oauth_request") && options["oauth_request"])
                {
                    requestHeaders.Remove("Authorization");
                }
            }
            else
            {
                requestHeaders.Remove("Authorization");
            }

            if (options != null && options.ContainsKey("file_download") && options["file_download"])
                requestHeaders["Accept"] = "*/*";
            else
                requestHeaders["Accept"] = "application/json";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = httpMethod;

            PodioResponse podioResponse = new PodioResponse();
            Dictionary<string, string> responseHeaders = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            object responseObject = new object();

            if (requestHeaders.Any())
            {
                if (requestHeaders.ContainsKey("Accept"))
                    request.Accept = requestHeaders["Accept"];
                if (requestHeaders.ContainsKey("Content-type"))
                    request.ContentType = requestHeaders["Content-type"];
                if (requestHeaders.ContainsKey("Content-length"))
                    request.ContentType = requestHeaders["Content-length"];
                if (requestHeaders.ContainsKey("Authorization"))
                    request.Headers.Add("Authorization", requestHeaders["Authorization"]);
            }
            if (data.Any())
            {
                foreach (var item in data)
                {
                    if (item == "file")
                        AddFileToRequestStream(attributes.filePath, attributes.fileName, request);
                    else if (item == "oauth")
                        WriteToRequestStream(EncodeAttributes(attributes), request);
                    else
                        WriteToRequestStream(attributes, request);
                }
            }

            try
            {
                using (var response = request.GetResponse())
                {
                    podioResponse.Status = (int)((HttpWebResponse)response).StatusCode;
                    foreach (var key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        podioResponse.Body = sr.ReadToEnd();
                    }
                    podioResponse.Headers = responseHeaders;
                }
            }
            catch (WebException e)
            {
                using (var response = e.Response)
                {
                    podioResponse.Status = (int)((HttpWebResponse)response).StatusCode;
                    foreach (var key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        podioResponse.Body = sr.ReadToEnd();
                    }
                    podioResponse.Headers = responseHeaders;
                }
            }

            if (podioResponse.Headers.ContainsKey("X-Rate-Limit-Remaining"))
                RateLimitRemaining = int.Parse(podioResponse.Headers["X-Rate-Limit-Remaining"]);
            if (podioResponse.Headers.ContainsKey("X-Rate-Limit-Limit"))
                RateLimit = int.Parse(podioResponse.Headers["X-Rate-Limit-Limit"]);

            PodioError podioError = new PodioError();
            if (podioResponse.Status >= 400)
                podioError = (PodioError)JSONSerializer.Deserilaize<PodioError>(podioResponse.Body);

            switch (podioResponse.Status)
            {
                case 200:
                case 201:
                case 204:
                    responseObject = JSONSerializer.Deserilaize<T>(podioResponse.Body);
                    break;
                case 400:
                    if (podioError.Error == "invalid_grant")
                    {
                        //Reset auth info
                        OAuth = new PodioOAuth();
                        throw new PodioInvalidGrantException(podioResponse.Status, podioError);
                    }
                    else
                    {
                        throw new PodioBadRequestException(podioResponse.Status, podioError);
                    }
                case 401:
                    if (podioError.ErrorDescription == "expired_token" || podioError.Error == "invalid_token")
                    {
                        if (!string.IsNullOrEmpty(OAuth.RefreshToken))
                        {
                            //Refresh access token
                            var authInfo = RefreshAccessToken();
                            if (authInfo != null && !string.IsNullOrEmpty(authInfo.AccessToken))
                                Request<T>(requestMethod, originalUrl, attributes, options);
                        }
                        else
                        {
                            throw new PodioAuthorizationException(podioResponse.Status, podioError);
                        }
                    }
                    break;
                case 403:
                    throw new PodioForbiddenError(podioResponse.Status, podioError);
                case 404:
                    throw new PodioNotFoundError(podioResponse.Status, podioError);
                case 409:
                    throw new PodioConflictError(podioResponse.Status, podioError);
                case 410:
                    throw new PodioGoneError(podioResponse.Status, podioError);
                case 420:
                    throw new PodioRateLimitError(podioResponse.Status, podioError);
                case 500:
                    throw new PodioServerError(podioResponse.Status, podioError);
                case 502:
                case 503:
                case 504:
                    throw new PodioUnavailableError(podioResponse.Status, podioError);
                default:
                    throw new PodioException(podioResponse.Status, podioError);
            }

            return responseObject;
        }

        /// <summary>
        /// Transform options object to query parameteres
        /// </summary>
        /// <param name="url"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal string PrepareUrlWithOptions(string url, CreateUpdateOptions options)
        {
            string urlWithOptions = "";
            List<string> parameters = new List<string>();
            if (options.Silent)
                parameters.Add("silent=1");
            if (!options.Hook)
                parameters.Add("hook=false");
            if (options.Fields != null && options.Fields.Any())
                parameters.Add(string.Join(",", options.Fields.Select(s => s).ToArray()));

            urlWithOptions = parameters.Any() ? url + "?" + string.Join("&", parameters.ToArray()) : url;
            return urlWithOptions;
        }

        /// <summary>
        /// Write an object to request stream
        /// <para>If object is string write it directly. else serialize the object to JSON and writes to current request stream </para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="request">HttpWebRequest object of which request to write</param>
        internal static void WriteToRequestStream(object obj, HttpWebRequest request)
        {
            if (obj != null)
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    if (obj is string)
                        streamWriter.Write(obj);
                    else
                        streamWriter.Write(JSONSerializer.Serilaize(obj));
                }
            }
        }

        internal static string EncodeAttributes(Dictionary<string, string> attributes)
        {
            if (attributes.Any())
            {
                return string.Join("&", attributes.Select(x => x.Key != "" ? HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value) : HttpUtility.UrlEncode(x.Value)).ToArray());
            }
            return String.Empty;
        }

        /// <summary>
        /// Add a file to request stream
        /// </summary>
        /// <param name="filePath">Physical path to file</param>
        /// <param name="fileName">File Name</param>
        /// <param name="request">HttpWebRequest object of which request stream file is added to</param>
        private static void AddFileToRequestStream(string filePath, string fileName, HttpWebRequest request)
        {
            byte[] inputData;
            string contentType = "";
            request.ServicePoint.Expect100Continue = false;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                string boundary = String.Format("----------{0:N}", Guid.NewGuid());
                contentType = "multipart/form-data; boundary=" + boundary;
                MemoryStream ms = new MemoryStream();

                ms.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));

                string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    "filename",
                    fileName);
                ms.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));

                var data = File.ReadAllBytes(filePath);
                var mimeType = MimeTypeMapping.GetMimeType(Path.GetExtension(filePath));

                ms.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));

                string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                   boundary,
                   "source",
                   fileName,
                   mimeType);
                ms.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
                ms.Write(data, 0, data.Length);

                string footer = "\r\n--" + boundary + "--\r\n";

                ms.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));
                inputData = ms.ToArray();

            }
            else
            {
                throw new FileNotFoundException("File not found in the specified path");
            }

            request.ContentType = contentType;
            request.ContentLength = inputData.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(inputData, 0, inputData.Length);
            }

        }

        #endregion

        #region Services

        /// <summary>
        /// Provies all API methods in Item Area
        /// <para>Podio API documentation: https://developers.podio.com/doc/items </para>
        /// </summary>
        public ItemService ItemService
        {
            get { return new ItemService(this); }
        }
        #endregion
    }
    public enum RequestMethod
    {
        GET, POST, PUT, DELETE
    }
}
