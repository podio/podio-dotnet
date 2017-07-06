using System.Collections.Generic;
using System.Linq;
using PodioAPI.Models;
using PodioAPI.Models.Request;
using PodioAPI.Utils;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class ApplicationService
    {
        private readonly Podio _podio;

        public ApplicationService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Gets the definition of an app and can include configuration and fields.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-app-22349 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="view">
        ///     The type of the view of the app requested. Can be either "full", "short", "mini" or "micro". Default
        ///     value: full
        /// </param>
        /// <param name="fields">
        ///     This parameter can be used to include more or less content in responses than the defaults provided by Podio.
        ///     E.g. space.view(full)
        /// </param>
        /// <returns></returns>
        public async Task<Application> GetApp(int appId, string view = "full", string fields = null)
        {
            string url = string.Format("/app/{0}", appId);
            var requestData = new Dictionary<string, string>()
            {
                {"view", view}
            };

            if (!string.IsNullOrEmpty(fields))
            {
                requestData.Add("fields", fields);
            }

            return await _podio.Get<Application>(url, requestData);

        }

        /// <summary>
        ///     Activates a deactivated app. This puts the app back in the app navigator and allows insertion of new items.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/activate-app-43822 </para>
        /// </summary>
        /// <param name="appId"></param>
        public async System.Threading.Tasks.Task ActivateApp(int appId)
        {
            string url = string.Format("/app/{0}/activate", appId);
            await _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Deactivates the app with the given id. This removes the app from the app navigator, and disables insertion of new
        ///     items.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/deactivate-app-43821 </para>
        /// </summary>
        /// <param name="appId"></param>
        public async System.Threading.Tasks.Task DeactivateApp(int appId)
        {
            string url = string.Format("/app/{0}/deactivate", appId);
            await _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Returns all the apps on the space that are visible. The apps are sorted by any custom ordering and else by name.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-apps-by-space-22478 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="includeInactive"> True if inactive apps should be included, false otherwise.Default value: false </param>
        /// <param name="additionalAttributes">Additional attributes to include in query string</param>
        /// <returns></returns>
        public async Task<List<Application>> GetAppsBySpace (int spaceId, bool includeInactive = false, Dictionary<string, string> additionalAttributes = null)
        {
            var requestData = new Dictionary<string, string>()
            {
                {"include_inactive", includeInactive.ToString()}
            };

            if (additionalAttributes != null)
            {
                foreach (var keyvaluePair in additionalAttributes)
                {
                    requestData.Add(keyvaluePair.Key, keyvaluePair.Value);
                }
            }

            string url = string.Format("/app/space/{0}/", spaceId);
            return await _podio.Get<List<Application>>(url, requestData);
        }

        /// <summary>
        ///     Returns the features that the given apps and optionally space includes.The current list of features are widgets,
        ///     tasks, filters, forms, integration, items.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-features-43648 </para>
        /// </summary>
        /// <param name="appIds"> A comma-separated list of app ids from which the features should be extracted </param>
        /// <param name="includeSpace"></param>
        /// <returns></returns>
        public async Task<List<string>> GetFeatures(int[] appIds, bool includeSpace = false)
        {
            string appIdCSV = Utility.ArrayToCSV(appIds);
            string url = "/app/features/";
            var requestData = new Dictionary<string, string>()
            {
                {"app_ids", appIdCSV},
                {"include_space", includeSpace.ToString()}
            };
            return await _podio.Get<List<string>>(url, requestData);
        }

        /// <summary>
        ///     Returns a single field from an app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-app-field-22353 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public async Task<ApplicationField> GetAppField(int appId, int fieldId)
        {
            string url = string.Format("/app/{0}/field/{1}", appId, fieldId);
            return await _podio.Get<ApplicationField>(url);
        }

        /// <summary>
        ///     Returns a single field from an app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-app-field-22353 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public async Task<ApplicationField> GetAppField(int appId, string externalId)
        {
            string url = string.Format("/app/{0}/field/{1}", appId, externalId);
            return await _podio.Get<ApplicationField>(url);
        }

        /// <summary>
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-icon-suggestions-82045764 </para>
        /// </summary>
        /// <param name="query">Any search term to match.</param>
        /// <returns></returns>
        public async Task<List<int>> GetIconSuggestions(string query)
        {
            string url = "/app/icon/search";
            var requestData = new Dictionary<string, string>()
            {
                {"query", query}
            };
            return await _podio.Get<List<int>>(url, requestData);
        }

        /// <summary>
        ///     Returns the top apps for the active user. This is the apps that the user have interacted with the most.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-top-apps-22476 </para>
        /// </summary>
        /// <param name="excludeDemo"> True if apps from demo workspace should be excluded, false otherwiseDefault value: false </param>
        /// <param name="limit">  maximum number of apps to return Default value: 4 </param>
        /// <returns></returns>
        public async Task<List<Application>> GetTopApps(bool excludeDemo = false, int limit = 4)
        {
            string url = "/app/top/";
            var requestData = new Dictionary<string, string>()
            {
                {"exclude_demo", excludeDemo.ToString()},
                {"limit", limit.ToString()}
            };
            return await _podio.Get<List<Application>>(url, requestData);
        }

        /// <summary>
        ///     Returns the top apps for the user inside the given organization
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-top-apps-for-organization-1671395 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public async Task<List<Application>> GetTopAppsForOrganization(int organizationId)
        {
            string url = string.Format("/app/org/{0}/top/", organizationId);
            return await _podio.Get<List<Application>>(url);
        }

        /// <summary>
        ///     Deletes the app with the given id. This will delete all items, widgets, filters and shares on the app. This
        ///     operation is not reversible.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/delete-app-43693 </para>
        /// </summary>
        /// <param name="appId"></param>
        public async System.Threading.Tasks.Task DeleteApp(int appId)
        {
            string url = string.Format("/app/{0}", appId);
            await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Deletes a field on an app. When deleting a field any new items and updates to existing items will not have this
        ///     field present. For existing items, the field will still be presented when viewing the item.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/delete-app-field-22355 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="fieldId"></param>
        /// <param name="deleteValues"> True if the values for the fields should be deleted, false otherwise Default value: false </param>
        /// <returns></returns>
        public async Task<ApplicationRevision> DeleteAppField(int appId, int fieldId, bool deleteValues = false)
        {
            string url = string.Format("/app/{0}/field/{1}", appId, fieldId);
            var requestData = new Dictionary<string, string>()
            {
                {"delete_values", deleteValues.ToString()}
            };
           
            return await _podio.Delete<ApplicationRevision>(url, requestData);
        }

        /// <summary>
        ///     Returns all the apps for the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-all-user-apps-5902728 </para>
        /// </summary>
        /// <param name="excludeAppIds">The comma separated list of app_ids to exclude from the returned list.</param>
        /// <param name="referenceableInOrg">
        ///     ID of the Organization to filter apps by. Returns only apps the user can reference in
        ///     that Organization.
        /// </param>
        /// <param name="right">The right the user must have on the returned apps.</param>
        /// <param name="text">
        ///     Any search term that should either match the name of the app, the name of items in the app or the
        ///     name of the workspace the app is in.
        /// </param>
        /// <param name="excludeDemo">True if apps from demo workspace should be excluded, false otherwise Default value: false</param>
        /// <param name="limit">The maximum number of apps to return Default value: 4</param>
        /// <param name="order">
        ///     The order to return the apps in.: score: Order by the  score of the app for the active user, name:
        ///     Order by the name of the app. Default value: score
        /// </param>
        /// <returns></returns>
        public async Task<List<Application>> GetAllUserApps(int[] excludeAppIds = null, int? referenceableInOrg = null,
            string right = null, string text = null, bool excludeDemo = false, int limit = 4, string order = "score")
        {
            string appIdCSV = Utility.ArrayToCSV(excludeAppIds);
            string url = "/app/v2/";
            var requestData = new Dictionary<string, string>()
            {
                {"exclude_app_ids", appIdCSV},
                {"exclude_demo", excludeDemo.ToString()},
                {"limit", limit.ToString()},
                {"order", order},
                {"referenceable_in_org", referenceableInOrg.ToString()},
                {"right", right},
                {"text", text}
            };
            return await _podio.Get<List<Application>>(url, requestData);
        }

        /// <summary>
        ///     Returns the app on the given space with the given URL label.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-app-on-space-by-url-label-477105 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="UrlLabel"></param>
        /// <param name="type">
        ///     The type of the view of the app requested. Can be either "full", "short", "mini" or "micro". Default
        ///     value: full
        /// </param>
        /// <returns></returns>
        public async Task<Application> GetAppOnSpaceByURLLabel(int spaceId, string UrlLabel, string type = "full")
        {
            string url = string.Format("/app/space/{0}/{1}", spaceId, UrlLabel);
            var requestData = new Dictionary<string, string>()
            {
                {"type", type}
            };
            return await _podio.Get<Application>(url, requestData);
        }

        /// <summary>
        ///     Returns app based on the provided org_label, space_label and app_label.
        ///     <para>
        ///         Podio API Reference:
        ///         https://developers.podio.com/doc/applications/get-app-by-org-label-space-label-and-app-label-91708386
        ///     </para>
        /// </summary>
        /// <param name="orgLabel"></param>
        /// <param name="spaceLabel"></param>
        /// <param name="appLabel"></param>
        /// <returns></returns>
        public async Task<Application> GetAppByOrgLabelSpaceLabelAndAppLabel(string orgLabel, string spaceLabel, string appLabel)
        {
            string url = string.Format("/app/org/{0}/space/{1}/{2}", orgLabel, spaceLabel, appLabel);
            return await _podio.Get<Application>(url);
        }

        /// <summary>
        ///     Updates the order of the apps on the space. It should post all the apps from the space in the order required.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/update-app-order-22463  </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="appIds"></param>
        public async System.Threading.Tasks.Task UpdateAppOrder(int spaceId, List<int> appIds)
        {
            appIds = new List<int>();
            string url = string.Format("/app/space/{0}/order", spaceId);
            await _podio.Put<dynamic>(url, appIds);
        }

        /// <summary>
        ///     Updates the app with a new description.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/update-app-description-33569973 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="description"></param>
        public async System.Threading.Tasks.Task UpdateAppDescription(int appId, string description)
        {
            string url = string.Format("/app/{0}/description", appId);
            dynamic requestData = new
            {
                description = description
            };
            await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Updates the usage instructions for the app.
        ///     <para>Podio API Reference:https://developers.podio.com/doc/applications/update-app-usage-instructions-33570086 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="usage"></param>
        public async System.Threading.Tasks.Task UpdateAppUsageInstructions(int appId, string usage)
        {
            string url = string.Format("/app/{0}/usage", appId);
            dynamic requestData = new
            {
                usage = usage
            };
            await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Installs the app with the given id on the space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/install-app-22506 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="spaceId"></param>
        /// <param name="features">
        ///     The features that should be installed with the app. Options are: filters, tasks, widgets,
        ///     integration, forms, items. If the value is not given all but the "items" feature will be selected.
        /// </param>
        /// <returns></returns>
        public async Task<int> InstallApp(int appId, int spaceId, string[] features = null)
        {
            features = features == null ? new string[] {"items"} : features;
            string url = string.Format("/app/{0}/install", appId);
            dynamic requestData = new
            {
                space_id = spaceId,
                features = features
            };
            dynamic response = await _podio.Post<dynamic>(url, requestData);
            return (int) response["app_id"];
        }

        /// <summary>
        ///     Returns the list of possible calculations that can be done on related apps.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-calculations-for-app-773005 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<List<AppCalculation>> GetCalculationsForApp(int appId)
        {
            string url = string.Format("/app/{0}/calculation/", appId);
            return await _podio.Get<List<AppCalculation>>(url);
        }

        /// <summary>
        ///     Creates a new app on a space.
        ///     <para>Podio API Reference:https://developers.podio.com/doc/applications/add-new-app-22351 </para>
        /// </summary>
        /// <param name="application"></param>
        /// <returns>The id of the newly created app</returns>
        public async Task<int> AddNewApp(Application application)
        {
            /*
                Example Usage: Adding a new application with a text field and category field.
             
                var application = new Application();
                application.SpaceId = SPACE_ID;
                application.Config = new ApplicationConfiguration
                {
                    Name = "Application Name",
                    Icon = "230.png",
                    ItemName = "Single item",
                    Description = "My Description"
                };
                var textField = application.Field<TextApplicationField>();
                textField.Config.Label = "Sample Text Field";
                textField.Config.Label = "Sample Text Field Description";
                textField.Size = "small";
             
                var categoryField = application.Field<CategoryApplicationField>();
                categoryField.Config.Label = "Sample Category Field";
                categoryField.Options = new List<CategoryItemField.Answer>()
                {
                    new CategoryItemField.Answer{ Text = "Option One "},
                    new CategoryItemField.Answer{ Text = "Option Two "}
                };
                categoryField.Multiple = true;
                categoryField.Display = "list";
                int newAppID = podio.ApplicationService.AddNewApp(application);
             */

            string url = "/app/";
            var requestDate = new ApplicationCreateUpdateRequest()
            {
                SpaceId = application.SpaceId,
                Config = application.Config,
                Fields = application.Fields
            };
            dynamic response = await _podio.Post<dynamic>(url, requestDate);
            return (int) response["app_id"];
        }

        /// <summary>
        ///     Adds a new field to an app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-app-field-22353 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="field"></param>
        public async Task<int> AddNewAppField(int appId, ApplicationField field)
        {
            /*
               Example Usage: Adding a new text field.
             
                var applicationField = new ApplicationField();
                applicationField.Type = "text";
                applicationField.Config.Label = "New text field";
                applicationField.Config.Settings = new Dictionary<string, object>
                {
                    {"size", "large"}
                };
                podio.ApplicationService.AddNewAppField(APP_ID, applicationField);
            */
            string url = string.Format("/app/{0}/field/", appId);
            dynamic response = await _podio.Post<dynamic>(url, field);
            return (int) response["field_id"];
        }

        /// <summary>
        ///     Updates an app.
        ///     <para>
        ///         The update can contain an new configuration for the app, addition of new fields as well as updates to the
        ///         configuration of existing fields. Fields not included will not be deleted.
        ///         To delete a field use the "delete field" operation
        ///     </para>
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/update-app-22352 </para>
        /// </summary>
        /// <param name="application"></param>
        public async System.Threading.Tasks.Task UpdateApp(Application application, bool silent = false)
        {
            /*
               Example Usage: Updating an App by adding a new Category Field and Updating a Text Field
             
               var application = new Application();
               application.AppId = APP_ID;
               application.Config = new ApplicationConfiguration
               {
                   Name = "Application Name",
                   Icon = "230.png",
                   ItemName = "Single item",
                   Description = "My Description"
               };
              
               //For Updating Existing field (provide 'FieldId')
               var textField = application.Field<TextApplicationField>();
               textField.FieldId = 123456;
               textField.Config.Label = "Sample Text Field Updated";
               textField.Config.Label = "Sample Text Field Description Updated";
               textField.Size = "small";
               
               //For adding new field
               var categoryField = application.Field<CategoryApplicationField>();
               categoryField.Config.Label = "New Sample Category Field";
               categoryField.Options = new List<CategoryItemField.Answer>()
               {
                   new CategoryItemField.Answer{ Text = "Option One "},
                   new CategoryItemField.Answer{ Text = "Option Two "}
               };
               categoryField.Multiple = true;
               categoryField.Display = "list";
               int newAppID = podio.ApplicationService.UpdateApp(application);
            */

            string url = string.Format("/app/{0}", application.AppId);
            url = Utility.PrepareUrlWithOptions(url, new CreateUpdateOptions(silent, false));
            var requestData = new ApplicationCreateUpdateRequest()
            {
                Config = application.Config,
                Fields = application.Fields
            };
            await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Updates the configuration of an app field. The type of the field cannot be updated, only the configuration.
        ///     <para>Supply the application object with AppId, and the field to be updated</para>
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/update-an-app-field-22356 </para>
        /// </summary>
        /// <param name="application"></param>
        public async System.Threading.Tasks.Task UpdateAnAppField(Application application)
        {
            /*
                Example Usage: Updating the configuration of a text field.
             
                var application = new Application();
                application.AppId = APP_ID;
                var textField = application.Field<TextApplicationField>();
                textField.FieldId = 57037270;
                textField.Config.Label = "Updated label";
                textField.Config.Description = "Updated description";
                textField.Size = "large";
            */
            ApplicationField fieldToUpdate = application.Fields.FirstOrDefault();
            await UpdateAnAppField(application.AppId, fieldToUpdate.FieldId.Value, fieldToUpdate.InternalConfig);
        }

        /// <summary>
        ///     Updates the configuration of an app field. The type of the field cannot be updated, only the configuration.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/update-an-app-field-22356 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="fieldId"></param>
        /// <param name="config"></param>
        public async System.Threading.Tasks.Task UpdateAnAppField(int appId, int fieldId, FieldConfig config)
        {
            string url = string.Format("/app/{0}/field/{1}", appId, fieldId);
            await _podio.Put<dynamic>(url, config);
        }

        /// <summary>
        ///     Returns the apps that the given app depends on.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-app-dependencies-39159 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<ApplicationDependency> GetAppDependencies(int appId)
        {
            string url = string.Format("/app/{0}/dependencies/", appId);
            return await _podio.Get<ApplicationDependency>(url);
        }

        /// <summary>
        ///     Returns all the active apps on the space along with their dependencies. The dependencies are only one level deep.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/applications/get-space-app-dependencies-45779 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public async Task<ApplicationDependency> GetSpaceAppDependencies(int spaceId)
        {
            string url = string.Format("/space/{0}/dependencies/", spaceId);
            return await _podio.Get<ApplicationDependency>(url);
        }
    }
}