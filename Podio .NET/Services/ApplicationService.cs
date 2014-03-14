using PodioAPI.Models;
using System.Collections.Generic;

namespace PodioAPI.Services
{
    public class ApplicationService
    {
        private Podio _podio;
        public ApplicationService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Gets the definition of an app and can include configuration and fields.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/get-app-22349 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="type">The type of the view of the app requested. Can be either "full", "short", "mini" or "micro". Default value: full</param>
        /// <returns></returns>
        public Application GetApp(int appId, string type = "full")
        {
            string url = string.Format("/app/{0}", appId);
            var attributes = new Dictionary<string, string>()
            {
                {"type", type}
            };
            return _podio.Get<Application>(url, attributes);
        }

        /// <summary>
        /// Activates a deactivated app. This puts the app back in the app navigator and allows insertion of new items.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/activate-app-43822 </para>
        /// </summary>
        /// <param name="appId"></param>
        public void ActivateApp(int appId)
        {
            string url = string.Format("/app/{0}/activate", appId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        /// Deactivates the app with the given id. This removes the app from the app navigator, and disables insertion of new items.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/deactivate-app-43821 </para>
        /// </summary>
        /// <param name="appId"></param>
        public void DeactivateApp(int appId)
        {
            string url = string.Format("/app/{0}/deactivate", appId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        /// Returns all the apps on the space that are visible. The apps are sorted by any custom ordering and else by name.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/get-apps-by-space-22478 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="includeInactive"> True if inactive apps should be included, false otherwise.Default value: false </param>
        /// <returns></returns>
        public List<Application> GetAppsBySpace(int spaceId , bool includeInactive = false)
        {
            var attributes = new Dictionary<string,string>(){
                {"include_inactive",includeInactive.ToString()}
            };
            string url = string.Format("/app/space/{0}/", spaceId);
            return _podio.Get<List<Application>>(url, attributes);
        }
    }
}
