using System.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class NumericItemField : ItemField
    {
        public double? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return (double) this.Values.First()["value"];
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