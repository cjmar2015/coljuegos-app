using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Ciudad
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "regionName")]
        public string regionName { get; set; }

        [JsonProperty(PropertyName = "regionId")]
        public int regionId { get; set; }

        [JsonProperty(PropertyName = "cod")]
        public object cod { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public decimal? lat { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public decimal? lon { get; set; }
    }
}
