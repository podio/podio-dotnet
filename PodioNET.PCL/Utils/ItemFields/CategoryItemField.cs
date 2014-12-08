﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PodioAPI.Utils.ItemFields
{
    public class CategoryItemField : ItemField
    {
        private List<Option> _options;

        public IEnumerable<Option> Options
        {
            get
            {
                return this.valuesAs<Option>(_options);
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
