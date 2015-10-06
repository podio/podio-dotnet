using System;
using System.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class ProgressItemField : ItemField
    {
        public int? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return Convert.ToInt32((Int64) this.Values.First()["value"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                EnsureValuesInitialized(true);
                this.Values.First()["value"] = value;
            }
        }
    }
}