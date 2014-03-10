using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Response
{
    public class ApplicationRevision
    {
        [JsonProperty("revision")]
        public string Revision { get; set; }
    }
}
