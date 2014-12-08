﻿using Newtonsoft.Json;
using System;

namespace PodioAPI.Models
{
    public class QuestionOption
    {
        [JsonProperty("question_option_id")]
        public int QuestionOptionId { get; set; }

        [JsonProperty("text")]
        public String Text { get; set; }
    }
}
