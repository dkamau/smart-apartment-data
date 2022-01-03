using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartApartmentData.App.Data
{
    public class Shards
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("successful")]
        public int Successful { get; set; }

        [JsonProperty("skipped")]
        public int Skipped { get; set; }

        [JsonProperty("failed")]
        public int Failed { get; set; }
    }

    public class Total
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("relation")]
        public string Relation { get; set; }
    }

    public class Property
    {
        [JsonProperty("propertyID")]
        public int PropertyID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("formerName")]
        public string FormerName { get; set; }

        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public class Mgmt
    {
        [JsonProperty("mgmtID")]
        public int MgmtID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }

    public class Source
    {
        [JsonProperty("property")]
        public Property Property { get; set; }

        [JsonProperty("mgmt")]
        public Mgmt Mgmt { get; set; }
    }

    public class Hit
    {
        [JsonProperty("_index")]
        public string Index { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_score")]
        public double Score { get; set; }

        [JsonProperty("_source")]
        public Source Source { get; set; }

        [JsonProperty("total")]
        public Total Total { get; set; }

        [JsonProperty("max_score")]
        public double MaxScore { get; set; }

        [JsonProperty("hits")]
        public List<Hit> Hits { get; set; }
    }

    public class SearchResult
    {
        [JsonProperty("took")]
        public int Took { get; set; }

        [JsonProperty("timed_out")]
        public bool TimedOut { get; set; }

        [JsonProperty("_shards")]
        public Shards Shards { get; set; }

        [JsonProperty("hits")]
        public Hit Hits { get; set; }
    }

}
