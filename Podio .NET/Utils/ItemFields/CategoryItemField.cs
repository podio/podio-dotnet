using Newtonsoft.Json;
using PodioAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PodioAPI.Utils.ItemFields
{
    public class CategoryItemField : ItemField
    {
        private List<Answer> _options;

        public IEnumerable<Answer> Options
        {
            get
            {
                return this.valuesAs<Answer>(_options);
            }
        }

        public int OptionId {
            set
            {
                ensureValuesInitialized(true);
                this.Values.First()["value"] = value;
            }
        }

        public IEnumerable<int> OptionIds
        {
            set
            {
                ensureValuesInitialized();
                foreach (var optionId in value)
                {
                    var dict = new Dictionary<string, object>();
                    dict["value"] = optionId;
                    this.Values.Add(dict);
                }
            }
        }

        public class Answer
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
