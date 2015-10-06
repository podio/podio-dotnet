using System;
using System.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class MoneyItemField : ItemField
    {
        public string Currency
        {
            get
            {
                if (this.HasValue("currency"))
                {
                    return (string) this.Values.First()["currency"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                EnsureValuesInitialized(true);
                this.Values.First()["currency"] = value;
            }
        }

        public decimal? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return (Decimal) this.Values.First()["value"];
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