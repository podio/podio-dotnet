using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PodioAPI.Models.Response
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ApplicationField
    {
        [JsonProperty("field_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? FieldId { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        public string Label {
            get { return this.InternalConfig.Label; }
            set 
            {
                InitializeFieldSettings();
                this.InternalConfig.Label = value;
            } 
        }

        [JsonProperty("external_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalId { get; internal set; }

        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public FieldConfig InternalConfig { get; internal set; }

        public FieldConfig Config {
            get 
            {
                return InitializeFieldSettings();
            }
        }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; internal set; }

        internal object GetSetting(string key)
        {
            if (this.InternalConfig.Settings != null)
            {
                if (InternalConfig.Settings.ContainsKey(key))
                    return InternalConfig.Settings[key];
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        internal IEnumerable<T> GetSettingsAs<T>(string key)
        {
            var rawOptions = (JArray)this.GetSetting(key);
            var options = new T[rawOptions.Count];
            for (int i = 0; i < rawOptions.Count; i++)
            {
                options[i] = rawOptions[i].ToObject<T>();
            }
            return options;
        }

        internal FieldConfig InitializeFieldSettings()
        {
            FieldConfig config = null;
            if (this.InternalConfig == null)
            {
                this.InternalConfig = new FieldConfig();
                config = this.InternalConfig;
            }
            else
            {
                config = this.InternalConfig;
            }

            if (this.InternalConfig.Settings == null)
                this.InternalConfig.Settings = new Dictionary<string, object>();

            return config;
        }
    }
}
