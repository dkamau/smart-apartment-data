using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartApartmentData.Infrastructure.Models
{
    public class MultiMatch
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("fields")]
        public List<string> Fields { get; set; }
    }

    public class Query
    {
        [JsonProperty("multi_match")]
        public MultiMatch MultiMatch { get; set; }
    }

    public class Search
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }
    }
}
