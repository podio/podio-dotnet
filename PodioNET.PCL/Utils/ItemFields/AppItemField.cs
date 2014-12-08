using Newtonsoft.Json.Linq;
using PodioAPI.Models;
using System.Collections.Generic;

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
                    var jobject = new JObject();
                    jobject["value"] = itemId;
                    this.Values.Add(jobject);
	            }
            }
        }
        public int  ItemId
        {
            set
            {
                ensureValuesInitialized();
               
                    var jobject = new JObject();
                    jobject["value"] = value;
                    this.Values.Add(jobject);
                
            }
        }
    }
}
