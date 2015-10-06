using Newtonsoft.Json;

namespace PodioAPI.Models
{
    public class Rating
    {
        [JsonProperty("approved")]
        public RatingType Approved { get; set; }

        [JsonProperty("rsvp")]
        public RatingType Rsvp { get; set; }

        [JsonProperty("fivestar")]
        public RatingType Fivestar { get; set; }

        [JsonProperty("yesno")]
        public RatingType YesNo { get; set; }

        [JsonProperty("thumbs")]
        public RatingType Thumbs { get; set; }

        [JsonProperty("like")]
        public RatingType Like { get; set; }
    }
}