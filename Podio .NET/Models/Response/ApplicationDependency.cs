using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioAPI.Models.Response
{
    public class ApplicationDependency
    {

        [JsonProperty("apps")]
        public List<Application> Apps { get; set; }

        [JsonProperty("dependencies")]
        private object DependencyObject { get; set; }

        public Dictionary<int, int[]> Dependencies { get {return LoadDependencies();}}

        private Dictionary<int, int[]> LoadDependencies()
        {
            Dictionary<int, int[]> dependencies = new Dictionary<int, int[]>();
            if(DependencyObject != null)
            {
                foreach(var property in DependencyObject.GetType().GetProperties())
                {
                    var test = DependencyObject.GetType().GetProperty(property.Name).GetValue(DependencyObject, null);
                }
            }
            return dependencies;
        }
    }
}
