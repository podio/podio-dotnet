using PodioAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Utils.ItemFields
{
    public class AppItemField : ItemField
    {
        private List<Item> _items;

        public IEnumerable<Item> Items
        {
            get
            {
                return this.valuesAs<Item>(_items);
            }
        }

        public IEnumerable<int> ItemIds {
            set {
                ensureValuesInitialized();
                foreach (var itemId in value)
	            {
                    var dict = new Dictionary<string, object>();
                    dict["value"] = itemId;
		            this.Values.Add(dict);
	            }
            }
        }
    }
}
