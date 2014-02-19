using PodioAPI.Models.Response;
using PodioAPI.Models.Request;
using PodioAPI.Utils;
using System.Collections.Generic;

namespace PodioAPI.Services
{
    public class ItemService
    {
        private Podio _podioInstance;
        public ItemService(Podio currentInstance)
        {
            _podioInstance = currentInstance;
        }

        /// <summary> Returns the item with the specified id. 
        /// <para>Podio API Reference: https://developers.podio.com/doc/items/get-item-22360 </para>
        /// </summary> 
        /// <param name="itemId"></param>
        /// <param name="markedAsViewed">If true marks any new notifications on the given item as viewed, otherwise leaves any notifications untouched, Default value true</param>       
        public Item GetItem(int itemId, bool markedAsViewed = true)
        {
            var markAsViewdValue = markedAsViewed == true ? "1" : "0";
            var attributes = new Dictionary<string, string>()
            {
                {"mark_as_viewed", markAsViewdValue}
            };
            var url = string.Format("/item/{0}", itemId);
            return _podioInstance.Get<Item>(url, attributes);
        }
    }
}
