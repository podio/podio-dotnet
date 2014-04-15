using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using PodioAPI.Exceptions;
using PodioAPI.Models.Request;
using PodioAPI.Models;
using PodioAPI.Services;
using PodioAPI.Utils;

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
        /// <para>You can get the Client ID and Client Secret from here: https://developers.podio.com/api-key </para>
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <param name="authStore">If you need to persist the access tokens for a longer period (in your database or whereever), Implement IAuthStore Interface and pass it in. 
        /// <para>By default it takes the SessionStore Implementation (Store access token is session). 
        /// You can use the IsAuthenticated method to check if there is a stored access token already present</para></param>
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

        #region Request Helpers

        internal T Get<T>(string url, Dictionary<string, string> requestData = null, dynamic options = null) where T : new()
        {
            return Request<T>(RequestMethod.GET, url, requestData, options);   
        }

        internal T Post<T>(string url, dynamic requestData = null, dynamic options = null) where T : new()
        {
            return Request<T>(RequestMethod.POST, url, requestData, options);
        }

        internal T Put<T>(string url, dynamic requestData = null, dynamic options = null) where T : new()
        {
            return Request<T>(RequestMethod.PUT, url, requestData);
        }

        internal T Delete<T>(string url, dynamic requestData = null, dynamic options = null) where T : new()
        {
            return Request<T>(RequestMethod.DELETE, url, requestData);
        }

        private T Request<T>(RequestMethod requestMethod, string url, dynamic requestData, dynamic options = null) where T : new()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            var data = new List<string>();
            string httpMethod = string.Empty;
            string originalUrl = url;
            url = this.ApiUrl + url;

            //To use url other than api.podio.com, ex file download from files.podio.com
            if (options != null && options.ContainsKey("url"))
            {
                url = options["url"];
            }

            if (string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientSecret))
            {
                throw new Exception("ClientId and ClientSecret is not set");
            }

            switch (requestMethod.ToString())
            {
                case "GET":
                    httpMethod = "GET";
                    requestHeaders["Content-type"] = "application/x-www-form-urlencoded";
                    if (requestData != null)
                    {
                        string query = EncodeAttributes(requestData);
                        url = url + "?" + query;
                    }
                    requestHeaders["Content-length"] = "0";
                    break;
                case "DELETE":
                    httpMethod = "DELETE";
                    requestHeaders["Content-type"] = "application/x-www-form-urlencoded";
                    if (requestData != null)
                    {
                        string query = EncodeAttributes(requestData);
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
                        requestHeaders["Content-type"] = "application/json";
                        data.Add("post");
                    }
                    break;
                case "PUT":
                    httpMethod = "PUT";
                    requestHeaders["Content-type"] = "application/json";
                    data.Add("put");
                    break;
            }

            if (OAuth != null && !string.IsNullOrEmpty(OAuth.AccessToken))
            {                
                requestHeaders["Authorization"] = "OAuth2 " + OAuth.AccessToken;
                if(options != null && options.ContainsKey("oauth_request") && options["oauth_request"])
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
            ServicePointManager.Expect100Continue = false;
            request.Method = httpMethod;
          
            PodioResponse podioResponse = new PodioResponse();
            var responseHeaders = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            var responseObject = new T();

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
                foreach (string item in data)
                {
                    if (item == "file")
                        AddFileToRequestStream(requestData.filePath, requestData.fileName, request);
                    else if (item == "oauth")
                        WriteToRequestStream(EncodeAttributes(requestData), request);
                    else
                        WriteToRequestStream(requestData, request);
                }
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    podioResponse.Status = (int)((HttpWebResponse)response).StatusCode;
                    foreach (string key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }

                    if (options != null && options.ContainsKey("file_download"))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            var fileResponse = new FileResponse();
                            response.GetResponseStream().CopyTo(memoryStream);
                            fileResponse.FileContents = memoryStream.ToArray();
                            fileResponse.ContentType = response.ContentType;
                            fileResponse.ContentLength = response.ContentLength;
                            return (T)fileResponse.ChangeType<T>();
                        }
                    }
                    else if (options != null && options.ContainsKey("return_raw"))
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            podioResponse.Body = sr.ReadToEnd();
                            return podioResponse.Body;
                        }  
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            podioResponse.Body = sr.ReadToEnd();
                        }  
                    }                                
                    podioResponse.Headers = responseHeaders;           
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    podioResponse.Status = (int)((HttpWebResponse)response).StatusCode;
                    foreach (string key in response.Headers.AllKeys)
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
                podioError = JSONSerializer.Deserilaize<PodioError>(podioResponse.Body);

            switch (podioResponse.Status)
            {
                case 200:
                case 201:
                    responseObject = JSONSerializer.Deserilaize<T>(podioResponse.Body);                   
                    break;
                case 204:
                    responseObject = default(T);
                    break;
                case 400:
                    if (podioError.Error == "invalid_grant")
                    {
                        //Reset auth info
                        OAuth = new PodioOAuth();
                        throw new PodioInvalidGrantException(podioResponse.Status,podioError);
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
                                responseObject = Request<T>(requestMethod, originalUrl, requestData, options);
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
        /// Write an object to request stream.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="request">HttpWebRequest object of which request to write</param>
        internal void WriteToRequestStream(object obj, HttpWebRequest request)
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

        /// <summary>
        /// Convert dictionay to to query string
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        internal static string EncodeAttributes(Dictionary<string, string> attributes)
        {
            var encodedString = string.Empty;
            if(attributes.Any())
            {
                var parameters = new List<string>();
                foreach (var item in attributes)
                {
                    if (item.Key != string.Empty && !string.IsNullOrEmpty(item.Value))
                    {
                        parameters.Add(HttpUtility.UrlEncode(item.Key)+"="+(HttpUtility.UrlEncode(item.Value)));
                    }
                }
                if(parameters.Any())
                    encodedString = string.Join("&", parameters.ToArray());
            }

            return encodedString;
        }

       

       /// <summary>
       /// Add a file to request stream
       /// </summary>
       /// <param name="filePath">Physical path to file</param>
       /// <param name="fileName">File Name</param>
       /// <param name="request">HttpWebRequest object of which request stream file is added to</param>
        private void AddFileToRequestStream(string filePath, string fileName, HttpWebRequest request)
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

                byte[] data = File.ReadAllBytes(filePath);
                string mimeType = MimeTypeMapping.GetMimeType(Path.GetExtension(filePath));
               
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

        #region Authentication
        /// <summary>Authenticate as an App (with AppId and AppSecret).
        /// <para>Podio API Reference: https://developers.podio.com/authentication/username_password </para>
        /// </summary> 
        public PodioOAuth AuthenicateWithApp(int appId, string appToken)
        {
            var authRequest = new Dictionary<string, string>(){
                   {"app_id", appId.ToString()},
                   {"app_token", appToken},
                   {"grant_type", "app"}
                };
            return Authenticate("app", authRequest);
        }

        /// <summary> Authenticate with username and password 
        /// <para>suitable in situations where you only need data from a single app and do not wish authenticate as a specific user</para>
        /// <para>Podio API Reference: https://developers.podio.com/authentication/username_password </para>
        /// </summary> 
        public PodioOAuth AuthenicateWithPassword(string username, string password)
        {
            var authRequest = new Dictionary<string, string>(){
                   {"username", username},
                   {"password", password},
                   {"grant_type", "password"}
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
                   {"redirect_uri", redirectUri},
                   {"grant_type", "authorization_code"}
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
                   {"refresh_token", OAuth.RefreshToken},
                   {"grant_type", "refresh_token"}
                };
            return Authenticate("refresh_token", authRequest);
        }

        private PodioOAuth Authenticate(string grantType, Dictionary<string, string> attributes)
        {
            attributes["client_id"] = ClientId;
            attributes["client_secret"] = ClientSecret;

            var options = new Dictionary<string, object>(){
                {"oauth_request",true}
            };

            PodioOAuth podioOAuth = Post<PodioOAuth>("/oauth/token", attributes, options);
            OAuth = podioOAuth;
            AuthStore.Set(podioOAuth);

            return podioOAuth;
        }

        /// <summary>
        /// Constructs the full url to Podio's authorization endpoint (To get AuthorizationCode in server-side flow)
        /// </summary>
        /// <param name="redirectUri">The redirectUri must be on the same domain as the domain you specified when you applied for your API Key</param>
        /// <returns></returns>
        public string GetAuthorizeUrl(string redirectUri)
        {
            string authorizeUrl = "https://podio.com/oauth/authorize?response_type=code&client_id={0}&redirect_uri={1}";
            return String.Format(authorizeUrl, this.ClientId, HttpUtility.UrlEncode(redirectUri));
        }

        /// <summary>
        /// Check if there is a stored access token already present in AuthStore.
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return (this.OAuth != null && !string.IsNullOrEmpty(this.OAuth.AccessToken));
        }

        /// <summary>
        /// Clear access token from AuthStore
        /// </summary>
        public void ClearAuth()
        {
            AuthStore.Clear();
        }
        #endregion

        #region Services

        /// <summary>
        /// Provies all API methods in Item Area
        /// <para>Podio API Reference: https://developers.podio.com/doc/items </para>
        /// </summary>
        public ItemService ItemService
        {
            get { return new ItemService(this); }
        }

        /// <summary>
        /// Provies all API methods in Files Area
        /// <para>Podio API Reference: https://developers.podio.com/doc/files </para>
        /// </summary>
        public FileService FileService
        {
            get { return new FileService(this); }
        }

        /// <summary>
        /// Provies all API methods in Embed Area
        /// <para>https://developers.podio.com/doc/embeds</para>
        /// </summary>
        public EmbedService EmbedService
        {
            get { return new EmbedService(this); }
        }

        /// <summary>
        /// Provies all API methods in Embed Area
        /// <para>https://developers.podio.com/doc/applications</para>
        /// </summary>
        public ApplicationService ApplicationService
        {
            get { return new ApplicationService(this); }
        }

        /// <summary>
        /// Provies all API methods in Tasks Area
        /// <para>https://developers.podio.com/doc/tasks</para>
        /// </summary>
        public TaskService TaskService
        {
            get { return new TaskService(this); }
        }

        /// <summary>
        /// Provies all API methods in Status Area
        /// <para>https://developers.podio.com/doc/status</para>
        /// </summary>
        public StatusService StatusService
        {
            get { return new StatusService(this); }
        }

        /// <summary>
        /// Provies all API methods in Contact Area
        /// <para>https://developers.podio.com/doc/contacts</para>
        /// </summary>
        public ContactService ContactService
        {
            get { return new ContactService(this); }
        }

        /// <summary>
        /// Provies all API methods in Hook Area
        /// <para> https://developers.podio.com/doc/hooks </para>
        /// </summary>
        public HookService HookService
        {
            get { return new HookService(this); }
        }

        /// <summary>
        /// Provies all API methods in Hook Area
        /// <para> https://developers.podio.com/doc/hooks </para>
        /// </summary>
        public CommentService CommentService
        {
            get { return new CommentService(this); }
        }

        /// <summary>
        /// Provies all API methods in Organization Area
        /// <para> https://developers.podio.com/doc/organizations </para>
        /// </summary>
        public OrganizationService OrganizationService
        {
            get { return new OrganizationService(this); }
        }

        /// <summary>
        /// Provies all API methods in Space Area
        /// <para> https://developers.podio.com/doc/spaces </para>
        /// </summary>
        public SpaceService SpaceService
        {
            get { return new SpaceService(this); }
        }

        /// <summary>
        /// Provies all API methods in SpaceMember Area
        /// <para> https://developers.podio.com/doc/space-members </para>
        /// </summary>
        public SpaceMembersService SpaceMembersService
        {
            get { return new SpaceMembersService(this); }
        }
        #endregion
    }
    public enum RequestMethod
    {
        GET, POST, PUT, DELETE
    }
}
