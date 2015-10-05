using Newtonsoft.Json;
using PodioAPI.Models;
using System.Collections.Generic;

namespace PodioAPI.Utils.ApplicationFields
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EmailApplicationField : ApplicationField
    {
        private string _size;

        /// <summary>
        ///     Size of the input field, either "small" or "large"
        /// </summary>
        public string Size
        {
            get
            {
                if (_size == null)
                {
                    _size = (string) this.GetSetting("size");
                }
                return _size;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["size"] = value;
            }
        }

        public bool IncludeInCC
        {
            get
            {
                return (bool)this.GetSetting("include_in_cc");
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["include_in_cc"] = value;
            }
        }

        public bool IncludeInBCC
        {
            get
            {
                return (bool)this.GetSetting("include_in_bcc");
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["include_in_bcc"] = value;
            }
        }


    }
}