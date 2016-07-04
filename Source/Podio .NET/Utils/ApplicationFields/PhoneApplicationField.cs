using Newtonsoft.Json;
using PodioAPI.Models;
using System.Collections.Generic;

namespace PodioAPI.Utils.ApplicationFields
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PhoneApplicationField : ApplicationField
    {
        private string _callLinkScheme;

        public string CallLinkScheme
        {
            get
            {
                if (_callLinkScheme == null)
                {
                    _callLinkScheme = (string) this.GetSetting("call_link_scheme");
                }
                return _callLinkScheme;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["call_link_scheme"] = value;
            }
        }

        public IEnumerable<string> PossibleTypes
        {
            get
            {
                return this.GetSettingsAs<string>("possible_types");
            } 
        }
    }
}