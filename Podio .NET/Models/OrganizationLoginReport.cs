using System;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class OrganizationLoginReport
    {
        /// <summary>
        ///     The start date of the week.
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        ///     The total number of users in the organization.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        ///     The total number of users that logged in during the week.
        /// </summary>
        [JsonProperty("active")]
        public int ActiveUser { get; set; }
    }
}