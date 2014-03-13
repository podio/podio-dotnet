using PodioAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Utils.ApplicationFields
{
    public class ContactApplicationField : ApplicationField
    {
        private string type;

        /// <summary>
        /// "type": The type of contacts this field allows. 
        /// <para>One of "space_users" (only members of the workspace), "all_users", "space_contacts" or "space_users_and_contacts" (deprecated)</para>
        /// </summary>
        public string Type
        {
            get
            {
                if (type == null)
                {
                    type = (string)this.GetSetting("type");
                }
                return type;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["type"] = value;
            }
        }
    }
}
