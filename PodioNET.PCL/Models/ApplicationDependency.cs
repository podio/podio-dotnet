using Newtonsoft.Json;
using System.Collections.Generic;
using PodioAPI.Utils;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace PodioAPI.Models
{
	public class ApplicationDependency
	{

		[JsonProperty("apps")]
		public List<Application> Apps { get; set; }

		[JsonProperty("dependencies")]
		private object DependencyObject { get; set; }

		public Dictionary<int, List<int>> Dependencies { get { return LoadDependencies(); } }

		private Dictionary<int, List<int>> LoadDependencies()
		{
			var dictionaryToLoad = new Dictionary<int, List<int>>();
			var reflectedValuesDictionay = (Dictionary<string, object>)typeof(ApplicationDependency).GetProperty("DependencyObject").GetValue(this, null);
			if (reflectedValuesDictionay.Count > 0)
			{
				foreach (var item in reflectedValuesDictionay)
				{
					var dependencyValueJArray = (JArray)item.Value;
					dictionaryToLoad.Add(int.Parse(item.Key), dependencyValueJArray.ToObject<List<int>>());
				}
			}
			return dictionaryToLoad;
		}
	}
}
