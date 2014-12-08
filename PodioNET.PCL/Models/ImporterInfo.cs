using Newtonsoft.Json;
using System.Collections.Generic;

namespace PodioAPI.Models
{
    public class ImporterInfo
    {
        [JsonProperty("row_count")]
        public int RowCount { get; set; }

        [JsonProperty("columns")]
        List<FileColumn> Columns { get; set; }
    }
    public class FileColumn
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
