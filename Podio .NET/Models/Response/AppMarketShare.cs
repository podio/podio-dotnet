using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioAPI.Models.Response
{
    public class AppMarketShare
    {
        [JsonProperty("share_id")]
        public int? ShareId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("abstract")]
        public string Abstract { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("features")]
        public String[] Features { get; set; }

        [JsonProperty("integration")]
        public string Integration { get; set; }

        [JsonProperty("categories")]
        public Dictionary<string,object> Categories { get; set; }

        [JsonProperty("org")]
        public Dictionary<string,object> Org { get; set; }

        [JsonProperty("author_apps")]
        public int? AuthorApps { get; set; }

        [JsonProperty("author_packs")]
        public int? AuthorPacks { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("icon_id")]
        public int? IconId { get; set; }

        [JsonProperty("ratings")]
        public Dictionary<string,object> Ratings { get; set; }

        [JsonProperty("user_rating")]
        public String[] UserRating { get; set; }

        [JsonProperty("video")]
        public string Video { get; set; }

        [JsonProperty("rating")]
        public int? Rating { get; set; }

        [JsonProperty("author")]
        public ByLine Author { get; set; }

        [JsonProperty("app")]
        public Application Application { get; set; }

        [JsonProperty("space")]
        public Space space { get; set; }

        [JsonProperty("children")]
        public List<AppMarketShare> Children { get; set; }

        [JsonProperty("parents")]
        public List<AppMarketShare> Parents { get; set; }

        [JsonProperty("screenshots")]
        public List<FileAttachment> Screenshots { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

    }
}
