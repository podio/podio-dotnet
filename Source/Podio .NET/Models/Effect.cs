using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PodioAPI.Models;

namespace PodioAPI
{
    public class Effect
    {
        [JsonProperty("type")]
        public String Type { get; set; }

        [JsonProperty("effect_id")]
        public int EffectId { get; set; }

        [JsonProperty("attributes")]
        public List<FlowAttribute> Attributes { get; set; }
    }
}