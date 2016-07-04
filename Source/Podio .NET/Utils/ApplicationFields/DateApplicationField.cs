using PodioAPI.Models;

namespace PodioAPI.Utils.ApplicationFields
{
    public class DateApplicationField : ApplicationField
    {
        /// <summary>
        ///     True if the items should show up in the calendar, false otherwise
        /// </summary>
        public bool Calendar
        {
            get { return (bool) this.GetSetting("calendar"); }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["calendar"] = value;
            }
        }

        /// <summary>
        ///     End date:  is either "disabled", "enabled" or "required"
        /// </summary>
        public string End
        {
            get { return (string) this.GetSetting("end"); }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["end"] = value;
            }
        }

        /// <summary>
        ///     Time component: is either "disabled", "enabled" or "required"
        /// </summary>
        public string Time
        {
            get { return (string) this.GetSetting("time"); }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["time"] = value;
            }
        }
    }
}