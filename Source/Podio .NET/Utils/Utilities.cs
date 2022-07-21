using PodioAPI.Models.Request;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PodioAPI.Utils
{
    internal class Utility
    {
        internal static string ArrayToCSV(int[] array, string splitter = ",")
        {
            if (array != null && array.Length > 0)
                return string.Join(splitter, array);

            return string.Empty;
        }

        internal static string ArrayToCSV(long[] array, string splitter = ",")
        {
            if (array != null && array.Length > 0)
                return string.Join(splitter, array);

            return string.Empty;
        }

        internal static string ArrayToCSV(string[] array, string splitter = ",")
        {
            if (array != null && array.Length > 0)
                return string.Join(splitter, array);

            return string.Empty;
        }

        /// <summary>
        ///     Convert dictionay to to query string
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        internal static string DictionaryToQueryString(Dictionary<string, string> attributes)
        {
            var encodedString = string.Empty;
            if (attributes != null && attributes.Any())
            {
                var parameters = new List<string>();
                foreach (var item in attributes)
                {
                    if (item.Key != string.Empty && !string.IsNullOrEmpty(item.Value))
                    {
                        parameters.Add(WebUtility.UrlEncode(item.Key) + "=" + (WebUtility.UrlEncode(item.Value)));
                    }
                }
                if (parameters.Any())
                    encodedString = string.Join("&", parameters.ToArray());
            }

            return encodedString;
        }

        /// <summary>
        ///     Transform options object to query parameteres
        /// </summary>
        /// <param name="url"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal static string PrepareUrlWithOptions(string url, CreateUpdateOptions options)
        {
            string urlWithOptions = "";
            List<string> parameters = new List<string>();
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

        public static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
        {
            var clone = new HttpRequestMessage(request.Method, request.RequestUri);

            // Copy the request's content (via a MemoryStream) into the cloned object
            var ms = new MemoryStream();
            if (request.Content != null)
            {
                await request.Content.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;
                clone.Content = new StreamContent(ms);

                // Copy the content headers
                if (request.Content.Headers != null)
                    foreach (var h in request.Content.Headers)
                        clone.Content.Headers.Add(h.Key, h.Value);
            }


            clone.Version = request.Version;

            foreach (KeyValuePair<string, object> prop in request.Properties)
                clone.Properties.Add(prop);

            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return clone;
        }

        public static async Task<HttpRequestMessage> CopyHttpRequestMessageContent(HttpRequestMessage originalRequest, HttpRequestMessage copy)
        {
            // Copy the request's content (via a MemoryStream) into the cloned object
            var ms = new MemoryStream();
            if (originalRequest.Content != null)
            {
                await originalRequest.Content.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;
                copy.Content = new StreamContent(ms);
            }

            return copy;
        }
    }
}