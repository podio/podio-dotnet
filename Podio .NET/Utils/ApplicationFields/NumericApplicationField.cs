using PodioAPI.Models;

namespace PodioAPI.Utils.ApplicationFields
{
    public class NumericApplicationField : ApplicationField
    {
        /// <summary>
        ///     The number of decimals displayed
        /// </summary>
        public int? Decimals
        {
            get { return (int?) this.GetSetting("decimals"); }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["decimals"] = value;
            }
        }
    }
}