using System;
using System.Linq;
using PodioAPI.Models;

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
                    return float.Parse((string) this.Values.First()["value"]);
                }
                else
                {
                    return null;
                }
            }
        }

        public string ValueAsString
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return (string) this.Values.First()["value"];
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? Start
        {
            get
            {
                if (this.HasValue("start"))
                {
                    return (DateTime) this.Values.First()["start"];
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? StartUTC
        {
            get
            {
                if (this.HasValue("start_utc"))
                {
                    return (DateTime) this.Values.First()["start_utc"];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}