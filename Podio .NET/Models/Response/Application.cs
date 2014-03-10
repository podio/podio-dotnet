using Newtonsoft.Json;
using System.Collections.Generic;

namespace PodioAPI.Models.Response
{
    public class Application
    {
        [JsonProperty("app_id")]
        public int AppId { get; set; }

        [JsonProperty("original")]
        public int? Original { get; set; }

        [JsonProperty("original_revision")]
        public int? OriginalRevision { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("icon_id")]
        public int? IconId { get; set; }

        [JsonProperty("space_id")]
        public int? SpaceId { get; set; }

        [JsonProperty("owner_id")]
        public int? OwnerId { get; set; }

        [JsonProperty("owner")]
        public Dictionary<string, object> Owner { get; set; }

        [JsonProperty(PropertyName = "config")]
        public Dictionary<string, object> Config { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("url_add")]
        public string UrlAdd { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("url_label")]
        public string UrlLabel { get; set; }

        [JsonProperty("mailbox")]
        public string Mailbox { get; set; }

        [JsonProperty("integration")]
        public Integration Integration { get; set; }

        [JsonProperty("fields")]
        public List<ApplicationField> Fields { get; set; }


        // When app is returned as part of large collection (e.g. for stream), some config properties is moved to the main object

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        public T Field<T>(string externalId)
            where T : ApplicationField, new()
        {
            var genericField = this.Fields.Find(field => field.ExternalId == externalId);
            return fieldInstance<T>(genericField);
        }

        public T Field<T>(int fieldId)
            where T : ApplicationField, new()
        {
            var genericField = this.Fields.Find(field => field.FieldId == fieldId);
            return fieldInstance<T>(genericField);
        }

        public T Field<T>()
           where T : ApplicationField, new()
        {
            T specificField = new T();
            this.Fields.Add(specificField);
            return specificField;
        }

        protected T fieldInstance<T>(ApplicationField genericField, string externalId = null, int? fieldId = null)
                    where T : ApplicationField, new()
        {
            T specificField = new T();
            if (genericField != null)
            {
                foreach (var property in genericField.GetType().GetProperties())
                {
                    specificField.GetType().GetProperty(property.Name).SetValue(specificField, property.GetValue(genericField, null), null);
                }
            }
            return specificField;
        }
    }
}
