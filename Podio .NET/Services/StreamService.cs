using PodioAPI.Models;
using System.Collections.Generic;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class StreamService
    {
        private Podio _podio;
        public StreamService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Returns the global stream. The types of objects in the stream can be either "item", "status", "task", "action" or "file". The data part of the result depends on the type of object. See area for more deatils.
        /// <para>Podio API Reference: https://developers.podio.com/doc/stream/get-global-stream-80012 </para>
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
        /// Returns the stream for the given app. This includes items from the app and tasks on the app.
        /// <para>Podio API Reference: https://developers.podio.com/doc/stream/get-app-stream-264673 </para>
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
        /// Returns the activity stream for the space.
        /// <para>Podio API Reference: https://developers.podio.com/doc/stream/get-space-stream-80039 </para>
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
        /// Returns the stream for the given user. This returns all objects the active user has access to sorted by the given user last touched the object.
        /// <para>Podio API Reference:https://developers.podio.com/doc/stream/get-user-stream-1289318 </para>
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
        /// Returns the activity stream for the given organization.
        /// <para>Podio API Reference:https://developers.podio.com/doc/stream/get-organization-stream-80038 </para>
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

    }
}
