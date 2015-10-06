using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using PodioAPI.Models;

namespace PodioAPI.Utils
{
    public static class ObjectExtensions
    {
       
        public static string ToStringOrNull(this object obj)
        {
            return obj == null ? null : obj.ToString();
        }

        public static T ChangeType<T>(this object obj)
        {
            return (T) Convert.ChangeType(obj, typeof (T));
        }


        private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propInfo = null;
            do
            {
                propInfo = type.GetProperty(propertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                type = type.BaseType;
            } while (propInfo == null && type != null);
            return propInfo;
        }

        internal static object GetPropertyValue(this object obj, string propertyName)
        {
            Type objType = obj.GetType();
            PropertyInfo propInfo = GetPropertyInfo(objType, propertyName);
            return propInfo.GetValue(obj, null);
        }
    }
}