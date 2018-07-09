using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using PodioAPI.Exceptions;
using PodioAPI.Models;
using PodioAPI.Models.Request;
using PodioAPI.Services;
using PodioAPI.Utils;
using PodioAPI.Utils.Authentication;
using Newtonsoft.Json;

namespace PodioAPI
{
    public class Podio
    {
        protected string ClientId { get; set; }
        protected string ClientSecret { get; set; }
        public PodioOAuth OAuth { get; set; }
        public IAuthStore AuthStore { get; set; }
        private WebProxy Proxy { get; set; }
        public int RateLimit { get; private set; }
        public int RateLimitRemaining { get; private set; }
        protected string ApiUrl { get; set; }

        /// <summary>
        ///     Initialize the podio class with Client ID and Client Secret
        ///     <para>You can get the Client ID and Client Secret from here: https://developers.podio.com/api-key </para>
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <param name="authStore">
        ///     If you need to persist the access tokens for a longer period (in your session, database or whereever), Implement
        ///     PodioAPI.Utils.IAuthStore Interface and pass it in.
        ///     <para> You can use the IsAuthenticated method to check if there is a stored access token already present</para>
        /// </param>
        /// <param name="proxy">To set proxy to HttpWebRequest</param>
        public Podio(string clientId, string clientSecret, IAuthStore authStore = null, WebProxy proxy = null)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            ApiUrl = "https://api.podio.com:443";
            Proxy = proxy;

            AuthStore = authStore ?? new NullAuthStore();
            OAuth = AuthStore.Get();
        }

        #region Request Helpers

        internal T Get<T>(string url, Dictionary<string, string> requestData = null, dynamic options = null)
            where T : new()
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

        private T Request<T>(RequestMethod requestMethod, string url, dynamic requestData, dynamic options = null)
            where T : new()
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
                    else if (options != null && options.ContainsKey("byteUpload") && options["byteUpload"])
                    {
                        requestHeaders["Content-type"] = "multipart/form-data";
                        data.Add("fileByteUpload");
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

            var request = (HttpWebRequest) WebRequest.Create(url);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            request.Proxy = this.Proxy;
            request.Method = httpMethod;
            request.UserAgent = "Podio Dotnet Client";

            var podioResponse = new PodioResponse();
            var responseHeaders = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            var responseObject = new T();

            if (requestHeaders.Any())
            {
                if (requestHeaders.ContainsKey("Accept"))
                    request.Accept = requestHeaders["Accept"];
                if (requestHeaders.ContainsKey("Content-type"))
                    request.ContentType = requestHeaders["Content-type"];
                if (requestHeaders.ContainsKey("Content-length"))
                    request.ContentLength = int.Parse(requestHeaders["Content-length"]);
                if (requestHeaders.ContainsKey("Authorization"))
                    request.Headers.Add("Authorization", requestHeaders["Authorization"]);
            }
            if (data.Any())
            {
                foreach (string item in data)
                {
                    if (item == "file")
                        AddFileToRequestStream(requestData.filePath, requestData.fileName, request);
                    else if(item == "fileByteUpload")
                        AddFileToRequestStream(requestData.fileName, requestData.data, requestData.mimeType, request);
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
                    podioResponse.Status = (int) ((HttpWebResponse) response).StatusCode;
                    foreach (string key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }

                    podioResponse.Headers = responseHeaders;
                    SetRateLimitInfo(podioResponse);

                    if (options != null && options.ContainsKey("file_download"))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            var fileResponse = new FileResponse();
                            response.GetResponseStream().CopyTo(memoryStream);
                            fileResponse.FileContents = memoryStream.ToArray();
                            fileResponse.ContentType = response.ContentType;
                            fileResponse.ContentLength = response.ContentLength;
                            return (T) fileResponse.ChangeType<T>();
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
                   
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    podioResponse.Status = (int) ((HttpWebResponse) response).StatusCode;
                    foreach (string key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }
                    podioResponse.Headers = responseHeaders;
                    SetRateLimitInfo(podioResponse);

                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        podioResponse.Body = sr.ReadToEnd();
                    }
                }
            }


