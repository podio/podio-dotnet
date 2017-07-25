using Newtonsoft.Json.Linq;
using PodioAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PodioAPI.Utils.ApplicationFields
{
    public class DurationApplicationField : ApplicationField
    {
        /// <summary>
        ///     True if Days option should be shown, False otherwise
        /// </summary>
        public bool Days
        {
            get { return FieldsContains("days"); }
            set
            {
                UpdateFields("days", value);
            }
        }

        /// <summary>
        ///     True if Hours option should be shown, False otherwise
        /// </summary>
        public bool Hours
        {
            get { return FieldsContains("hours"); }
            set
            {
                UpdateFields("hours", value);
            }
        }

        /// <summary>
        ///     True if Minutes option should be shown, False otherwise
        /// </summary>
        public bool Minutes
        {
            get { return FieldsContains("minutes"); }
            set
            {
                UpdateFields("minutes", value);
            }
        }

        /// <summary>
        ///     True if Seconds option should be shown, False otherwise
        /// </summary>
        public bool Seconds
        {
            get { return FieldsContains("seconds"); }
            set
            {
                UpdateFields("seconds", value);
            }
        }

        private bool FieldsContains(string fieldName) => GetFields().Contains(fieldName);

        private void UpdateFields(string fieldName, bool value)
        {
            var currentValues = new HashSet<string>(GetFields());
            if (value)
            {
                currentValues.Add(fieldName);
            }
            else
            {
                currentValues.Remove(fieldName);
            }
            this.InternalConfig.Settings["fields"] = JToken.FromObject(currentValues);
        }

        private IEnumerable<string> GetFields()
        {
            InitializeFieldSettings();
            if (this.GetSetting("fields") == null)
            {
                return Enumerable.Empty<string>();
            }

            return this.GetSettingsAs<string>("fields");
        }
    }
}