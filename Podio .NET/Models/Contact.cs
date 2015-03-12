using System;
using System.Globalization;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Contact
    {
        [JsonProperty("profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ProfileId { get; set; }

        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserId { get; set; }

        [JsonProperty("external_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalId { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     The file id of the avatar
        /// </summary>
        [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
        public int Avatar { get; set; }

        public DateTime? BirthDate
        {
            get
            {
                if (!string.IsNullOrEmpty(BirthDateString))
                    return DateTime.ParseExact(BirthDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                return null;
            }
            set { BirthDateString = value.Value.ToString("yyyy-MM-dd"); }
        }

        [JsonProperty("birthdate", NullValueHandling = NullValueHandling.Ignore)]
        internal string BirthDateString { get; set; }

        [JsonProperty("department", NullValueHandling = NullValueHandling.Ignore)]
        public string Department { get; set; }

        [JsonProperty("vatin", NullValueHandling = NullValueHandling.Ignore)]
        public string Vatin { get; set; }

        [JsonProperty("skype", NullValueHandling = NullValueHandling.Ignore)]
        public string Skype { get; set; }

        [JsonProperty("about", NullValueHandling = NullValueHandling.Ignore)]
        public string About { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public String[] Address { get; set; }

        [JsonProperty("zip", NullValueHandling = NullValueHandling.Ignore)]
        public string Zip { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("im", NullValueHandling = NullValueHandling.Ignore)]
        public String[] IM { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public String[] Location { get; set; }

        [JsonProperty("mail", NullValueHandling = NullValueHandling.Ignore)]
        public String[] Mail { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public String[] Phone { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public String[] Title { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public String[] Url { get; set; }

        [JsonProperty("skill", NullValueHandling = NullValueHandling.Ignore)]
        public String[] Skill { get; set; }

        [JsonProperty("linkedin", NullValueHandling = NullValueHandling.Ignore)]
        public string LinkedIn { get; set; }

        [JsonProperty("twitter", NullValueHandling = NullValueHandling.Ignore)]
        public string Twitter { get; set; }

        [JsonProperty("organization", NullValueHandling = NullValueHandling.Ignore)]
        public string Organization { get; set; }

        [JsonProperty("space_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? SpaceId { get; set; }

        [JsonProperty("link", NullValueHandling = NullValueHandling.Ignore)]
        public string Link { get; set; }

        [JsonProperty("rights", NullValueHandling = NullValueHandling.Ignore)]
        public String[] Rights { get; set; }

        [JsonProperty("app_store_about", NullValueHandling = NullValueHandling.Ignore)]
        public string AboutAppStore { get; set; }

        [JsonProperty("app_store_organization", NullValueHandling = NullValueHandling.Ignore)]
        public string AppStoreOrganization { get; set; }

        [JsonProperty("app_store_location", NullValueHandling = NullValueHandling.Ignore)]
        public string AppStoreLocation { get; set; }

        [JsonProperty("app_store_title", NullValueHandling = NullValueHandling.Ignore)]
        public string AppStoreTitle { get; set; }

        [JsonProperty("app_store_url", NullValueHandling = NullValueHandling.Ignore)]
        public string AppStoreUrl { get; set; }

        [JsonProperty("last_seen_on", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastSeenOn { get; set; }

        [JsonProperty("is_employee", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEmployee { get; set; }

        // Only available for space contacts

        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public int? Role { get; set; }

        [JsonProperty("removable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Removable { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public FileAttachment Image { get; set; }
    }
}