            var podioError = new PodioError();
            if (podioResponse.Status >= 400)
            {
                try
                {
                    podioError = JSONSerializer.Deserilaize<PodioError>(podioResponse.Body);
                }
                catch (JsonException ex)
                {
                    throw new PodioInvalidJsonException(podioResponse.Status, new PodioError
                    {
                        Error = "Error response is not a valid Json string.",
                        ErrorDescription = ex.ToString(),
                        ErrorDetail = podioResponse.Body
                    });
                }
            }

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
                                responseObject = Request<T>(requestMethod, originalUrl, requestData, options);
                        }
                        else
                        {
                            throw new PodioAuthorizationException(podioResponse.Status, podioError);
                        }
                    }
                    break;
                case 403:
                    throw new PodioForbiddenException(podioResponse.Status, podioError);
                case 404:
                    throw new PodioNotFoundException(podioResponse.Status, podioError);
                case 409:
                    throw new PodioConflictException(podioResponse.Status, podioError);
                case 410:
                    throw new PodioGoneException(podioResponse.Status, podioError);
                case 420:
                    throw new PodioRateLimitException(podioResponse.Status, podioError);
                case 500:
                    throw new PodioServerException(podioResponse.Status, podioError);
                case 502:
                case 503:
                case 504:
                    throw new PodioUnavailableException(podioResponse.Status, podioError);
                default:
                    throw new PodioException(podioResponse.Status, podioError);
            }

            return responseObject;
        }

        /// <summary>
        ///     Transform options object to query parameteres
        /// </summary>
        /// <param name="url"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal static string PrepareUrlWithOptions(string url, CreateUpdateOptions options)
        {
            var urlWithOptions = string.Empty;
            var parameters = new List<string>();
            if (options.Silent)
                parameters.Add("silent=true");
            if (!options.Hook)
                parameters.Add("hook=false");
            if (options.AlertInvite)
                parameters.Add("alert_invite=true");
            if (options.Fields != null && options.Fields.Any())
                parameters.Add(string.Join(",", options.Fields.Select(s => s).ToArray()));

            urlWithOptions = parameters.Any() ? url + "?" + string.Join("&", parameters.ToArray()) : url;
            return urlWithOptions;
        }

        /// <summary>
        ///     Write an object to request stream.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="request">HttpWebRequest object of which request to write</param>
        internal static void WriteToRequestStream(object obj, HttpWebRequest request)
        {
            if (obj != null)
            {
                try
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        if (obj is string)
                            streamWriter.Write(obj);
                        else
                            streamWriter.Write(JSONSerializer.Serilaize(obj));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        ///     Convert dictionay to to query string
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        internal static string EncodeAttributes(Dictionary<string, string> attributes)
        {
            var encodedString = string.Empty;
            if (attributes != null && attributes.Any())
            {
                var parameters = new List<string>();
                foreach (var item in attributes)
                {
                    if (item.Key != string.Empty && !string.IsNullOrEmpty(item.Value))
                    {
                        parameters.Add(HttpUtility.UrlEncode(item.Key) + "=" + (HttpUtility.UrlEncode(item.Value)));
                    }
                }
                if (parameters.Any())
                    encodedString = string.Join("&", parameters.ToArray());
            }

            return encodedString;
        }

        private void SetRateLimitInfo(PodioResponse podioResponse)
        {
            if (podioResponse.Headers.ContainsKey("X-Rate-Limit-Remaining"))
                RateLimitRemaining = int.Parse(podioResponse.Headers["X-Rate-Limit-Remaining"]);
            if (podioResponse.Headers.ContainsKey("X-Rate-Limit-Limit"))
                RateLimit = int.Parse(podioResponse.Headers["X-Rate-Limit-Limit"]);
        }


        /// <summary>
        ///     Add a file to request stream
        /// </summary>
        /// <param name="filePath">Physical path to file</param>
        /// <param name="fileName">File Name</param>
        /// <param name="request">HttpWebRequest object of which request stream file is added to</param>
        private static void AddFileToRequestStream(string filePath, string fileName, HttpWebRequest request)
        {
            byte[] inputData;
            string boundary = String.Format("----------{0:N}", Guid.NewGuid());
            var contentType = "multipart/form-data; boundary=" + boundary;

            request.ServicePoint.Expect100Continue = false;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                byte[] data = File.ReadAllBytes(filePath);
                string mimeType = MimeTypeMapping.GetMimeType(Path.GetExtension(filePath));

                inputData = PrepareFileInput(fileName, data, mimeType, boundary);
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

        private static void AddFileToRequestStream(string fileName, byte[] data, string mimeType, HttpWebRequest request)
        {
            byte[] inputData;
            string boundary = String.Format("----------{0:N}", Guid.NewGuid());
            var contentType = "multipart/form-data; boundary=" + boundary;

            inputData = PrepareFileInput(fileName, data, mimeType, boundary);

            request.ContentType = contentType;
            request.ContentLength = inputData.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(inputData, 0, inputData.Length);
            }
        }

        private static byte[] PrepareFileInput(string fileName, byte[] data, string mimeType, string boundary)
        {
            var memoryStream = new MemoryStream();
            byte[] inputData;

            memoryStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));

            string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                boundary,
                "filename",
                fileName);
            memoryStream.Write(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));

            memoryStream.Write(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));

            string header =
                string.Format(
                    "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                    boundary,
                    "source",
                    fileName,
                    mimeType);
            memoryStream.Write(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
            memoryStream.Write(data, 0, data.Length);

            string footer = "\r\n--" + boundary + "--\r\n";

            memoryStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));
            inputData = memoryStream.ToArray();
            return inputData;
        }

        #endregion

        #region Authentication

        /// <summary>
        ///     Authenticate as an App (with AppId and AppSecret)
        ///     <para>Podio API Reference: https://developers.podio.com/authentication/app_auth </para>
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="appToken">AppToken</param>
        /// <returns>PodioOAuth object with OAuth data</returns>
        public PodioOAuth AuthenticateWithApp(int appId, string appToken)
        {
            var authRequest = new Dictionary<string, string>
            {
                {"app_id", appId.ToString()},
                {"app_token", appToken},
                {"grant_type", "app"}
            };
            return Authenticate("app", authRequest);
        }

        /// <summary>
        ///     Authenticate with username and password
        ///     <para>Podio API Reference: https://developers.podio.com/authentication/username_password </para>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>PodioOAuth object with OAuth data</returns>
        public PodioOAuth AuthenticateWithPassword(string username, string password)
        {
            var authRequest = new Dictionary<string, string>
            {
                {"username", username},
                {"password", password},
                {"grant_type", "password"}
            };
            return Authenticate("password", authRequest);
        }

        /// <summary>
        ///     Authenticate with an authorization code
        ///     <para>Podio API Reference: https://developers.podio.com/authentication/server_side </para>
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <param name="redirectUri"></param>
        /// <returns>PodioOAuth object with OAuth data</returns>
        public PodioOAuth AuthenticateWithAuthorizationCode(string authorizationCode, string redirectUri)
        {
            var authRequest = new Dictionary<string, string>
            {
                {"code", authorizationCode},
                {"redirect_uri", redirectUri},
                {"grant_type", "authorization_code"}
            };
            return Authenticate("authorization_code", authRequest);
        }

        /// <summary>
        ///     Refresh the Access Token.
        ///     <para>When the access token expires, you can use this method to refresh your access, and gain another access_token</para>
        ///     <para>Podio API Reference: https://developers.podio.com/authentication </para>
        /// </summary>
        /// <returns>PodioOAuth object with OAuth data</returns>
        public PodioOAuth RefreshAccessToken()
        {
            var authRequest = new Dictionary<string, string>
            {
                {"refresh_token", OAuth.RefreshToken},
                {"grant_type", "refresh_token"}
            };
            return Authenticate("refresh_token", authRequest);
        }

        private PodioOAuth Authenticate(string grantType, Dictionary<string, string> attributes)
        {
            attributes["client_id"] = ClientId;
            attributes["client_secret"] = ClientSecret;

            var options = new Dictionary<string, object>
            {
                {"oauth_request", true}
            };

            PodioOAuth podioOAuth = Post<PodioOAuth>("/oauth/token", attributes, options);
            this.OAuth = podioOAuth;
            AuthStore.Set(podioOAuth);

            return podioOAuth;
        }

        /// <summary>
        ///     Constructs the full url to Podio's authorization endpoint (To get AuthorizationCode in server-side flow)
        /// </summary>
        /// <param name="redirectUri">
        ///     The redirectUri must be on the same domain as the domain you specified when you applied for
        ///     your API Key
        /// </param>
        /// <returns></returns>
        public string GetAuthorizeUrl(string redirectUri)
        {
            string authorizeUrl = "https://podio.com/oauth/authorize?response_type=code&client_id={0}&redirect_uri={1}";
            return String.Format(authorizeUrl, this.ClientId, HttpUtility.UrlEncode(redirectUri));
        }

        /// <summary>
        ///     Check if there is a stored access token already present.
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return (this.OAuth != null && !string.IsNullOrEmpty(this.OAuth.AccessToken));
        }

        #endregion

        #region Services

        /// <summary>
        ///     Provies all API methods in Item Area
        ///     <para>Podio API Reference: https://developers.podio.com/doc/items </para>
        /// </summary>
        public ItemService ItemService
        {
            get { return new ItemService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Files Area
        ///     <para>Podio API Reference: https://developers.podio.com/doc/files </para>
        /// </summary>
        public FileService FileService
        {
            get { return new FileService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Embed Area
        ///     <para>https://developers.podio.com/doc/embeds</para>
        /// </summary>
        public EmbedService EmbedService
        {
            get { return new EmbedService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Embed Area
        ///     <para>https://developers.podio.com/doc/applications</para>
        /// </summary>
        public ApplicationService ApplicationService
        {
            get { return new ApplicationService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Tasks Area
        ///     <para>https://developers.podio.com/doc/tasks</para>
        /// </summary>
        public TaskService TaskService
        {
            get { return new TaskService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Status Area
        ///     <para>https://developers.podio.com/doc/status</para>
        /// </summary>
        public StatusService StatusService
        {
            get { return new StatusService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Contact Area
        ///     <para>https://developers.podio.com/doc/contacts</para>
        /// </summary>
        public ContactService ContactService
        {
            get { return new ContactService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Hook Area
        ///     <para> https://developers.podio.com/doc/hooks </para>
        /// </summary>
        public HookService HookService
        {
            get { return new HookService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Hook Area
        ///     <para> https://developers.podio.com/doc/hooks </para>
        /// </summary>
        public CommentService CommentService
        {
            get { return new CommentService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Organization Area
        ///     <para> https://developers.podio.com/doc/organizations </para>
        /// </summary>
        public OrganizationService OrganizationService
        {
            get { return new OrganizationService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Space Area
        ///     <para> https://developers.podio.com/doc/spaces </para>
        /// </summary>
        public SpaceService SpaceService
        {
            get { return new SpaceService(this); }
        }

        /// <summary>
        ///     Provies all API methods in SpaceMember Area
        ///     <para> https://developers.podio.com/doc/space-members </para>
        /// </summary>
        public SpaceMembersService SpaceMembersService
        {
            get { return new SpaceMembersService(this); }
        }

        /// <summary>
        ///     Provies all API methods in  Widgets Area
        ///     <para> https://developers.podio.com/doc/widgets </para>
        /// </summary>
        public WidgetService WidgetService
        {
            get { return new WidgetService(this); }
        }

        /// <summary>
        ///     Provies API methods in Stream Area
        ///     <para> https://developers.podio.com/doc/stream </para>
        /// </summary>
        public StreamService StreamService
        {
            get { return new StreamService(this); }
        }

        /// <summary>
        ///     Provies all API methods in  Reference Area
        ///     <para> https://developers.podio.com/doc/reference </para>
        /// </summary>
        public ReferenceService ReferenceService
        {
            get { return new ReferenceService(this); }
        }

        /// Provies all API methods in Grants area
        /// <para> https://developers.nextpodio.dk/doc/grants </para>
        /// </summary>
        public GrantService GrantService
        {
            get { return new GrantService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Search area
        ///     <para> https://developers.podio.com/doc/search </para>
        /// </summary>
        public SearchService SearchService
        {
            get { return new SearchService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Rating Area
        ///     <para> https://developers.podio.com/doc/ratings </para>
        /// </summary>
        public RatingService RatingService
        {
            get { return new RatingService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Tag Area
        ///     <para> https://developers.podio.com/doc/tags </para>
        /// </summary>
        public TagService TagService
        {
            get { return new TagService(this); }
        }

        /// Provies all API methods in Batch area
        /// <para> https://developers.podio.com/doc/batch </para>
        /// </summary>
        public BatchService BatchService
        {
            get { return new BatchService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Actions area
        ///     <para> https://developers.podio.com/doc/actions </para>
        /// </summary>
        public ActionService ActionService
        {
            get { return new ActionService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Calendar Area
        ///     <para> https://developers.podio.com/doc/calendar </para>
        /// </summary>
        public CalendarService CalendarService
        {
            get { return new CalendarService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Conversations area
        ///     <para> https://developers.podio.com/doc/conversations </para>
        /// </summary>
        public ConversationService ConversationService
        {
            get { return new ConversationService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Notifications area
        ///     <para> https://developers.podio.com/doc/notifications </para>
        /// </summary>
        public NotificationService NotificationService
        {
            get { return new NotificationService(this); }
        }

        /// Provies all API methods in Reminder area
        /// <para> https://developers.podio.com/doc/reminders </para>
        /// </summary>
        public ReminderService ReminderService
        {
            get { return new ReminderService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Recurrence Area
        ///     <para> https://developers.podio.com/doc/recurrence </para>
        /// </summary>
        public RecurrenceService RecurrenceService
        {
            get { return new RecurrenceService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Importer area
        ///     <para> https://developers.podio.com/doc/importer </para>
        /// </summary>
        public ImporterService ImporterService
        {
            get { return new ImporterService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Question Area
        ///     <para> https://developers.podio.com/doc/questions </para>
        /// </summary>
        public QuestionService QuestionService
        {
            get { return new QuestionService(this); }
        }

        /// <summary>
        ///     Provies all API methods in Subscriptions area
        ///     <para> https://developers.podio.com/doc/subscriptions </para>
        /// </summary>
        public SubscriptionService SubscriptionService
        {
            get { return new SubscriptionService(this); }
        }

        /// <summary>
        ///     Provies API methods in User Area
        ///     <para> https://developers.podio.com/doc/users </para>
        /// </summary>
        public UserService UserService
        {
            get { return new UserService(this); }
        }

        /// <summary>
        ///     Provies API methods in Forms area
        ///     <para> https://developers.podio.com/doc/forms </para>
        /// </summary>
        public FormService FormService
        {
            get { return new FormService(this); }
        }

        /// <summary>
        ///     Provies all API methods in  AppMarket Area
        ///     <para> https://developers.podio.com/doc/app-store </para>
        /// </summary>
        public AppMarketService AppMarketService
        {
            get { return new AppMarketService(this); }
        }

        /// Provies all API methods in Views area
        /// <para> https://developers.podio.com/doc/filters </para>
        /// </summary>
        public ViewService ViewService
        {
            get { return new ViewService(this); }
        }

        /// <summary>
        ///     Provies API methods in Integrations area
        ///     <para> https://developers.podio.com/doc/integrations </para>
        /// </summary>
        public IntegrationService IntegrationService
        {
            get { return new IntegrationService(this); }
        }

        /// <summary>
        ///     Provies API methods in Flow area
        ///     <para> https://developers.podio.com/doc/flows </para>
        /// </summary>
        public FlowService FlowService
        {
            get { return new FlowService(this); }
        }

        #endregion
    }

    public enum RequestMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}