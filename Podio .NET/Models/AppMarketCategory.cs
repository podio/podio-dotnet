
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
  public class AppMarketCategory
    {
      [JsonProperty("functional")]
      public JArray Functional { get; set; }

      [JsonProperty("vertical")]
      public JArray Vertical { get; set; }
    }
}
