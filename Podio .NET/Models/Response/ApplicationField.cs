using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PodioAPI.Models.Response
{
    public class ApplicationField
    {
        [JsonProperty("field_id")]
        public int? FieldId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("config")]
        public FieldConfig Config { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        protected object GetSetting(string key)
        {
            if (this.Config.Settings != null)
            {
                if (Config.Settings.ContainsKey(key))
                    return Config.Settings[key];
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        protected IEnumerable<T> GetSettingsAs<T>(string key)
        {
            var rawOptions = (JArray)this.GetSetting(key);
            var options = new T[rawOptions.Count];
            for (int i = 0; i < rawOptions.Count; i++)
            {
                options[i] = rawOptions[i].ToObject<T>();
            }
            return options;
        }

        protected void InitializeFieldSettings()
        {
            if(this.Config == null)
                this.Config = new FieldConfig();

            if (this.Config.Settings == null)
                this.Config.Settings = new Dictionary<string, object>();
        }
    }
}
