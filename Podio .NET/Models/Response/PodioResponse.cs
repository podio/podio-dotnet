using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodioAPI.Models.Response
{
    public class PodioResponse
    {
        public dynamic Body { get; set; }
        public int Status { get; set; }
        public Dictionary<string,string> Headers { get; set; }
        public string RequestUri { get; set; }
    }
}
