using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public class Reminder
    {
        [JsonProperty("reminder_id")]
        public int Ref { get; set; }

        [JsonProperty("reminder_delta")]
        public int IsLiked { get; set; }
    }
}
