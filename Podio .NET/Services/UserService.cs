using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class UserService
    {
        private readonly Podio _podio;

        public UserService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Deletes the property for the active user with the given name. The property is specific to the auth client used.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/delete-user-property-29800 </para>
        /// </summary>
        /// <param name="name"></param>
        public async Task<dynamic> DeleteUserProperty(string name)
        {
            string url = string.Format("/user/property/{0}", name);
            return  await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Get's the setting for the given client type and notification type.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/get-notification-setting-3649859 </para>
        /// </summary>
        /// <param name="clientType">Client type: "email" and "mobile".</param>
        /// <param name="notificationType">
        ///     Notification type: "digest", "bulletin", "reference", "message", "space",
        ///     "subscription", "user", "reminder", "push_notification", "push_notification_sound" or "push_notification_browser".
        /// </param>
        /// <returns></returns>
        public async Task<bool> GetNotificationSetting(string clientType, string notificationType)
        {
            string url = string.Format("/user/setting/{0}/{1}", clientType, notificationType);
            dynamic response =  await _podio.Get<dynamic>(url);
            return (bool) response["value"];
        }

        /// <summary>
        ///     Get's the setting for the given client type and notification type.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/get-notification-setting-3649859 </para>
        /// </summary>
        /// <param name="clientType">Client type: "email" and "mobile".</param>
        /// <returns></returns>
        public async Task<Dictionary<string, bool>> GetNotificationSettings(string clientType)
        {
            string url = string.Format("/user/setting/{0}/", clientType);
            return  await _podio.Get<Dictionary<string, bool>>(url);
        }

        /// <summary>
        ///     Returns the field of the profile for the given key from the active user. For a list of valid keys and their
        ///     possible values, see the contact area.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/get-profile-field-22380 </para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<string>> GetProfileField(string key)
        {
            string url = string.Format("/user/profile/{0}", key);
            return  await _podio.Get<List<string>>(url);
        }

        /// <summary>
        ///     Gets the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/get-user-22378 </para>
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetUser()
        {
            string url = "/user";
            return  await _podio.Get<User>(url);
        }

        /// <summary>
        ///     Updates the setting for the given client type and notification type.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/update-notification-setting-3649918 </para>
        /// </summary>
        /// <param name="clientType">Client type: "email" and "mobile".</param>
        /// <param name="notificationType">
        ///     Notification type: "digest", "bulletin", "reference", "message", "space",
        ///     "subscription", "user", "reminder", "push_notification", "push_notification_sound" or "push_notification_browser".
        /// </param>
        /// <param name="value"></param>
        public async Task<dynamic> UpdateNotificationSetting(string clientType, string notificationType, bool value)
        {
            string url = string.Format("/user/setting/{0}/{1}", clientType, notificationType);
            dynamic requestData = new
            {
                value = value
            };
            return  await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Updates the setting for the given client type
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/update-notification-settings-3649927 </para>
        /// </summary>
        /// <param name="clientType">Client type: "email" and "mobile".</param>
        /// <param name="notificationTypes">
        ///     Notification type: "digest", "bulletin", "reference", "message", "space",
        ///     "subscription", "user", "reminder", "push_notification", "push_notification_sound" or "push_notification_browser".
        /// </param>
        public async Task<dynamic> UpdateNotificationSettings(string clientType, Dictionary<string, bool> notificationTypes)
        {
            string url = string.Format("/user/setting/{0}/", clientType);
            return  await _podio.Put<dynamic>(url, notificationTypes);
        }

        /// <summary>
        ///     Updates the fields of an existing profile. Fields not specified will not be updated. To delete a field set the
        ///     value of the field to null.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/update-profile-22402 </para>
        /// </summary>
        /// <param name="updatedProfile">The value or list of values for the given field. For a list of fields see the contact area</param>
        public async Task<dynamic> UpdateProfile(Contact updatedProfile)
        {
            string url = "/user/profile/";
            return  await _podio.Put<dynamic>(url, updatedProfile);
        }

        /// <summary>
        ///     Updates the specific field on the user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/update-profile-field-22500 </para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">The new value for the profile field.</param>
        public async Task<dynamic> UpdateProfileField(string key, string value)
        {
            string url = string.Format("/user/profile/{0}", key);
            dynamic requestData = new
            {
                value = value
            };
            return  await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Returns the current status for the user. This includes the user data, profile data and notification data.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/get-user-status-22480 </para>
        /// </summary>
        /// <returns></returns>
        public async Task<UserStatus> GetUserStatus()
        {
            string url = "/user/status";
            return  await _podio.Get<UserStatus>(url);
        }

        /// <summary>
        ///     Returns the value of the property for the active user with the given name. The property is specific to the auth
        ///     client used.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/get-user-property-29798 </para>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<JObject> GetUserProperty(string name)
        {
            string url = string.Format("/user/property/{0}", name);
            return  await _podio.Get<JObject>(url);
        }

        /// <summary>
        ///     Sets the value of the property for the active user with the given name. The property is specific to the auth client
        ///     used.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/set-user-property-29799 </para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public async Task<dynamic> SetUserProperty(string name, dynamic value)
        {
            string url = string.Format("/user/property/{0}", name);
            return  await _podio.Put<dynamic>(url, value);
        }

        /// <summary>
        ///     Sets the values of one or more properties for the active user. The properties are specific to the auth client used.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/users/set-user-properties-9052829 </para>
        /// </summary>
        /// <param name="properties">The JSON object value of the property</param>
        public async Task<dynamic> SetUserProperties(Dictionary<string, object> properties)
        {
            string url = "/user/property/";
            return  await _podio.Put<dynamic>(url, properties);
        }
    }
}