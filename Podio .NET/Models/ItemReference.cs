using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    /// <summary>
    ///     Used to returns the items that have a reference to the given item.
    /// </summary>
    public class ItemReference
    {
        [JsonProperty("field")]
        public ItemFieldMicro Field { get; set; }

        [JsonProperty("app")]
        public Application App { get; set; }

        [JsonProperty("items")]
        public List<ItemMicro> Items { get; set; }
    }
}