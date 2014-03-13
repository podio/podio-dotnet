using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace PodioAPI.Utils
{
    public static class ObjectExtensions
    {
        public static T As<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            T someObject = new T();
            return (T)objectFromDict(someObject, source);
        }

        public static T ChangeType<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }

        private static object objectFromDict(object someObject, IDictionary<string, object> source)
        {
            var propertyMap = new Dictionary<string, PropertyInfo>();
            foreach (var property in someObject.GetType().GetProperties())
            {
                var name = ((JsonPropertyAttribute[])property.GetCustomAttributes(typeof(JsonPropertyAttribute), false)).First().PropertyName;
                propertyMap[name] = property;
            }
            //((DataMemberAttribute[])someObject.GetType().GetProperties().First().GetCustomAttributes(typeof(DataMemberAttribute), false)).First().Name

            foreach (KeyValuePair<string, object> item in source)
            {
                //someObject.GetType().GetProperty(item.Key).SetValue(someObject, item.Value, null);
                if (propertyMap.ContainsKey(item.Key))
                {
                    var value = item.Value;
                    if (value is Int64)
                    {
                        // Convert 64 bit ints to 32 bit if required
                        value = Convert.ToInt32(value);
                    }
                    else if (value is Dictionary<string, object> && propertyMap[item.Key].PropertyType != typeof(Dictionary<string, object>))
                    {
                        // Convert nested objects to appropriate type
                        var nestedObject = Activator.CreateInstance(propertyMap[item.Key].PropertyType);
                        value = objectFromDict(nestedObject, (Dictionary<string, object>)value);
                    }
                    else if (propertyMap[item.Key].PropertyType == typeof(DateTime?) && value != null)
                    {
                        // Convert date strings to date times
                        value = DateTime.Parse((string)value);
                    }
                    else if (value is Newtonsoft.Json.Linq.JArray)
                    {
                        var castedValue = (Newtonsoft.Json.Linq.JArray)value;
                        switch (propertyMap[item.Key].PropertyType.Name)
                        {
                            case "String[]":
                                value = castedValue.Select(s => s.ToString()).ToArray();
                                break;
                        }
                    }
                    propertyMap[item.Key].SetValue(someObject, value, null);
                }
            }
            return someObject;
        }
    }
}
