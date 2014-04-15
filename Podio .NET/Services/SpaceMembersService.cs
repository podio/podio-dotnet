using PodioAPI.Models;
using PodioAPI.Models.Request;
using PodioAPI.Utils;
using System.Collections.Generic;

namespace PodioAPI.Services
{
    public class SpaceMembersService
    {
        private Podio _podio;
        public SpaceMembersService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Returns the active members of the given space.
        /// <para>Podio API Reference: https://developers.podio.com/doc/space-members/get-active-members-of-space-22395 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public List<SpaceMember> GetActiveMembersOfSpace(int spaceId)
        {
            string url = string.Format("/space/{0}/member/", spaceId);
            return _podio.Get<List<SpaceMember>>(url);
        }

        /// <summary>
        /// Returns the membership details for the given user on the given space.
        /// <para>Podio API Reference: https://developers.podio.com/doc/space-members/get-space-member-20735097 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SpaceMember GetSpaceMember(int spaceId, int userId)
        {
            string url = string.Format("/space/{0}/member/{1}/v2", spaceId,userId);
            return _podio.Get<SpaceMember>(url);
        }

        /// <summary>
        /// Returns the space members with the specified role.
        /// <para>Podio API Reference: https://developers.podio.com/doc/space-members/get-space-members-by-role-68043 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public List<SpaceMember> GetSpaceMembersByRole(int spaceId, string role)
        {
            string url = string.Format("/space/{0}/member/{1}/", spaceId, role);
            return _podio.Get<List<SpaceMember>>(url);
        }
    }
}
