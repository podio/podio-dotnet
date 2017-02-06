using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Models.Request;
using PodioAPI.Utils;

namespace PodioAPI.Services
{
    public class SpaceMembersService
    {
        private readonly Podio _podio;

        public SpaceMembersService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Returns the active members of the given space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/get-active-members-of-space-22395 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public List<SpaceMember> GetActiveMembersOfSpace(int spaceId)
        {
            string url = string.Format("/space/{0}/member/", spaceId);
            return _podio.Get<List<SpaceMember>>(url);
        }

        /// <summary>
        ///     Returns the membership details for the given user on the given space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/get-space-member-20735097 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SpaceMember GetSpaceMember(int spaceId, int userId)
        {
            string url = string.Format("/space/{0}/member/{1}/v2", spaceId, userId);
            return _podio.Get<SpaceMember>(url);
        }

        /// <summary>
        ///     Returns the space members with the specified role.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/get-space-members-by-role-68043 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public List<SpaceMember> GetSpaceMembersByRole(int spaceId, string role)
        {
            string url = string.Format("/space/{0}/member/{1}/", spaceId, role);
            return _podio.Get<List<SpaceMember>>(url);
        }

        /// <summary>
        ///     Returns the members of the given space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/get-space-members-v2-19350328 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="memberType">
        ///     The type of members to return. Can be one of: employee, external, admin, regular, light,
        ///     guest.
        /// </param>
        /// <param name="query">Any search term to match.</param>
        /// <param name="limit">The maximum number of members to return. Default value: 100</param>
        /// <param name="offset">The offset into the member list. Default value: 0</param>
        /// <returns></returns>
        public List<SpaceMember> GetSpaceMembers(int spaceId, string memberType = null, string query = null,
            int limit = 100, int offset = 0)
        {
            string url = string.Format("/space/{0}/member/v2/", spaceId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToString()},
                {"member_type", memberType},
                {"offset", offset.ToString()},
                {"query", query}
            };
            return _podio.Get<List<SpaceMember>>(url, requestData);
        }

        /// <summary>
        ///     Used to get the details of an active users membership of a space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/get-space-membership-22397 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SpaceMember GetSpaceMembership(int spaceId, int userId)
        {
            string url = string.Format("/space/{0}/member/{1}", spaceId, userId);
            return _podio.Get<SpaceMember>(url);
        }

        /// <summary>
        ///     Joins the open space with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/join-space-1927286 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        public void JoinSpace(int spaceId)
        {
            string url = string.Format("/space/{0}/join", spaceId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Used to leave the space for the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/leave-space-19410457 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        public void LeaveSpace(int spaceId)
        {
            string url = string.Format("/space/{0}/leave", spaceId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Updates the space memberships with another role.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/update-space-memberships-22398 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="userIds"></param>
        /// <param name="role"></param>
        public void UpdateSpaceMemberships(int spaceId, int[] userIds, string role)
        {
            string userIdsCSV = Utilities.ArrayToCSV(userIds);
            string url = string.Format("/space/{0}/member/{1}", spaceId, userIdsCSV);
            dynamic requestData = new
            {
                role = role
            };
            _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Request access to a space you don't have access to. All admins of this space will get notified and can accept or
        ///     ignore it.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/request-space-membership-6146231 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        public void RequestSpaceMembership(int spaceId)
        {
            string url = string.Format("/space/{0}/member_request/", spaceId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Accept a request from another user to be added to the space.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/accept-space-membership-request-6146271 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="spaceMemberRequestId"></param>
        public void AcceptSpaceMembershipRequest(int spaceId, int spaceMemberRequestId)
        {
            string url = string.Format("/space/{0}/member_request/{1}/accept", spaceId, spaceMemberRequestId);
            _podio.Post<dynamic>(url);
        }

        /// <summary>
        ///     Adds a list of users (either through user_id or email) to the space. If the user limit is reached, status code 403
        ///     will be returned.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/add-member-to-space-1066259 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="addSpaceMemberRequest"></param>
        public void AddMemberToSpace(int spaceId, AddSpaceMemberRequest addSpaceMemberRequest)
        {
            string url = string.Format("/space/{0}/member/", spaceId);
            _podio.Post<dynamic>(url, addSpaceMemberRequest);
        }

        /// <summary>
        ///     Ends all the users membership on the space, can also be called for members in state invited.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/space-members/end-space-memberships-22399 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="userIds"></param>
        public void EndSpaceMemberships(int spaceId, int[] userIds)
        {
            string userIdsAsCSV = Utilities.ArrayToCSV(userIds);
            string url = string.Format("/space/{0}/member/{1}", spaceId, userIdsAsCSV);
            _podio.Delete<dynamic>(url);
        }
    }
}