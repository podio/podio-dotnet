using System.Collections.Generic;
using Newtonsoft.Json;
using PodioAPI.Utils.ApplicationFields;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace PodioAPI.Models
{
    public class Application
    {
        public Application()
        {
            this.Fields = new List<ApplicationField>();
        }

        [JsonProperty("app_id")]
        public int AppId { get; set; }

        [JsonProperty("original")]
        public int? Original { get; private set; }

        [JsonProperty("original_revision")]
        public int? OriginalRevision { get; private set; }

        [JsonProperty("status")]
        public string Status { get; private set; }

        [JsonProperty("icon")]
        public string Icon { get; private set; }

        [JsonProperty("icon_id")]
        public int? IconId { get; private set; }

        [JsonProperty("space_id")]
        public int? SpaceId { get; set; }

        [JsonProperty("owner_id")]
        public int? OwnerId { get; private set; }

        [JsonProperty("owner")]
        public Profile Owner { get; private set; }

        [JsonProperty("config")]
        public ApplicationConfiguration Config { get; set; }

        [JsonProperty("layouts")]
        public JToken Layouts { get; set; }

        [JsonProperty("current_revision")]
        public int? CurrentRevision { get; private set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; private set; }

        [JsonProperty("rights")]
        public string[] Rights { get; private set; }

        [JsonProperty("link")]
        public string Link { get; private set; }

        [JsonProperty("link_add")]
        public string LinkAdd { get; set; }

        [JsonProperty("url_add")]
        public string UrlAdd { get; private set; }

        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("url_label")]
        public string UrlLabel { get; private set; }

        [JsonProperty("mailbox")]
        public string Mailbox { get; private set; }

        [JsonProperty("integration")]
        public Integration Integration { get; set; }

        [JsonProperty("fields")]
        public List<ApplicationField> Fields { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        // When app is returned as part of large collection (e.g. for stream), some config properties is moved to the main object

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        [JsonProperty("space")]
        public Space Space { get; set; }


        /// <summary>
        ///     Only for retrival
        ///     <para> for create or update use <see cref="PodioAPI.Models.Application.Field &lt;T&gt;()" /> overload</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="externalId"></param>
        /// <returns></returns>
        public T Field<T>(string externalId)
            where T : ApplicationField, new()
        {
            var genericField = this.Fields.Find(field => field.ExternalId == externalId);
            return fieldInstance<T>(genericField);
        }

        /// <summary>
        ///     Only for retrival
        ///     <para> for create or update use <see cref="PodioAPI.Models.Application.Field &lt;T&gt;()" /> overload</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public T Field<T>(int fieldId)
            where T : ApplicationField, new()
        {
            var genericField = this.Fields.Find(field => field.FieldId == fieldId);
            return fieldInstance<T>(genericField);
        }

        /// <summary>
        ///     For application create and update only
        ///     <para>Use the other overloads for retrival</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Field<T>()
            where T : ApplicationField, new()
        {
            T specificField = new T();
            SetFieldType(specificField);
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
                    var jsonAttribute =
                        ((JsonPropertyAttribute[])property.GetCustomAttributes(typeof(JsonPropertyAttribute), false));
                    if (jsonAttribute.Length > 0)
                        specificField.GetType()
                            .GetProperty(property.Name)
                            .SetValue(specificField, property.GetValue(genericField, null), null);
                }
            }
            return specificField;
        }

        private void SetFieldType(ApplicationField field)
        {
            if (field.GetType() == typeof(TextApplicationField))
            {
                field.Type = "text";
            }
            if (field.GetType() == typeof(NumericApplicationField))
            {
                field.Type = "number";
            }
            if (field.GetType() == typeof(StateApplicationField))
            {
                field.Type = "state";
            }
            if (field.GetType() == typeof(ImageApplicationField))
            {
                field.Type = "image";
            }
            if (field.GetType() == typeof(DateApplicationField))
            {
                field.Type = "date";
            }
            if (field.GetType() == typeof(AppReferenceApplicationField))
            {
                field.Type = "app";
            }
            if (field.GetType() == typeof(MoneyApplicationField))
            {
                field.Type = "money";
            }
            if (field.GetType() == typeof(ProgressApplicationField))
            {
                field.Type = "progress";
            }
            if (field.GetType() == typeof(LocationApplicationField))
            {
                field.Type = "location";
            }
            if (field.GetType() == typeof(DurationApplicationField))
            {
                field.Type = "duration";
            }
            if (field.GetType() == typeof(ContactApplicationField))
            {
                field.Type = "contact";
            }
            if (field.GetType() == typeof(CalculationApplicationField))
            {
                field.Type = "calculation";
            }
            if (field.GetType() == typeof(EmbedApplicationField))
            {
                field.Type = "embed";
            }
            if (field.GetType() == typeof(QuestionApplicationField))
            {
                field.Type = "question";
            }
            if (field.GetType() == typeof(CategoryApplicationField))
            {
                field.Type = "category";
            }
            if (field.GetType() == typeof(FileApplicationField))
            {
                field.Type = "file";
            }
            if (field.GetType() == typeof(EmailApplicationField))
            {
                field.Type = "email";
            }
            if (field.GetType() == typeof(PhoneApplicationField))
            {
                field.Type = "tel";
            }
        }
    }
}