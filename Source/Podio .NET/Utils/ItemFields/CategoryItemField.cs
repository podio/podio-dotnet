using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class CategoryItemField : ItemField
    {

        public IEnumerable<Option> Options
        {
            get { return this.ValuesAs<Option>(); }
        }

        public int OptionId
        {
            set
            {
                EnsureValuesInitialized(true);
                this.Values.First()["value"] = value;
            }
        }

        public IEnumerable<int> OptionIds
        {
            set
            {
                EnsureValuesInitialized();
                foreach (var optionId in value)
                {
                    var jobject = new JObject();
                    jobject["value"] = optionId;
                    this.Values.Add(jobject);
                }
            }
        }

        public string OptionText
        {
            set
            {
                EnsureValuesInitialized(true);
                this.Values.First()["value"] = value;
            }
        }

        public IEnumerable<string> OptionTexts
        {
            set
            {
                EnsureValuesInitialized();
                foreach (var optionId in value)
                {
                    var jobject = new JObject();
                    jobject["value"] = optionId;
                    this.Values.Add(jobject);
                }
            }
        }

        public class Option
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("id")]
            public int? Id { get; set; }

            [JsonProperty("color")]
            public string Color { get; set; }
        }
    }
}