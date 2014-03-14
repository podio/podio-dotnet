using PodioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PodioAPI.Utils.ItemFields
{
    public class LocationItemField : ItemField
    {
        public IEnumerable<string> Locations
        {
            get
            {
                if (this.Values != null && this.Values.Any())
                    return new List<string>(this.Values.Select(s => (string)s["value"]));
                else
                    return new List<String>();

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
