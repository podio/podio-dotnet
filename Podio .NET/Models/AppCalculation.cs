using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class AppCalculation
    {
        [JsonProperty("app")]
        public Application App { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("reference_field")]
        public ReferenceField ReferenceField { get; set; }

        [JsonProperty("value_fields")]
        public List<ValueFields> ValueFields { get; set; }
    }

    public class ReferenceField : CommenFieldsValues
    {
        [JsonProperty("config")]
        public ReferenceFieldConfig Config { get; set; }
    }

    public class ReferenceFieldConfig
    {
        [JsonProperty("settings")]
        public ReferenceFieldSetting Setting { get; set; }

        [JsonProperty("mapping")]
        public string Mapping { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class ReferenceFieldSetting
    {
        [JsonProperty("referenceable_types")]
        public int[] ReferenceableTypes { get; set; }
    }

    public class ValueFields : CommenFieldsValues
    {
        [JsonProperty("config")]
        public ValueFieldConfig Config { get; set; }
    }

    public class ValueFieldConfig
    {
        [JsonProperty("mapping")]
        public string Mapping { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("settings")]
        public object Setting { get; set; }
    }

    public class CommenFieldsValues
    {
        [JsonProperty("external_id")]
        private string ExternalId { get; set; }

        [JsonProperty("type")]
        private string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("field_id")]
        public int FieldId { get; set; }
    }
}