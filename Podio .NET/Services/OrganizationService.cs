using System.Collections.Generic;
using PodioAPI.Models;
using PodioAPI.Utils;


namespace PodioAPI.Services
{
    public class OrganizationService
    {
        private Podio _podio;
        public OrganizationService(Podio currentInstance)
        {
            _podio = currentInstance;
        }

        /// <summary>
        /// Creates a new organization.
        /// <para>Podio API Reference: https://developers.podio.com/doc/organizations/add-new-organization-22385 </para>
        /// </summary>
        /// <param name="name">The name of the new organization</param>
        /// <param name="logo">The file id of the logo of the organization</param>
        /// <returns></returns>
        public Organization AddNewOrganization(string name, int logo)
        {
            string url = "/org/";
            dynamic requestData = new
            {
                name = name,
                logo = logo
            };
            return _podio.Post<Organization>(url, requestData);
        }
    }
}
