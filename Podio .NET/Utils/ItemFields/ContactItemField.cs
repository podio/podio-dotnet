using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

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
}