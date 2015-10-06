using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class ImageItemField : ItemField
    {
        public IEnumerable<FileAttachment> Images
        {
            get { return this.ValuesAs<FileAttachment>(); }
        }

        public IEnumerable<int> FileIds
        {
            set
            {
                EnsureValuesInitialized();
                foreach (var fileId in value)
                {
                    var jobject = new JObject();
                    jobject["value"] = fileId;
                    this.Values.Add(jobject);
                }
            }
        }

        public int FileId
        {
            set
            {
                EnsureValuesInitialized();

                var jobject = new JObject();
                jobject["value"] = value;
                this.Values.Add(jobject);
            }
        }
    }
}