using PodioAPI.Models;
using PodioAPI.Utils.ItemFields;
using System.Collections.Generic;

namespace PodioAPI.Utils.ApplicationFields
{
    public class CategoryApplicationField : ApplicationField
    {
        IEnumerable<CategoryItemField.Answer> _options;

        /// <summary>
        /// The list of options for the category
        /// </summary>
        public IEnumerable<CategoryItemField.Answer> Options
        {
            get
            {
                if (_options == null)
                {
                    _options = this.GetSettingsAs<CategoryItemField.Answer>("options");
                }
                return _options;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["options"] = value;
            }
        }

        /// <summary>
        /// True if multiple options should be allowed, False otherwise
        /// </summary>
        public bool Multiple
        {
            get
            {
                return (bool)this.GetSetting("multiple");
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["multiple"] = value;
            }
        }

        /// <summary>
        /// The way the options are displayed on the item, one of "inline", "list" or "dropdown"
        /// </summary>
        public string Display
        {
            get
            {
                return (string)this.GetSetting("display");
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["display"] = value;
            }
        }
    }
}
