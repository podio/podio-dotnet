using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class AppItemField : ItemField
    {
        public IEnumerable<Item> Items
        {
            get { return this.ValuesAs<Item>(); }
        }

        public IEnumerable<int> ItemIds
        {
            set
            {
                ensureValuesInitialized();
                foreach (var itemId in value)
                {
                    var jobject = new JObject();
                    jobject["value"] = itemId;
                    this.Values.Add(jobject);
                }
            }
        }

        public int ItemId
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