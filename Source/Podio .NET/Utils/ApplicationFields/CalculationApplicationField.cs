using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ApplicationFields
{
    public class CalculationApplicationField : ApplicationField
    {
        private CalculationExpression _calculationExpression;

        public CalculationExpression Expression
        {
            get
            {
                if (_calculationExpression == null)
                {
                    var expressions = this.GetSettingsAs<CalculationExpression>("expression");
                    if (expressions != null && expressions.Any())
                        _calculationExpression = expressions.First();
                }
                return _calculationExpression;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["expression"] = value != null ? JToken.FromObject(value) : null;
            }
        }

        private string _script;

        /// <summary>
        /// Script as string
        /// </summary>
        public string ScriptAsString
        {
            get
            {
                if (_script == null)
                {
                    _script = (string)this.GetSetting("script");
                }
                return _script;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["script"] = value;
            }
        }

        private string _unit;

        /// <summary>
        ///     The unit of the result, if any
        /// </summary>
        public string Unit
        {
            get
            {
                if (_unit == null)
                {
                    _unit = (string) this.GetSetting("unit");
                }
                return _unit;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["unit"] = value;
            }
        }

        private string _returnType;

        /// <summary>
        ///     The type of the script result, can be text, number or date
        /// </summary>
        public string ReturnType
        {
            get
            {
                if (_returnType == null)
                {
                    _returnType = (string)this.GetSetting("return_type");
                }
                return _returnType;
            }
        }

        private int? _decimals;

        /// <summary>
        ///     The number of decimals displayed
        /// </summary>
        public int? Decimals
        {
            get
            {
                if (_decimals == null)
                {
                    _decimals = (int?) this.GetSetting("decimals");
                }
                return _decimals;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["decimals"] = value;
            }
        }
    }

    public class CalculationExpression
    {
        /// <summary>
        ///     The type of expression, can be "field", "number", "reference", "outgoing_reference", "multiply", "divide", "plus"
        ///     and "minus"
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        #region The additional properties are depending on the type

        /// <summary>
        ///     The id of the field to receive the value from. only when type is "field"
        /// </summary>
        [JsonProperty("field_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? FieldId { get; set; }

        /// <summary>
        ///     The id of the field to receive the value from, only when type is "number"
        /// </summary>
        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public double? Number { get; set; }

        /// <summary>
        ///     The app reference field, only when type is "reference" , "outgoing_reference"
        /// </summary>
        [JsonProperty("reference_field_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReferenceField_id { get; set; }

        /// <summary>
        ///     The id of the value field on the related app
        /// </summary>
        [JsonProperty("value_field_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ValueFieldId { get; set; }

        /// <summary>
        ///     The aggregation of the related item, either "sum", "average", "min", "max" and "count"
        /// </summary>
        [JsonProperty("aggregation", NullValueHandling = NullValueHandling.Ignore)]
        public string Aggregation { get; set; }

        /// <summary>
        ///     The left part of the operation, Only when type is multiply, divide, plus, minus
        /// </summary>
        [JsonProperty("left", NullValueHandling = NullValueHandling.Ignore)]
        public CalculationExpression Left { get; set; }

        /// <summary>
        ///     The right part of the operation , Only when type is multiply, divide, plus, minus
        /// </summary>
        [JsonProperty("right", NullValueHandling = NullValueHandling.Ignore)]
        public CalculationExpression Right { get; set; }

        #endregion
    }
}