using PodioAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Utils.ItemFields
{
    public class LocationItemField : ItemField
    {
        public IEnumerable<string> Locations
        {
            get
            {
                return new List<string>(this.Values.Select(s => (string)s["value"]));
            }

            set
            {
                ensureValuesInitialized();
                foreach (var location in value)
                {
                    var dict = new Dictionary<string, object>();
                    dict["value"] = location;
                    this.Values.Add(dict);
                }
            }
        }
    }
}
