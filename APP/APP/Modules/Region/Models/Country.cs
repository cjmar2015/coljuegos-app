using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Country
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "iso")]
        public string iso { get; set; }

        [JsonProperty(PropertyName = "iso2")]
        public string iso2 { get; set; }

        [JsonProperty(PropertyName = "iso3")]
        public string iso3 { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public decimal? lat { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public decimal? lon { get; set; }
    }
}
