using PodioAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Utils.ApplicationFields
{
    public class NumericApplicationField : ApplicationField
    {
        /// <summary>
        /// The number of decimals displayed
        /// </summary>
        public int? Decimals
        {
            get
            {
                return (int?)this.GetSetting("decimals");
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["decimals"] = value;
            }
        }
    }
}
