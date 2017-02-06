using System.Collections.Generic;
using System.Linq;
using PodioAPI.Models;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class ContactService
    {
        private readonly Podio _podio;

        public ContactService(Podio currentInstance)
        {
            _podio = currentInstance;
        }


        /// <summary>
        ///     Creates a new space contact for use by everyone on the space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/create-space-contact-65590 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="contact"></param>
        /// <returns>profile_id of the created contact</returns>
        public int CreateContact(int spaceId, Contact contact)
        {
            string url = string.Format("/contact/space/{0}/", spaceId);
            dynamic response = _podio.Post<dynamic>(url, contact);
            return (int) response["profile_id"];
        }

        /// <summary>
        ///     Updates the contact with the given profile id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/update-contact-60556 </para>
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="contact"></param>
        public void UpdateContact(int profileId, Contact contact)
        {
            string url = string.Format("/contact/{0}", profileId);
            _podio.Put<dynamic>(url, contact);
        }

        /// <summary>
        ///     Deletes the contact(s) with the given id(s). It is currently only allowed to delete contacts of type "space".
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/delete-contact-s-60560 </para>
        /// </summary>
        /// <param name="profileIds"></param>
        public void DeleteContacts(int[] profileIds)
        {
            string profileIdCSV = Utilities.ArrayToCSV(profileIds);
            string url = string.Format("/contact/{0}", profileIdCSV);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Returns the total number of contacts for the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-contact-totals-v3-34629208 </para>
        /// </summary>
        /// <returns></returns>
        public ContactTotal GetContactTotals()
        {
            string url = "/contact/totals/v3/";
            return _podio.Get<ContactTotal>(url);
        }

        /// <summary>
        ///     Returns the skills of related contacts, ordered by most frequently used.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-skills-1346872 </para>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<string> GetSkills(string text, int limit = 12)
        {
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"text", text}
            };
            string url = "/contact/skill/";
            return _podio.Get<List<string>>(url, requestData);
        }

        /// <summary>
        ///     Returns the total number of contacts on the space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-space-contact-totals-67508 </para>
        /// </summary>
        /// <param name="SpaceId"></param>
        /// <returns></returns>
        public int GetSpaceContactTotals(int SpaceId)
        {
            string url = string.Format("/contact/space/{0}/totals/space", SpaceId);
            dynamic response = _podio.Get<dynamic>(url);
            return (int) response["total"];
        }

        /// <summary>
        ///     Returns the contact with the given user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Contact GetUserContact(int userId)
        {
            string url = string.Format("/contact/user/{0}", userId);
            return _podio.Get<Contact>(url);
        }

        /// <summary>
        ///     Returns all the contact details about the contact(s) with the given profile id(s).
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-contact-s-22335 </para>
        /// </summary>
        /// <param name="profileIds"></param>
        /// <param name="spaceId">If set the role and removable property will be set in the context of the given space.</param>
        /// <returns></returns>
        public List<Contact> GetContactsByProfileId(int[] profileIds, int? spaceId = null)
        {
            string profileIdCSV = Utilities.ArrayToCSV(profileIds);
            string url = string.Format("/contact/{0}/v2", profileIdCSV);
            var requestData = new Dictionary<string, string>
            {
                {"space_id", spaceId.ToStringOrNull()}
            };
            var contacts = new List<Contact>();

            if (profileIds.Length > 1)
                contacts = _podio.Get<List<Contact>>(url, requestData);
            else
                contacts.Add(_podio.Get<Contact>(url, requestData));

            return contacts;
        }

        /// <summary>
        ///     Used to get a list of contacts for the user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-contacts-22400 </para>
        /// </summary>
        /// <param name="fields">A value for the required field. For text fields partial matches will be returned.</param>
        /// <param name="contactType">
        ///     The types of contacts to return, can be either "user" or "space". Separate by comma to get
        ///     multiple types, or leave blank for all contacts. Default value: user
        /// </param>
        /// <param name="externalId">The external id of the contact</param>
        /// <param name="limit">The maximum number of contacts that should be returned</param>
        /// <param name="offset">The offset to use when returning contacts</param>
        /// <param name="required">
        ///     A comma-separated list of fields that should exist for the contacts returned. Useful for only
        ///     getting contacts with an email address or phone number.
        /// </param>
        /// <param name="excludeSelf">True to exclude self, False to include self. Default value: true</param>
        /// <param name="order">
        ///     The order in which the contacts can be returned. See the area for details on the ordering options.
        ///     Default value: name
        /// </param>
        /// <param name="type">Determines the way the result is returned. Valid options are "mini" and "full". Default value: mini</param>
        /// <returns></returns>
        public List<Contact> GetAllContacts(Dictionary<string, string> fields = null, string contactType = "user",
            string externalId = null, int? limit = null, int? offset = null, string required = null,
            bool excludeSelf = true, string order = "name", string type = "mini")
        {
            string url = "/contact/";
            var requestData = new Dictionary<string, string>();
            var parameters = new Dictionary<string, string>
            {
                {"contact_type", contactType.ToStringOrNull()},
                {"exclude_self", excludeSelf.ToStringOrNull()},
                {"external_id", externalId.ToStringOrNull()},
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()},
                {"order", order.ToStringOrNull()},
                {"required", required.ToStringOrNull()},
                {"type", type.ToStringOrNull()}
            };

            if (fields != null && fields.Any())
                requestData = parameters.Concat(fields).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            else
                requestData = parameters;

            return _podio.Get<List<Contact>>(url, requestData);
        }

        /// <summary>
        ///     UsedReturns all the profiles of the users contacts on the given organization. For the details of the possible
        ///     return values, see the area.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-organization-contacts-22401 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="fields">A value for the required field. For text fields partial matches will be returned.</param>
        /// <param name="contactType">
        ///     The types of contacts to return, can be either "user" or "space". Separate by comma to get
        ///     multiple types, or leave blank for all contacts. Default value: user
        /// </param>
        /// <param name="externalId">The external id of the contact</param>
        /// <param name="limit">The maximum number of contacts that should be returned</param>
        /// <param name="offset">The offset to use when returning contacts</param>
        /// <param name="required">
        ///     A comma-separated list of fields that should exist for the contacts returned. Useful for only
        ///     getting contacts with an email address or phone number.
        /// </param>
        /// <param name="excludeSelf">True to exclude self, False to include self. Default value: true</param>
        /// <param name="order">
        ///     The order in which the contacts can be returned. See the area for details on the ordering options.
        ///     Default value: name
        /// </param>
        /// <param name="type">Determines the way the result is returned. Valid options are "mini" and "full". Default value: mini</param>
        /// <returns></returns>
        public List<Contact> GetOrganizationContacts(int orgId, Dictionary<string, string> fields = null,
            string contactType = "user", string externalId = null, int? limit = null, int? offset = null,
            string required = null, bool excludeSelf = true, string order = "name", string type = "mini")
        {
            string url = string.Format("/contact/org/{0}", orgId);
            var requestData = new Dictionary<string, string>();
            var parameters = new Dictionary<string, string>
            {
                {"contact_type", contactType.ToStringOrNull()},
                {"exclude_self", excludeSelf.ToStringOrNull()},
                {"external_id", externalId.ToStringOrNull()},
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()},
                {"order", order.ToStringOrNull()},
                {"required", required.ToStringOrNull()},
                {"type", type.ToStringOrNull()}
            };

            if (fields != null && fields.Any())
                requestData = parameters.Concat(fields).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            else
                requestData = parameters;

            return _podio.Get<List<Contact>>(url, requestData);
        }

        /// <summary>
        ///     Returns all the profiles of the users contacts on the given space. For the details of the possible return values,
        ///     see the area.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-space-contacts-22414 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="fields">A value for the required field. For text fields partial matches will be returned.</param>
        /// <param name="contactType">
        ///     The types of contacts to return, can be either "user" or "space". Separate by comma to get
        ///     multiple types, or leave blank for all contacts. Default value: user
        /// </param>
        /// <param name="externalId">The external id of the contact</param>
        /// <param name="limit">The maximum number of contacts that should be returned</param>
        /// <param name="offset">The offset to use when returning contacts</param>
        /// <param name="required">
        ///     A comma-separated list of fields that should exist for the contacts returned. Useful for only
        ///     getting contacts with an email address or phone number.
        /// </param>
        /// <param name="excludeSelf">True to exclude self, False to include self. Default value: true</param>
        /// <param name="order">
        ///     The order in which the contacts can be returned. See the area for details on the ordering options.
        ///     Default value: name
        /// </param>
        /// <param name="type">Determines the way the result is returned. Valid options are "mini" and "full". Default value: mini</param>
        /// <returns></returns>
        public List<Contact> GetSpaceContacts(int spaceId, Dictionary<string, string> fields = null,
            string contactType = "user", string externalId = null, int? limit = null, int? offset = null,
            string required = null, bool excludeSelf = true, string order = "name", string type = "mini")
        {
            string url = string.Format("/contact/space/{0}/", spaceId);
            var requestData = new Dictionary<string, string>();
            var parameters = new Dictionary<string, string>
            {
                {"contact_type", contactType.ToStringOrNull()},
                {"exclude_self", excludeSelf.ToStringOrNull()},
                {"external_id", externalId.ToStringOrNull()},
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()},
                {"order", order.ToStringOrNull()},
                {"required", required.ToStringOrNull()},
                {"type", type.ToStringOrNull()}
            };

            if (fields != null && fields.Any())
                requestData = parameters.Concat(fields).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            else
                requestData = parameters;

            return _podio.Get<List<Contact>>(url, requestData);
        }

        /// <summary>
        ///     Returns all the space contacts referenced by the given app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-space-contacts-on-app-79475279 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="fields">A value for the required field. For text fields partial matches will be returned.</param>
        /// <param name="limit">The maximum number of contacts that should be returned</param>
        /// <param name="offset">The offset to use when returning contacts</param>
        /// <param name="order">
        ///     The order in which the contacts can be returned. See the area for details on the ordering options.
        ///     Default value: name
        /// </param>
        /// <returns></returns>
        public List<Contact> GetSpaceContactsOnApp(int appId, Dictionary<string, string> fields = null,
            int? limit = null, int? offset = null, string order = "name")
        {
            string url = string.Format("/contact/app/{0}/", appId);
            var requestData = new Dictionary<string, string>();
            var parameters = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()},
                {"order", order.ToStringOrNull()}
            };

            if (fields != null && fields.Any())
                requestData = parameters.Concat(fields).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            else
                requestData = parameters;

            return _podio.Get<List<Contact>>(url, requestData);
        }

        /// <summary>
        ///     Returns the value of a contact with the specific field. For the possible keys to use, see the area.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/contacts/get-user-contact-field-22403 </para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetUserContactField(int userId, string key)
        {
            string url = string.Format("/contact/user/{0}/{1}", userId, key);
            return _podio.Get<List<string>>(url);
        }

        /// <summary>
        ///     Returns the vCard for the given contact.
        ///     <para>API Reference: https://developers.podio.com/doc/contacts/get-vcard-213496 </para>
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public string GetvCard(int profileId)
        {
            string url = string.Format("/contact/{0}/vcard", profileId);
            var options = new Dictionary<string, bool>
            {
                {"return_raw", true}
            };
            return _podio.Get<dynamic>(url, options: options);
        }

        /// <summary>
        ///     Updates the given field on the given contact. Updates are currently only allowed from contacts of type "space".
        ///     <para>API Reference: https://developers.podio.com/doc/contacts/update-contact-field-60558 </para>
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="key"></param>
        /// <param name="value">The new value for the profile field.</param>
        public void UpdateContactField(int profileId, string key, string value)
        {
            string url = string.Format("/contact/{0}/{1}", profileId, key);
            dynamic requestData = new
            {
                value = value
            };
            _podio.Put<dynamic>(url, requestData);
        }
    }
}