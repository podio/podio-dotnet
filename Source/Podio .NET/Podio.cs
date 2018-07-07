using Newtonsoft.Json;
using PodioAPI.Exceptions;
using PodioAPI.Models;
using PodioAPI.Services;
using PodioAPI.Utils;
using PodioAPI.Utils.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PodioAPI
{
    public class Podio
    {
        protected string ClientId { get; set; }
        protected string ClientSecret { get; set; }
        public OAuth OAuth { get; set; }
        public IAuthStore AuthStore { get; set; }
        public int RateLimit { get; private set; }
        public int RateLimitRemaining { get; private set; }
        protected virtual string ApiUrl { get; set; }

        private static readonly HttpClient HttpClient;

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
        public Podio(string clientId, string clientSecret, IAuthStore authStore = null)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            ApiUrl = "https://api.podio.com:443";

            AuthStore = authStore ?? new NullAuthStore();
        }

        static Podio()
        {
            HttpClient = new HttpClient();
        }

        #region Request Helpers

        internal async Task<T> Get<T>(string url, Dictionary<string, string> requestData = null, bool isFileDownload = false, bool returnAsString = false)
            where T : new()
        {
            string queryString = Utility.DictionaryToQueryString(requestData);
            if (!string.IsNullOrEmpty(queryString))
            {
                url = url + "?" + queryString;
            }

            var request = CreateHttpRequest(url, HttpMethod.Get, true, isFileDownload);
            return await Request<T>(request, isFileDownload, returnAsString);
        }

        internal async Task<T> Post<T>(string url, dynamic requestData = null, bool isOAuthTokenRequest = false) where T : new()
        {
            var request = CreateHttpRequest(url, HttpMethod.Post, !isOAuthTokenRequest);
            if (isOAuthTokenRequest)
            {
                request.Content = new FormUrlEncodedContent(requestData);
            }
            else
            {
                if (requestData != null)
                {
                    var jsonString = JSONSerializer.Serilaize(requestData);
                    request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                }
            }

            return await Request<T>(request);
        }

        internal async Task<T> PostMultipartFormData<T>(string url, byte[] fileData, string fileName, string mimeType) where T : new()
        {
            var request = CreateHttpRequest(url, HttpMethod.Post);

            var multipartFormContent = new MultipartFormDataContent();
            multipartFormContent.Add(new ByteArrayContent(fileData), "source", fileName);
            multipartFormContent.Add(new StringContent(fileName), "filename");

            request.Content = multipartFormContent;

            return await Request<T>(request);
        }

        internal async Task<T> Put<T>(string url, object requestData = null) where T : new()
        {
            var request = CreateHttpRequest(url, HttpMethod.Put);
            var jsonString = JSONSerializer.Serilaize(requestData);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await Request<T>(request);
        }

        internal async Task<T> Delete<T>(string url, Dictionary<string, string> requestData = null) where T : new()
        {
            var request = CreateHttpRequest(url, HttpMethod.Delete);
            if(requestData != null)
                request.Content = new FormUrlEncodedContent(requestData);

            return await Request<T>(request);
        }

        internal async Task<T> Request<T>(HttpRequestMessage httpRequest, bool isFileDownload = false, bool returnAsString = false) where T : new()
        {

            // for retry in case of failure.
            var requestCopy = CreateHttpRequest(httpRequest.RequestUri.OriginalString, httpRequest.Method, true, isFileDownload);
            await Utility.CopyHttpRequestMessageContent(httpRequest, requestCopy);

            var response = await HttpClient.SendAsync(httpRequest);

            // Get rate limits from header values
            if (response.Headers.Contains("X-Rate-Limit-Remaining"))
                RateLimitRemaining = int.Parse(response.Headers.GetValues("X-Rate-Limit-Remaining").First());
            if (response.Headers.Contains("X-Rate-Limit-Limit"))
                RateLimit = int.Parse(response.Headers.GetValues("X-Rate-Limit-Limit").First());

            if (response.IsSuccessStatusCode)
            {
                if(isFileDownload)
                {
                    var fileResponse = new FileResponse();
                    fileResponse.FileContents = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    fileResponse.ContentType = response.Content.Headers.ContentType.ToString();
                    fileResponse.ContentLength = response.Content.Headers.ContentLength ?? 0;

                    return fileResponse.ChangeType<T>();
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }

                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (returnAsString)
                    {
                        var stringResponse = new StringResponse { Data = responseBody };
                        return stringResponse.ChangeType<T>();
                    }

                    return JSONSerializer.Deserialize<T>(responseBody);
                }
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var podioError = JSONSerializer.Deserialize<PodioError>(responseBody);

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        // If RefreshToken exists, refresh the access token and try the request again
                        if (OAuth is PodioOAuth podioOAuth && !string.IsNullOrEmpty(podioOAuth.RefreshToken) && podioError.ErrorDescription == "expired_token" || podioError.Error == "invalid_token")
                        {
                            var authInfo = await RefreshAccessToken().ConfigureAwait(false);
                            if (authInfo != null && !string.IsNullOrEmpty(authInfo.AccessToken))
                            {
                                requestCopy.Headers.Authorization = new AuthenticationHeaderValue("OAuth2", authInfo.AccessToken);
                                return await Request<T>(requestCopy, isFileDownload, returnAsString);
                            }
                        }
                        else
                        {
                            OAuth = null;
                            throw new PodioAuthorizationException((int)response.StatusCode, podioError);
                        }
                    }
                    else
                    {
                        ProcessErrorResponse(response.StatusCode, podioError);
                    }

                }
                catch (JsonException ex)
                {
                    throw new PodioInvalidJsonException((int)response.StatusCode, new PodioError
                    {
                        Error = "Error response is not a valid Json string.",
                        ErrorDescription = ex.ToString(),
                        ErrorDetail = responseBody
                    });
                }
               
                return default(T);
            }
        }

        private HttpRequestMessage CreateHttpRequest(string url, HttpMethod httpMethod, bool addAuthorizationHeader = true, bool isFileDownload = false)
        {
            var fullUrl = ApiUrl + url;
            if (url.StartsWith("http")) fullUrl = url;

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(fullUrl),
                Method = httpMethod
            };

            if (isFileDownload)
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            else
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (addAuthorizationHeader)
            {
                OAuth?.AddAuthorizationHeader(request.Headers);
            }

            return request;
        }

        private void ProcessErrorResponse(HttpStatusCode statusCode, PodioError podioError)
        {
            var status = (int)statusCode;
           
            switch (status)
            {
                case 400:
                    if (podioError.Error == "invalid_grant")
                    {
                        //Reset auth info
                        OAuth = null;
                        throw new PodioInvalidGrantException(status, podioError);
                    }
                    else
                    {
                        throw new PodioBadRequestException(status, podioError);
                    }
                case 403:
                    throw new PodioForbiddenException(status, podioError);
                case 404:
                    throw new PodioNotFoundException(status, podioError);
                case 409:
                    throw new PodioConflictException(status, podioError);
                case 410:
                    throw new PodioGoneException(status, podioError);
                case 420:
                    throw new PodioRateLimitException(status, podioError);
                case 500:
                    throw new PodioServerException(status, podioError);
                case 502:
                case 503:
                case 504:
                    throw new PodioUnavailableException(status, podioError);
                default:
                    throw new PodioException(status, podioError);
            }
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
        public async Task<PodioOAuth> AuthenticateWithApp(int appId, string appToken)
        {
            var authRequest = new Dictionary<string, string>()
            {
                {"app_id", appId.ToString()},
                {"app_token", appToken},
                {"grant_type", "app"}
            };
            return await Authenticate(authRequest).ConfigureAwait(false);
        }

        /// <summary>
        ///     Authenticate with username and password
        ///     <para>Podio API Reference: https://developers.podio.com/authentication/username_password </para>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>PodioOAuth object with OAuth data</returns>
        public async Task<PodioOAuth> AuthenticateWithPassword(string username, string password)
        {
            var authRequest = new Dictionary<string, string>()
            {
                {"username", username},
                {"password", password},
                {"grant_type", "password"}
            };
            return await Authenticate(authRequest).ConfigureAwait(false);
        }

        /// <summary>
        ///     Authenticate with an authorization code
        ///     <para>Podio API Reference: https://developers.podio.com/authentication/server_side </para>
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <param name="redirectUri"></param>
        /// <returns>PodioOAuth object with OAuth data</returns>
        public async Task<PodioOAuth> AuthenticateWithAuthorizationCode(string authorizationCode, string redirectUri)
        {
            var authRequest = new Dictionary<string, string>()
            {
                {"code", authorizationCode},
                {"redirect_uri", redirectUri},
                {"grant_type", "authorization_code"}
            };
            return await Authenticate(authRequest).ConfigureAwait(false);
        }

        /// <summary>
        ///     Authenticate with ShareFile OAuth context
        /// </summary>
        /// <param name="shareFileAccountId"></param>
        /// <param name="shareFileAccessToken"></param>
        /// <returns>ShareFileOAuth object with OAuth data</returns>
        public ShareFileOAuth AuthenticateWithShareFile(string shareFileAccountId, string shareFileAccessToken)
        {
            this.OAuth = new ShareFileOAuth
            {
                AccountId = shareFileAccountId,
                AccessToken = shareFileAccessToken
            };

            AuthStore.Set(null);

            return (ShareFileOAuth)OAuth;
        }

        /// <summary>
        ///     Refresh the Access Token.
        ///     <para>When the access token expires, you can use this method to refresh your access, and gain another access_token</para>
        ///     <para>Podio API Reference: https://developers.podio.com/authentication </para>
        /// </summary>
        /// <returns>PodioOAuth object with OAuth data</returns>
        public async Task<PodioOAuth> RefreshAccessToken()
        {
            if (!(OAuth is PodioOAuth podioOAuth))
            {
                return null;
            }

            var authRequest = new Dictionary<string, string>()
            {
                {"refresh_token", podioOAuth.RefreshToken},
                {"grant_type", "refresh_token"}
            };
            return await Authenticate(authRequest).ConfigureAwait(false);
        }

        private async Task<PodioOAuth> Authenticate(Dictionary<string, string> attributes)
        {
            attributes["client_id"] = ClientId;
            attributes["client_secret"] = ClientSecret;

            PodioOAuth podioOAuth = await Post<PodioOAuth>("/oauth/token", attributes, true).ConfigureAwait(false);
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
            return String.Format(authorizeUrl, this.ClientId, WebUtility.UrlEncode(redirectUri));
        }

        /// <summary>
        ///     Check if there is a stored access token already present.
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return OAuth != null && OAuth.IsAuthenticated();
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
}