using PodioAPI.Models.Response;
using System.Collections.Generic;

namespace PodioAPI.Utils.ItemFields
{
    public class ImageItemField : ItemField
    {
        private List<FileAttachment> _images;

        public IEnumerable<FileAttachment> Images
        {
            get
            {
                return this.valuesAs<FileAttachment>(_images);
            }
        }

        public IEnumerable<int> FileIds
        {
            set
            {
                ensureValuesInitialized();
                foreach (var fileId in value)
                {
                    var dict = new Dictionary<string, object>();
                    dict["value"] = fileId;
                    this.Values.Add(dict);
                }
            }
        }
    }
}
