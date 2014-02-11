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
        /// 
        /* public Item Get(int itemId)
        {
            //TODO   
        }
        */
    }


}
