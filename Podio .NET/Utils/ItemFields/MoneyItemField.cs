using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PodioAPI.Models.Response;

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
                    return (string)this.Values.First()["currency"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ensureValuesInitialized(true);
                this.Values.First()["currency"] = value;
            }
        }

        public decimal? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return Decimal.Parse((string)this.Values.First()["value"]);
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
