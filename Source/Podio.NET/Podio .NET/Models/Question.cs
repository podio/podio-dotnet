using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Question
    {
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ref")]
        public Reference Ref { get; set; }

        [JsonProperty("answers")]
        public List<QuestionAnswer> Answers { get; set; }

        [JsonProperty("options")]
        public List<QuestionOption> Options { get; set; }
    }
}