using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class TagService
    {
        private readonly Podio _podio;

        public TagService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Adds additional tags to the object. If a tag with the same text is already present, the tag will be ignored.
        ///     Existing tags on the object are preserved.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/create-tags-22464 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="tags"></param>
        public void CreateTags(string refType, int refId, List<string> tags)
        {
            string url = string.Format("/tag/{0}/{1}/", refType, refId);
            _podio.Post<dynamic>(url, tags);
        }

        /// <summary>
        ///     Returns the objects that are tagged with the given text on the app. The objects are returned sorted descending by
        ///     the time the tag was added.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/get-objects-on-app-with-tag-22469 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="text">The tag to search for.</param>
        /// <returns></returns>
        public List<TaggedObject> GetObjectsOnAppWithTag(int appId, string text)
        {
            string url = string.Format("/tag/app/{0}/search/", appId);
            var requestData = new Dictionary<string, string>
            {
                {"text", text}
            };
            return _podio.Get<List<TaggedObject>>(url, requestData);
        }

        /// <summary>
        ///     Returns the objects that are tagged with the given text on the organization. The objects are returned sorted
        ///     descending by the time the tag was added.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/get-objects-on-organization-with-tag-48478 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<TaggedObject> GetObjectsOnOrganizationWithTag(int orgId, string text)
        {
            string url = string.Format("/tag/org/{0}/search/", orgId);
            var requestData = new Dictionary<string, string>
            {
                {"text", text}
            };
            return _podio.Get<List<TaggedObject>>(url, requestData);
        }

        /// <summary>
        ///     Returns the objects that are tagged with the given text on the space. The objects are returned sorted descending by
        ///     the time the tag was added.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/get-objects-on-space-with-tag-22468 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<TaggedObject> GetObjectsOnSpaceWithTag(int spaceId, string text)
        {
            string url = string.Format("/tag/space/{0}/search/", spaceId);
            var requestData = new Dictionary<string, string>
            {
                {"text", text}
            };
            return _podio.Get<List<TaggedObject>>(url, requestData);
        }

        /// <summary>
        ///     Returns the tags on the given app. This includes only items. The tags are first limited ordered by their frequency
        ///     of use, and then returned sorted alphabetically.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/get-tags-on-app-22467 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="limit">The maximum number of tags to return.</param>
        /// <param name="text">The tag to search for.</param>
        /// <returns></returns>
        public List<Tag> GetTagsOnApp(int appId, int? limit = null, string text = null)
        {
            string url = string.Format("/tag/app/{0}/", appId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"text", text}
            };
            return _podio.Get<List<Tag>>(url, requestData);
        }

        /// <summary>
        ///     Returns the top tags on the app.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/get-tags-on-app-top-68485 </para>
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="limit">The maximum number of tags to return</param>
        /// <param name="text">The tag to search for</param>
        /// <returns></returns>
        public List<string> GetTagsOnAppTop(int appId, int? limit = null, string text = null)
        {
            string url = string.Format("/tag/app/{0}/top/", appId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"text", text}
            };
            return _podio.Get<List<string>>(url, requestData);
        }

        /// <summary>
        ///     Returns the tags on the given org. This includes both items and statuses on all spaces in the organization that the
        ///     user is part of. The tags are first limited ordered by their frequency of use, and then returned sorted
        ///     alphabetically.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/get-tags-on-organization-48473 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="limit">The maximum number of tags to return</param>
        /// <param name="text">The tag to search for</param>
        /// <returns></returns>
        public List<Tag> GetTagsOnOrganization(int orgId, int? limit = null, string text = null)
        {
            string url = string.Format("/tag/org/{0}/", orgId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"text", text}
            };
            return _podio.Get<List<Tag>>(url, requestData);
        }

        /// <summary>
        ///     Returns the tags on the given space. This includes both items and statuses. The tags are first limited ordered by
        ///     their frequency of use, and then returned sorted alphabetically.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/get-tags-on-space-22466 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="limit">The maximum number of tags to return</param>
        /// <param name="text">The tag to search for</param>
        /// <returns></returns>
        public List<Tag> GetTagsOnSpace(int spaceId, int? limit = null, string text = null)
        {
            string url = string.Format("/tag/space/{0}/", spaceId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"text", text}
            };
            return _podio.Get<List<Tag>>(url, requestData);
        }

        /// <summary>
        ///     Removes a single tag from an object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/remove-tag-22465 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="text">The tag to search for</param>
        public void RemoveTag(string refType, int refId, string text)
        {
            string url = string.Format("/tag/{0}/{1}?text={2}", refType, refId, text);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Updates the tags on the given object. Existing tags on the object will be overwritten. Use Create Tags operation to
        ///     preserve existing tags.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/tags/update-tags-39859 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="tags"></param>
        public void UpdateTags(string refType, int refId, List<string> tags)
        {
            string url = string.Format("/tag/{0}/{1}/", refType, refId);
            _podio.Put<dynamic>(url, tags);
        }
    }
}