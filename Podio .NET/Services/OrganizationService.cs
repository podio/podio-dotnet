using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Utils;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class OrganizationService
    {
        private readonly Podio _podio;

        public OrganizationService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Creates a new organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/add-new-organization-22385 </para>
        /// </summary>
        /// <param name="name">The name of the new organization</param>
        /// <param name="logo">The file id of the logo of the organization</param>
        /// <returns></returns>
        public async Task<Organization> AddNewOrganization(string name, int logo)
        {
            string url = "/org/";
            dynamic requestData = new
            {
                name = name,
                logo = logo
            };
            return await _podio.Post<Organization>(url, requestData);
        }

        /// <summary>
        ///     Adds a new administrator to the organization. The user must be administrator to perform this operation.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/add-organization-admin-50854 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="userId">The id of the user to be made administrator</param>
        public async Task<dynamic> AddOrganizationAdmin(int organizationId, int userId)
        {
            string url = string.Format("/org/{0}/admin/", organizationId);
            dynamic requestData = new
            {
                user_id = userId
            };
            return await _podio.Post<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Creates an app store profile for the organization if it doesn't already exist.
        ///     <para>
        ///         Podio API Reference:
        ///         https://developers.podio.com/doc/organizations/create-organization-app-store-profile-87819
        ///     </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="profileData"></param>
        /// <returns></returns>
        public async Task<int?> CreateOrganizationAppStoreProfile(int organizationId, Contact profileData)
        {
            string url = string.Format("/org/{0}/appstore", organizationId);
            dynamic response = await _podio.Post<dynamic>(url, profileData);

            if (response != null)
                return (int) response["profile_id"];
            else
                return null;
        }

        /// <summary>
        ///     Deletes the organizations app store profile.
        ///     <para>
        ///         Podio API Reference:
        ///         https://developers.podio.com/doc/organizations/delete-organization-app-store-profile-87808
        ///     </para>
        /// </summary>
        /// <param name="organizationId"></param>
        public async Task<dynamic> DeleteOrganizationAppStoreProfile(int organizationId)
        {
            string url = string.Format("/org/{0}/appstore", organizationId);
            return await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Removes the role from the given user on the given organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/delete-organization-member-role-935217 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="userId"></param>
        public async Task<dynamic> DeleteOrganizationMemberRole(int organizationId, int userId)
        {
            string url = string.Format("/org/{0}/member/{1}/role", organizationId, userId);
            return await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Returns the member data for the given user in the given organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-organization-member-50908 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OrganizationMember> GetMember(int organizationId, int userId)
        {
            string url = string.Format("/org/{0}/member/{1}", organizationId, userId);
            return await _podio.Get<OrganizationMember>(url);
        }

        /// <summary>
        ///     Returns the members, both invited and active, of the given organization. This method is only available for
        ///     organization administrators.
        ///     <para> Podio API Reference: https://developers.podio.com/doc/organizations/get-organization-members-50661 </para>
        /// </summary>
        /// <param name="organizationId"> </param>
        /// <param name="memberType">
        ///     The type of members that should be returned Possible values are: employee, external, admin,
        ///     regular, guest, premium
        /// </param>
        /// <param name="query">Any search term to match.</param>
        /// <param name="limit">The maximum number of items to return </param>
        /// <param name="offset">The offset to use when returning the list Default value: 0 </param>
        /// <param name="sortBy">The sorting order of the results returned. Valid options are "name" and "last_seen_on" </param>
        /// <param name="sortDesc">True if the results should be sorted descending, false otherwise. Default value: false </param>
        /// <returns></returns>
        public async Task<List<OrganizationMember>> GetMembers(int organizationId, string memberType = null, string query = null,
            int? limit = null, int? offset = null, string sortBy = null, bool sortDesc = false)
        {
            string url = string.Format("/org/{0}/member/", organizationId);
            var requestData = new Dictionary<string, string>
            {
                {"limit", limit.ToStringOrNull()},
                {"offset", offset.ToStringOrNull()},
                {"member_type", memberType},
                {"query", query},
                {"sort_by", sortBy},
                {"sort_desc", sortDesc.ToString()},
            };
            return await _podio.Get<List<OrganizationMember>>(url, requestData);
        }

        /// <summary>
        ///     Returns a list of all the organizations and spaces the user is member of.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-organizations-22344 </para>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Organization>> GetOrganizations()
        {
            string url = "/org/";
            return await _podio.Get<List<Organization>>(url);
        }

        /// <summary>
        ///     Gets the organization with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-organization-22383 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public async Task<Organization> GetOrganization(int organizationId)
        {
            string url = string.Format("/org/{0}", organizationId);
            return await _podio.Get<Organization>(url);
        }

        /// <summary>
        ///     Returns the organization with the given full URL. The URL does not have to be truncated to the root, it can be to
        ///     any resource on the URL.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-organization-by-url-22384 </para>
        /// </summary>
        /// <param name="orgUrl"></param>
        /// <returns></returns>
        public async Task<Organization> GetOrganizationByURL(string orgUrl)
        {
            string url = "/org/url";
            var requestData = new Dictionary<string, string>()
            {
                {"url", orgUrl}
            };
            return await _podio.Get<Organization>(url, requestData);
        }

        /// <summary>
        ///     Returns the administrators of the organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-organization-admins-81542 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public async Task<List<User>> GetOrganizationAdmins(int organizationId)
        {
            string url = string.Format("/org/{0}/admin/", organizationId);
            return await _podio.Get<List<User>>(url);
        }

        /// <summary>
        ///     Gets the appstore profile of an organization, if any.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-organization-app-store-profile-87799 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public async Task<Contact> GetOrganizationAppStoreProfile(int organizationId)
        {
            string url = string.Format("/org/{0}/appstore", organizationId);
            return await _podio.Get<Contact>(url);
        }

        /// <summary>
        ///     Returns the login report for the organization. This reports list the total number of users and the total number of
        ///     active users per week.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-organization-login-report-51730 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="limit">
        ///     The number of weeks to return counting backwards from the previous week, but no more than 16. It
        ///     will also never return data older than when the organization was created. Default value: 4
        /// </param>
        /// <param name="offset">The offset into the weeks to return. Default value: 0</param>
        /// <returns></returns>
        public async Task<List<OrganizationLoginReport>> GetOrganizationLoginReport(int organizationId, int limit = 4,
            int offset = 0)
        {
            string url = string.Format("/org/{0}/report/login/", organizationId);
            var requestData = new Dictionary<string, string>()
            {
                {"limit", limit.ToString()},
                {"offset", offset.ToString()}
            };
            return await _podio.Get<List<OrganizationLoginReport>>(url, requestData);
        }

        /// <summary>
        ///     Gets the billing profile of an organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-organization-billing-profile-51370 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public async Task<Contact> GetOrganizationBillingProfile(int organizationId)
        {
            string url = string.Format("/org/{0}/billing", organizationId);
            return await _podio.Get<Contact>(url);
        }

        /// <summary>
        ///     Returns all the spaces for the organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-spaces-on-organization-22387 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public async Task<List<Space>> GetSpacesOnOrganization(int organizationId)
        {
            string url = string.Format("/org/{0}/space/", organizationId);
            return await _podio.Get<List<Space>>(url);
        }

        /// <summary>
        ///     Removes the user as administrator from the organization. The active user must be an administrator of the
        ///     organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/organizations/remove-organization-admin-50855 </para>
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="userId"></param>
        public async Task<dynamic> RemoveOrganizationAdmin(int organizationId, int userId)
        {
            string url = string.Format("/org/{0}/admin/{1}", organizationId, userId);
            return await _podio.Delete<dynamic>(url);
        }

        /// <summary>
        ///     Updates an organization with new name and logo. Note that the URL of the organization will not change even though
        ///     the name changes.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="name">The name of the new organization</param>
        /// <param name="urlLabel">The new subdomain of the URL of the organization, defaults to the existing URL</param>
        /// <param name="logo">The file id of the logo of the organization</param>
        public async Task<dynamic> UpdateOrganization(int organizationId, string name = null, string urlLabel = null, int? logo = null)
        {
            string url = string.Format("/org/{0}", organizationId);
            dynamic requestData = new
            {
                name = name,
                url_label = urlLabel,
                logo = logo
            };
            return await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Updates the appstore profile of the organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="profileData">The value or list of values for the given field. For a list of fields see the contact area</param>
        public async Task<dynamic> UpdateOrganizationAppStoreProfile(int organizationId, Contact profileData)
        {
            string url = string.Format("/org/{0}/appstore", organizationId);
            return await _podio.Put<dynamic>(url, profileData);
        }

        /// <summary>
        ///     Updates the billing profile of the organization. The profile is used for billing and contact information.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="profileData">The value or list of values for the given field. For a list of fields see the contact area</param>
        public async Task<dynamic> UpdateOrganizationBillingProfile(int organizationId, Contact profileData)
        {
            string url = string.Format("/org/{0}/billing", organizationId);
            return await _podio.Put<dynamic>(url, profileData);
        }

        /// <summary>
        ///     Returns all space memberships the specified org member has in this organization. If the org admin requesting this
        ///     information is not a member of any of these workspaces, sensitive information like name and url will not be
        ///     exposed.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<SpaceMember>> GetSpaceMembershipsForOrgMember(int organizationId, int userId)
        {
            string url = string.Format("/org/{0}/member/{1}/space_member/", organizationId, userId);
            return await _podio.Get<List<SpaceMember>>(url);
        }

        /// <summary>
        ///     Returns all space memberships the specified org member has in this organization. If the org admin requesting this
        ///     information is not a member of any of these workspaces, sensitive information like name and url will not be
        ///     exposed.
        /// </summary>
        /// <param name="organizationId"></param>
        /// Returns the organizations and spaces that the logged in user shares with the specified user.
        /// <para>Podio API Reference: https://developers.podio.com/doc/organizations/get-shared-organizations-22411 </para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Organization>> GetSharedOrganizations(int userId)
        {
            string url = string.Format("/org/shared/{0}", userId);
            return await _podio.Get<List<Organization>>(url);
        }
    }
}