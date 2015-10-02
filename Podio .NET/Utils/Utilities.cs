using PodioAPI.Models;
using PodioAPI.Models.Request;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
                        parameters.Add(HttpUtility.UrlEncode(item.Key) + "=" + (HttpUtility.UrlEncode(item.Value)));
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
    }
}