using System;
using System.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class DurationItemField : ItemField
    {
        public TimeSpan? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return TimeSpan.FromSeconds(Convert.ToDouble((Int64) this.Values.First()["value"]));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                EnsureValuesInitialized(true);
                if (value != null)
                {
                    this.Values.First()["value"] = (Int64) value.Value.TotalSeconds;
                }
                else
                {
                    this.Values.First()["value"] = null;
                }
            }
        }
    }
}