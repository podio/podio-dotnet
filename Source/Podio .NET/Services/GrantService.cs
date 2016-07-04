using System.Collections.Generic;
using PodioAPI.Models;

namespace PodioAPI.Services
{
    public class GrantService
    {
        private readonly Podio _podio;

        public GrantService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the count of grants on the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/grants/count-grants-on-object-19275931 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public int CountGrantsOnObject(string refType, dynamic refId)
        {
            string url = string.Format("/grant/{0}/{1}/count", refType, refId);
            dynamic response = _podio.Get<dynamic>(url);
            return (int) response["count"];
        }

        /// <summary>
        ///     Returns the grants on the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/grants/get-grants-on-object-16491464 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public List<Grant> GetGrantsOnObject(string refType, dynamic refId)
        {
            string url = string.Format("/grant/{0}/{1}/", refType, refId);
            return _podio.Get<List<Grant>>(url);
        }

        /// <summary>
        ///     Return the grant information for the active user, if any
        ///     <para>Podio API Reference: https://developers.podio.com/doc/grants/get-own-grant-information-16490748 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        public Grant GetOwnGrantInformation(string refType, dynamic refId)
        {
            string url = string.Format("/grant/{0}/{1}/own", refType, refId);
            return _podio.Get<Grant>(url);
        }

        /// <summary>
        ///     Removes the grant from the given user on the given object.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/grants/remove-grant-16496711 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="userId"></param>
        public void RemoveGrant(string refType, dynamic refId, int userId)
        {
            string url = string.Format("/grant/{0}/{1}/{2}", refType, refId, userId);
            _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Returns all the grants for the user on the given space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/grants/get-grants-to-user-on-space-19389786 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Grant> GetGrantsToUserOnSpace(int spaceId, int userId)
        {
            string url = string.Format("/grant/space/{0}/user/{0}/", spaceId, userId);
            return _podio.Get<List<Grant>>(url);
        }

        /// <summary>
        ///     Returns all the grants for the current user on the given organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/grants/get-own-grants-on-org-22330891 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<Grant> GetOwnGrantsOnOrg(int orgId)
        {
            string url = string.Format("/grant/org/{0}/own/", orgId);
            return _podio.Get<List<Grant>>(url);
        }

        /// <summary>
        ///     Create a grant on the given object to the given users.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/grants/create-grant-16168841 </para>
        /// </summary>
        /// <param name="refType"></param>
        /// <param name="refId"></param>
        /// <param name="people">The list of people to grant access to. This is a list of contact identifiers</param>
        /// <param name="action">The action required of the people, either "view", "comment" or "rate", or left out</param>
        /// <param name="message">Any special message to the users</param>
        /// <returns></returns>
        public CreatedGrant CreateGrant(string refType, int refId, List<Ref> people, string action,
            string message = null)
        {
            string url = string.Format("/grant/{0}/{1}", refType, refId);
            dynamic requestData = new
            {
                people = people,
                action = action,
                message = message
            };
            return _podio.Post<CreatedGrant>(url, requestData);
        }
    }
}