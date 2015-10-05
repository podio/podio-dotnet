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
    }
}