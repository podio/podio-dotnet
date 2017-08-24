using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;
using Newtonsoft.Json;

namespace PodioAPI.Utils.ItemFields
{
    public class ContactItemField : ItemField
    {

        public IEnumerable<Contact> Contacts
        {
            get { return this.ValuesAs<Contact>(); }
        }

        /// <summary>
        ///     The profile_id's of the contacts
        /// </summary>
        public IEnumerable<int> ContactIds
        {
            set
            {
                EnsureValuesInitialized();
                foreach (var contactId in value)
                {
                    var jobject = new JObject();
                    jobject["value"] = contactId;
                    this.Values.Add(jobject);
                }
            }
        }

        /// <summary>
        ///     The contact members
        /// </summary>
        public IEnumerable<Member> Members
        {
            set
            {
                EnsureValuesInitialized();
                foreach (var member in value)
                {
                    var jobject = new JObject();
                    jobject["value"] = JObject.FromObject(member);
                    this.Values.Add(jobject);
                }
            }
        }

        public int ContactId
        {
            set
            {
                EnsureValuesInitialized();

                var jobject = new JObject();
                jobject["value"] = value;
                this.Values.Add(jobject);
            }
        }
    }

    public abstract class Member
    {
        [JsonProperty("type")]
        public abstract string Type { get; }
    }

    public class MailMember : Member
    {
        [JsonProperty("type")]
        public override string Type { get; } = "mail";

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class UserMember : Member
    {
        [JsonProperty("type")]
        public override string Type { get; } = "user";

        /// <summary>
        /// The profile_id of the contact
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}