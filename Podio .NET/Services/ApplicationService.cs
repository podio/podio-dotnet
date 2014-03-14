using PodioAPI.Models.Request;
using PodioAPI.Models;
using PodioAPI.Utils;
using System.Collections.Generic;
using System.Linq;

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
        public List<Application> GetAppsBySpace(int spaceId, bool includeInactive = false)
        {
            var attributes = new Dictionary<string, string>(){
                {"include_inactive",includeInactive.ToString()}
            };
            string url = string.Format("/app/space/{0}/", spaceId);
            return _podio.Get<List<Application>>(url, attributes);
        }

        /// <summary>
        /// Returns the features that the given apps and optionally space includes.The current list of features are widgets, tasks, filters, forms, integration, items.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/get-features-43648 </para>
        /// </summary>
        /// <param name="appId"> A comma-separated list of app ids from which the features should be extracted </param>
        /// <param name="includeSpace"></param>
        /// <returns></returns>
        public List<string> GetFeatures(int[] appIds, bool includeSpace = false)
        {
            var appIdCSV = Utilities.ArrayToCSV(appIds);
            string url = "/app/features/";
            var attributes = new Dictionary<string, string>(){
                {"app_ids",appIdCSV},
                {"include_space",includeSpace.ToString()}
            };
            return _podio.Get<List<string>>(url, attributes);
        }

        /// <summary>
        /// Returns a single field from an app.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/get-app-field-22353 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public ApplicationField GetAppField(int appId, int fieldId)
        {
            string url = string.Format("/app/{0}/field/{1}", appId, fieldId);
            return _podio.Get<ApplicationField>(url);
        }

        /// <summary>
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/get-icon-suggestions-82045764 </para>
        /// </summary>
        /// <param name="query">Any search term to match.</param>
        /// <returns></returns>
        public List<int> GetIconSuggestions(string query)
        {
            string url = "/app/icon/search";
            var attributes = new Dictionary<string, string>(){
                {"query",query}
            };
            return _podio.Get<List<int>>(url, attributes);
        }

        /// <summary>
        /// Returns the top apps for the active user. This is the apps that the user have interacted with the most.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/get-top-apps-22476 </para>
        /// </summary>
        /// <param name="excludeDemo"> True if apps from demo workspace should be excluded, false otherwiseDefault value: false </param>
        /// <param name="limit">  maximum number of apps to return Default value: 4 </param>
        /// <returns></returns>
        public List<Application> GetTopApps(bool excludeDemo = false, int limit = 4)
        {
            string url = "/app/top/";
            var attributes = new Dictionary<string, string>(){
                {"exclude_demo",excludeDemo.ToString()},
                {"limit",limit.ToString()}
            };
            return _podio.Get<List<Application>>(url, attributes);
        }

        /// <summary>
        /// Returns the top apps for the user inside the given organization
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/get-top-apps-for-organization-1671395 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public List<Application> GetTopAppsForOrganization(int organizationId)
        {
            string url = string.Format("/app/org/{0}/top/", organizationId);
            return _podio.Get<List<Application>>(url);
        }

        /// <summary>
        /// Deletes the app with the given id. This will delete all items, widgets, filters and shares on the app. This operation is not reversible.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/delete-app-43693 </para>
        /// </summary>
        /// <param name="appId"></param>
        public void DeleteApp(int appId)
        {
            string url = string.Format("/app/{0}", appId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        /// Deletes a field on an app. When deleting a field any new items and updates to existing items will not have this field present. For existing items, the field will still be presented when viewing the item.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/delete-app-field-22355 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="fieldId"></param>
        /// <param name="deleteValues"> True if the values for the fields should be deleted, false otherwise Default value: false </param>
        /// <returns></returns>
        public ApplicationRevision DeleteAppField(int appId, int fieldId, bool deleteValues = false)
        {
            string url = string.Format("/app/{0}/field/{1}", appId, fieldId);
            dynamic attributes = new
            {
                delete_values = deleteValues.ToString()
            };
            return _podio.Delete<ApplicationRevision>(url, attributes);
        }

        /// <summary>
        /// Returns all the apps for the active user.
        /// <para>Podio API Reference: https://developers.podio.com/doc/applications/get-all-user-apps-5902728 </para>
        /// </summary>
        /// <param name="excludeAppIds">The comma separated list of app_ids to exclude from the returned list.</param>
        /// <param name="referenceableInOrg">ID of the Organization to filter apps by. Returns only apps the user can reference in that Organization.</param>
        /// <param name="right">The right the user must have on the returned apps.</param>
        /// <param name="text">Any search term that should either match the name of the app, the name of items in the app or the name of the workspace the app is in.</param>
        /// <param name="excludeDemo">True if apps from demo workspace should be excluded, false otherwise Default value: false</param>
        /// <param name="limit">The maximum number of apps to return Default value: 4</param>
        /// <param name="order">The order to return the apps in.: score: Order by the  score of the app for the active user, name: Order by the name of the app. Default value: score</param>
        /// <returns></returns>
        public List<Application> GetAllUserApps(int[] excludeAppIds = null, int? referenceableInOrg = null, string right = null, string text = null, bool excludeDemo = false, int limit = 4, string order = "score")
        {
            var appIdCSV = Utilities.ArrayToCSV(excludeAppIds);
            string url = "/app/v2/";
            var attributes = new Dictionary<string, string>(){
                {"exclude_app_ids",appIdCSV},
                {"exclude_demo",excludeDemo.ToString()},
                {"limit",limit.ToString()},
                {"order",order},
                {"referenceable_in_org",referenceableInOrg.ToString()},
                {"right",right},
                {"text",text}
            };
            return _podio.Get<List<Application>>(url, attributes);
        }
    }
}
