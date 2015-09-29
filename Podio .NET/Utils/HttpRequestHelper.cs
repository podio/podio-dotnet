using PodioAPI.Exceptions;
using PodioAPI.Models;
using PodioAPI.Utils.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PodioAPI.Utils
{
    public class HttpRequestHelper
    {
        private OAuthInfo _OAuthInfo;

        public HttpRequestHelper(OAuthInfo oAuthInfo)
        {
            _OAuthInfo = oAuthInfo;
        }

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
            url = "https://api.podio.com:443" + url;

            //To use url other than api.podio.com, ex file download from files.podio.com
            if (options != null && options.ContainsKey("url"))
            {
                url = options["url"];
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

            if (_OAuthInfo != null && !string.IsNullOrEmpty(_OAuthInfo.AccessToken))
            {
                requestHeaders["Authorization"] = "OAuth2 " + _OAuthInfo.AccessToken;
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
            ServicePointManager.Expect100Continue = false;
            request.Method = httpMethod;
            request.UserAgent = "Podio Dotnet Client";

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
                    else if (item == "fileByteUpload")
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

            return ProcessPodioResponse<T>(requestMethod, requestData, options, originalUrl, podioResponse, ref responseObject);
        }

        private T ProcessPodioResponse<T>(RequestMethod requestMethod, dynamic requestData, dynamic options, string originalUrl, PodioResponse podioResponse, ref T responseObject) where T : new()
        {
            //if (podioResponse.Headers.ContainsKey("X-Rate-Limit-Remaining"))
            //    RateLimitRemaining = int.Parse(podioResponse.Headers["X-Rate-Limit-Remaining"]);
            //if (podioResponse.Headers.ContainsKey("X-Rate-Limit-Limit"))
            //    RateLimit = int.Parse(podioResponse.Headers["X-Rate-Limit-Limit"]);

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
                        _OAuthInfo = new OAuthInfo();
                        throw new PodioInvalidGrantException(podioResponse.Status, podioError);
                    }
                    else
                    {
                        throw new PodioBadRequestException(podioResponse.Status, podioError);
                    }
                case 401:
                    if (podioError.ErrorDescription == "expired_token" || podioError.Error == "invalid_token")
                    {
                        if (!string.IsNullOrEmpty(_OAuthInfo.RefreshToken))
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
        ///     Write an object to request stream.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="request">HttpWebRequest object of which request to write</param>
        internal void WriteToRequestStream(object obj, HttpWebRequest request)
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
        ///     Add a file to request stream
        /// </summary>
        /// <param name="filePath">Physical path to file</param>
        /// <param name="fileName">File Name</param>
        /// <param name="request">HttpWebRequest object of which request stream file is added to</param>
        private void AddFileToRequestStream(string filePath, string fileName, HttpWebRequest request)
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

        private void AddFileToRequestStream(string fileName, byte[] data, string mimeType, HttpWebRequest request)
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
            MemoryStream memoryStream = new MemoryStream();
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

        /// <summary>
        ///     Convert dictionay to to query string
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        internal static string EncodeAttributes(Dictionary<string, string> attributes)
        {
            var encodedString = string.Empty;
            if (attributes.Any())
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
    }
}
