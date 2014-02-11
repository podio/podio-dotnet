using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodioAPI.Utils
{
    class JSONSerializer
    {
        internal static string Serilaize(object entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        internal static object Deserilaize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
