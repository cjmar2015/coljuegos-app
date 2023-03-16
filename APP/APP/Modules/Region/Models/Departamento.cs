using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Departamento
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "countryName")]
        public string countryName { get; set; }

        [JsonProperty(PropertyName = "countryId")]
        public int countryId { get; set; }

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
