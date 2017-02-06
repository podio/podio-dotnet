using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class StreamService
    {
        private readonly Podio _podio;

        public StreamService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the global stream. The types of objects in the stream can be either "item", "status", "task", "action" or
        ///     "file". The data part of the result depends on the type of object. See area for more deatils.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-global-stream-80012 </para>
        /// </summary>
        /// <param name="limit">How many objects should be returned. Default: 10</param>
        /// <param name="offset">How far should the objects be offset</param>
        /// <returns></returns>
        public IEnumerable<StreamObject> GetGlobalStream(int? limit = null, int? offset = null)
        {
            string url = "/stream/";
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()}
            };

            return _podio.Get<List<StreamObject>>(url, requestData);
        }

        /// <summary>
        ///     Returns the stream for the given app. This includes items from the app and tasks on the app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-app-stream-264673 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="limit">How many objects should be returned. Default: 10</param>
        /// <param name="offset">How far should the objects be offset</param>
        /// <returns></returns>
        public IEnumerable<StreamObject> GetAppStream(int appId, int? limit = null, int? offset = null)
        {
            string url = string.Format("/stream/app/{0}/", appId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()}
            };

            return _podio.Get<List<StreamObject>>(url, requestData);
        }

        /// <summary>
        ///     Returns the activity stream for the space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-space-stream-80039 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="limit">How many objects should be returned. Default: 10</param>
        /// <param name="offset">How far should the objects be offset </param>
        /// <returns></returns>
        public IEnumerable<StreamObject> GetSpaceStream(int spaceId, int? limit = null, int? offset = null)
        {
            string url = string.Format("/stream/space/{0}/", spaceId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()}
            };

            return _podio.Get<List<StreamObject>>(url, requestData);
        }

        /// <summary>
        ///     Returns the stream for the given user. This returns all objects the active user has access to sorted by the given
        ///     user last touched the object.
        ///     <para>Podio API Reference:https://developers.podio.com/doc/stream/get-user-stream-1289318 </para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="limit">How many objects should be returned. Default: 10</param>
        /// <param name="offset">How far should the objects be offset </param>
        /// <returns></returns>
        public IEnumerable<StreamObject> GetUserStream(int userId, int? limit = null, int? offset = null)
        {
            string url = string.Format("/stream/user/{0}/", userId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()}
            };

            return _podio.Get<List<StreamObject>>(url, requestData);
        }

        /// <summary>
        ///     Returns the activity stream for the given organization.
        ///     <para>Podio API Reference:https://developers.podio.com/doc/stream/get-organization-stream-80038 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="limit">How many objects should be returned. Default: 10</param>
        /// <param name="offset">How far should the objects be offset </param>
        /// <returns></returns>
        public IEnumerable<StreamObject> GetOrganizationStream(int orgId, int? limit = null, int? offset = null)
        {
            string url = string.Format("/stream/org/{0}/", orgId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()}
            };

            return _podio.Get<List<StreamObject>>(url, requestData);
        }

        /// <summary>
        ///     Returns an object of type "item", "status" or "task" as a stream object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-stream-object-80054 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public StreamObject GetStreamObject(string refType, int refId)
        {
            string url = string.Format("/stream/{0}/{1}", refType, refId);
            return _podio.Get<StreamObject>(url);
        }

        /// <summary>
        ///     Get global stream - version 3
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-global-stream-v3-100405451 </para>
        /// </summary>
        /// <param name="groupsEventTypes">The types of events to include in the returned activity groups.</param>
        /// <param name="groupsLimit">Default value: 2</param>
        /// <param name="limit">Maximum number of groups returned. Default value: 10</param>
        /// <param name="offset">Index of first returned group. Default value: 0</param>
        /// <returns></returns>
        public List<StreamObjectV3> GetGlobalStreamV3(string[] groupsEventTypes = null, int groupsLimit = 2,
            int limit = 10, int offset = 0)
        {
            string url = "/stream/v3/";
            string groupEventTypesCSV = Utilities.ArrayToCSV(groupsEventTypes);
            var requestData = new Dictionary<string, string>
            {
                {"groups_event_types", groupEventTypesCSV},
                {"groups_limit", groupsLimit.ToString()},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()}
            };
            return _podio.Get<List<StreamObjectV3>>(url, requestData);
        }

        /// <summary>
        ///     Get stream for an application
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-application-stream-v3-100406563 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="groupsEventTypes">The types of events to include in the returned activity groups.</param>
        /// <param name="groupsLimit">Default value: 2</param>
        /// <param name="limit">Maximum number of groups returned. Default value: 10</param>
        /// <param name="offset">Index of first returned group. Default value: 0</param>
        /// <returns></returns>
        public List<StreamObjectV3> GetApplicationStreamV3(int appId, string[] groupsEventTypes = null,
            int groupsLimit = 2, int limit = 10, int offset = 0)
        {
            string url = string.Format("/stream/app/{0}/v3", appId);
            string groupEventTypesCSV = Utilities.ArrayToCSV(groupsEventTypes);
            var requestData = new Dictionary<string, string>
            {
                {"groups_event_types", groupEventTypesCSV},
                {"groups_limit", groupsLimit.ToString()},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()}
            };
            return _podio.Get<List<StreamObjectV3>>(url, requestData);
        }

        /// <summary>
        ///     Get stream for an organization - version 3
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-org-stream-v3-100405933 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="groupsEventTypes">The types of events to include in the returned activity groups.</param>
        /// <param name="groupsLimit">Default value: 2</param>
        /// <param name="limit">Maximum number of groups returned. Default value: 10</param>
        /// <param name="offset">Index of first returned group. Default value: 0</param>
        /// >
        /// <returns></returns>
        public List<StreamObjectV3> GetOrgStreamV3(int orgId, string[] groupsEventTypes = null, int groupsLimit = 2,
            int limit = 10, int offset = 0)
        {
            string url = string.Format("/stream/org/{0}/v3/", orgId);
            string groupEventTypesCSV = Utilities.ArrayToCSV(groupsEventTypes);
            var requestData = new Dictionary<string, string>
            {
                {"groups_event_types", groupEventTypesCSV},
                {"groups_limit", groupsLimit.ToString()},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()}
            };
            return _podio.Get<List<StreamObjectV3>>(url, requestData);
        }

        /// <summary>
        ///     Get stream for a space - version 3
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-space-stream-v3-116373969 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="groupsEventTypes">The types of events to include in the returned activity groups.</param>
        /// <param name="groupsLimit">Default value: 2</param>
        /// <returns></returns>
        public List<StreamObjectV3> GetSpaceStreamV3(int spaceId, string[] groupsEventTypes = null, int groupsLimit = 2)
        {
            string url = string.Format("/stream/space/{0}/v3/", spaceId);
            string groupEventTypesCSV = Utilities.ArrayToCSV(groupsEventTypes);
            var requestData = new Dictionary<string, string>
            {
                {"groups_event_types", groupEventTypesCSV},
                {"groups_limit", groupsLimit.ToString()}
            };
            return _podio.Get<List<StreamObjectV3>>(url, requestData);
        }

        /// <summary>
        ///     Get stream for an user - version 3
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-user-stream-v3-100406822 </para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupsEventTypes">The types of events to include in the returned activity groups.</param>
        /// <param name="groupsLimit">Default value: 2</param>
        /// <param name="limit">Maximum number of groups returned. Default value: 10</param>
        /// <param name="offset">Index of first returned group. Default value: 0</param>
        /// >
        /// <returns></returns>
        public List<StreamObjectV3> GetUserStreamV3(int userId, string[] groupsEventTypes = null, int groupsLimit = 2,
            int limit = 10, int offset = 0)
        {
            string url = string.Format("/stream/user/{0}/v3", userId);
            string groupEventTypesCSV = Utilities.ArrayToCSV(groupsEventTypes);
            var requestData = new Dictionary<string, string>
            {
                {"groups_event_types", groupEventTypesCSV},
                {"groups_limit", groupsLimit.ToString()},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()}
            };
            return _podio.Get<List<StreamObjectV3>>(url, requestData);
        }

        /// <summary>
        ///     Get stream view of an object - version 3.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/stream/get-stream-object-v3-116373396 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="groupsEventTypes">The types of events to include in the returned activity groups.</param>
        /// <param name="groupsLimit">Default value: 2</param>
        /// <returns></returns>
        public List<StreamObjectV3> GetStreamObjectV3(string refType, int refId, string[] groupsEventTypes = null,
            int groupsLimit = 2)
        {
            string url = string.Format("/stream/{0}/{1}/v3", refType, refId);
            string groupEventTypesCSV = Utilities.ArrayToCSV(groupsEventTypes);
            var requestData = new Dictionary<string, string>
            {
                {"groups_event_types", groupEventTypesCSV},
                {"groups_limit", groupsLimit.ToString()}
            };
            return _podio.Get<List<StreamObjectV3>>(url, requestData);
        }
    }
}