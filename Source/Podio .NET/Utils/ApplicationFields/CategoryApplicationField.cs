using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;
using PodioAPI.Utils.ItemFields;

namespace PodioAPI.Utils.ApplicationFields
{
    public class CategoryApplicationField : ApplicationField
    {
        private IEnumerable<CategoryItemField.Option> _options;

        /// <summary>
        ///     The list of options for the category
        /// </summary>
        public IEnumerable<CategoryItemField.Option> Options
        {
            get
            {
                if (_options == null)
                {
                    _options = this.GetSettingsAs<CategoryItemField.Option>("options");
                }
                return _options;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["options"] = value != null ? JToken.FromObject(value) : null;
            }
        }

        /// <summary>
        ///     True if multiple options should be allowed, False otherwise
        /// </summary>
        public bool Multiple
        {
            get { return (bool) this.GetSetting("multiple"); }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["multiple"] = value;
            }
        }

        /// <summary>
        ///     The way the options are displayed on the item, one of "inline", "list" or "dropdown"
        /// </summary>
        public string Display
        {
            get { return (string) this.GetSetting("display"); }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["display"] = value;
            }
        }
    }
}