using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodioAPI.Utils
{
    public interface IJsonSerializer
    {
        string Serilaize(object entity);
        object Deserilaize<T>(string json);
    }
}
