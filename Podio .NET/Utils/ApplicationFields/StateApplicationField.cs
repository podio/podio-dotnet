using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ApplicationFields
{
    public class StateApplicationField : ApplicationField
    {
        private IEnumerable<string> _allowedValues;

        public IEnumerable<string> AllowedValues
        {
            get
            {
                if (_allowedValues == null)
                {
                    _allowedValues = this.GetSettingsAs<string>("allowed_values");
                }
                return _allowedValues;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["allowed_values"] = value != null ? JToken.FromObject(value) : null;
            }
        }
    }
}