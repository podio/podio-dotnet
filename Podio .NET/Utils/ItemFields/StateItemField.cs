using PodioAPI.Models;
using System.Linq;

namespace PodioAPI.Utils.ItemFields
{
    public class StateItemField : ItemField
    {
        public string Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return (string)this.Values.First()["value"];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                ensureValuesInitialized(true);
                this.Values.First()["value"] = value;
            }
        }
    }
}
