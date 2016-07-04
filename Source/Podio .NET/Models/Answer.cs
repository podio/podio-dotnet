using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Answer
    {
        [JsonProperty("question_option_id")]
        public int questionOptionId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}