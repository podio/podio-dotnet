using System.Linq;
using PodioAPI.Models.Response;

namespace PodioAPI.Utils.ItemFields
{
    public class CalculationItemField : ItemField
    {
        public float? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return float.Parse((string)this.Values.First()["value"]);
                }
                else
                {
                    return null;
                }
            }
        }

    }

}
