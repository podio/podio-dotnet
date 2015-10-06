using System.Collections.Generic;
using PodioAPI.Models;
using System.Threading.Tasks;

namespace PodioAPI.Services
{
    public class SpaceService
    {
        private readonly Podio _podio;

        public SpaceService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        ///     Add a new space to an organization.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/create-space-22390 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="name">The name of the space</param>
        /// <param name="privacy">The privacy level of the space, either "open" or "closed", defaults to "closed"</param>
        /// <param name="autoJoin">True if new employees should be joined automatically, false otherwise, defaults to false</param>
        /// <param name="postOnNewApp">True if new apps should be announced with a status update, false otherwise</param>
        /// <param name="postOnNewMember">True if new members should be announced with a status update, false otherwise</param>
        /// <returns></returns>
        public async Task<int> CreateSpace(int orgId, string name, string privacy = null, bool? autoJoin = null,
            bool? postOnNewApp = null, bool? postOnNewMember = null)
        {
            string url = "/space/";
            dynamic requestData = new
            {
                org_id = orgId,
                name = name,
                privacy = privacy,
                auto_join = autoJoin,
                post_on_new_app = postOnNewApp,
                post_on_new_member = postOnNewMember
            };
            dynamic respone =  await _podio.Post<dynamic>(url, requestData);
            return (int) respone["space_id"];
        }

        /// <summary>
        ///     Updates the space with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/update-space-22391 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="name">The name of the space</param>
        /// <param name="urlLabel"> The new URL label, if any changes</param>
        /// <param name="privacy">The privacy level of the space, either "open" or "closed", defaults to "closed"</param>
        /// <param name="autoJoin">True if new employees should be joined automatically, false otherwise, defaults to false</param>
        /// <param name="postOnNewApp">True if new apps should be announced with a status update, false otherwise</param>
        /// <param name="postOnNewMember">True if new members should be announced with a status update, false otherwise</param>
        public async Task<dynamic> UpdateSpace(int spaceId, string name = null, string urlLabel = null, string privacy = null,
            bool? autoJoin = null, bool? postOnNewApp = null, bool? postOnNewMember = null)
        {
            string url = string.Format("/space/{0}", spaceId);
            dynamic requestData = new
            {
                name = name,
                url_label = urlLabel,
                privacy = privacy,
                auto_join = autoJoin,
                post_on_new_app = postOnNewApp,
                post_on_new_member = postOnNewMember
            };
             return await _podio.Put<dynamic>(url, requestData);
        }

        /// <summary>
        ///     Returns the workspaces in the organization that are accesible for the active user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/get-list-of-organization-workspaces-238875316 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SpaceMicro>> GetOrganizationSpaces(int orgId)
        {
            string url = string.Format("/space/org/{0}/", orgId);
            return  await _podio.Get<List<SpaceMicro>>(url);
        }

        /// <summary>
        ///     Returns the available spaces for the given organization. This is spaces that are open and available for the user to
        ///     join.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/get-available-spaces-1911961 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<SpaceMicro>> GetAvailableSpaces(int orgId)
        {
            string url = string.Format("/space/org/{0}/available/", orgId);
            return  await _podio.Get<List<SpaceMicro>>(url);
        }

        /// <summary>
        ///     Returns the available seats. A null value means there is an unlimited number of available seats.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/get-available-spaces-1911961 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public async Task<Seat> GetAvailableSeats(int spaceId)
        {
            string url = string.Format("/space/{0}/available", spaceId);
            return  await _podio.Get<Seat>(url);
        }

        /// <summary>
        ///     Get the space with the given id.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/get-space-22389 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public async Task<Space> GetSpace(int spaceId)
        {
            string url = string.Format("/space/{0}", spaceId);
            return  await _podio.Get<Space>(url);
        }

        /// <summary>
        ///     Returns the space in the given org with the given URL label
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/get-space-by-org-and-url-label-476929 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="urlLabel"></param>
        /// <returns></returns>
        public async Task<Space> GetSpaceByOrgAndUrlLabel(int orgId, string urlLabel)
        {
            string url = string.Format("/space/org/{0}/{1}", orgId, urlLabel);
            return  await _podio.Get<Space>(url);
        }

        /// <summary>
        ///     Returns the space and organization with the given full URL.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/get-space-by-url-22481 </para>
        /// </summary>
        /// <param name="orgSlug">The org of the slug to search for</param>
        /// <param name="spaceSlug">The slug of the url to search for</param>
        /// <param name="spaceUrl">
        ///     The full URL of the space. The URL does not have to be truncated to the space root, it can be
        ///     the full URL of the resource.
        /// </param>
        /// <returns></returns>
        public async Task<Space> GetSpaceByUrl(string orgSlug, string spaceSlug, string spaceUrl)
        {
            string url = "/space/url";
            var requestData = new Dictionary<string, string>()
            {
                {"org_slug", orgSlug},
                {"space_slug", spaceSlug},
                {"url", spaceUrl}
            };
            return  await _podio.Get<Space>(url, requestData);
        }

        /// <summary>
        ///     Returns the top spaces for the user.
        ///     <para>Podio API Reference: https://developers.podio.com/doc/spaces/get-top-spaces-22477 </para>
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<Space>> GetTopSpaces(int limit = 6)
        {
            string url = "/space/top/";
            var requestData = new Dictionary<string, string>()
            {
                {"limit", limit.ToString()}
            };
            return  await _podio.Get<List<Space>>(url, requestData);
        }
    }
}