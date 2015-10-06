using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class EmbedService
    {
        private readonly Podio _podio;

        public EmbedService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Grabs metadata and returns metadata for the given url such as title, description and thumbnails.
        ///     <para>Podio API Reference : https://developers.podio.com/doc/embeds/add-an-embed-726483 </para>
        /// </summary>
        /// <param name="embedUrl">The absolute url of the link to fetch metadata for including protocol</param>
        /// <param name="mode">
        ///     "immediate" if the lookup should be performed immediately, before returning from the call, or
        ///     "delayed" if the lookup can be performed delayed (optional, default is "immediate")
        /// </param>
        /// <returns></returns>
        public async Task<Embed> AddAnEmbed(string embedUrl, string mode = "immediate")
        {
            string url = "/embed/";
            dynamic requestData = new
            {
                url = embedUrl,
                mode = mode
            };
            return await _podio.Post<Embed>(url, requestData);
        }
    }
}