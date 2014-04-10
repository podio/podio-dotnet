using PodioAPI.Models;
using System.Collections.Generic;

namespace PodioAPI.Services
{
    public class SpaceService
    {
        private Podio _podio;
        public SpaceService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Add a new space to an organization.
        /// <para>Podio API Reference: https://developers.podio.com/doc/spaces/create-space-22390 </para>
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="name">The name of the space</param>
        /// <param name="privacy">The privacy level of the space, either "open" or "closed", defaults to "closed"</param>
        /// <param name="autoJoin">True if new employees should be joined automatically, false otherwise, defaults to false</param>
        /// <param name="postOnNewApp">True if new apps should be announced with a status update, false otherwise</param>
        /// <param name="postOnNewMember">True if new members should be announced with a status update, false otherwise</param>
        /// <returns></returns>
        public int CreateSpace(int orgId, string name, string privacy = null, bool? autoJoin = null, bool? postOnNewApp = null, bool? postOnNewMember = null)
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
            dynamic respone = _podio.Post<dynamic>(url, requestData);
            return (int)respone["space_id"];
        }

        /// <summary>
        /// Updates the space with the given id.
        /// <para>Podio API Reference: https://developers.podio.com/doc/spaces/update-space-22391 </para>
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="name">The name of the space</param>
        /// <param name="urlLabel"> The new URL label, if any changes</param>
        /// <param name="privacy">The privacy level of the space, either "open" or "closed", defaults to "closed"</param>
        /// <param name="autoJoin">True if new employees should be joined automatically, false otherwise, defaults to false</param>
        /// <param name="postOnNewApp">True if new apps should be announced with a status update, false otherwise</param>
        /// <param name="postOnNewMember">True if new members should be announced with a status update, false otherwise</param>
        public void UpdateSpace(int spaceId, string name = "", string urlLabel = null, string privacy = null, bool? autoJoin = null, bool? postOnNewApp = null, bool? postOnNewMember = null)
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
            _podio.Put<dynamic>(url, requestData);
        }
    }
}
