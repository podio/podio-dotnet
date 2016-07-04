using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class ItemField
    {
        [JsonProperty("field_id")]
        public int? FieldId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("values")]
        public JArray Values { get; set; }

        [JsonProperty("config")]
        public FieldConfig Config { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }


        public bool HasValue(string key = null)
        {
            return this.Values != null
                   && this.Values.Any()
                   && (key == null ||
                       (this.Values.FirstOrDefault() != null &&
                        this.Values.First()[key] != null));
        }

        protected T ValueAs<T>(JToken value, string key)
            where T : class, new()
        {
            if (value != null && value[key] != null)
                return value[key].ToObject<T>();

            return null;
        }

        protected List<T> ValuesAs<T>()
            where T : class, new()
        {
            var list = new List<T>();
            if (this.Values != null)
            {
                foreach (var itemAttributes in this.Values)
                {
                    var obj = this.ValueAs<T>(itemAttributes, "value");
                    list.Add(obj);
                }
            }

            return list;
        }

        protected void EnsureValuesInitialized(bool includeFirstChildDict = false)
        {
            if (this.Values == null)
            {
                this.Values = new JArray();
            }
            if (includeFirstChildDict && this.Values.Count == 0)
            {
                this.Values.Add(new JObject());
            }
        }
    }
}