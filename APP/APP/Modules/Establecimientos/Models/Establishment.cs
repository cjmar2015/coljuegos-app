using APP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace APP.Modules.Establecimientos.Models
{
    public class Establishment
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string address { get; set; }

        [JsonProperty(PropertyName = "opertor")]
        public string opertor { get; set; }

        [JsonProperty(PropertyName = "codContract")]
        public string codContract { get; set; }

        [JsonProperty(PropertyName = "nuc")]
        public string nuc { get; set; }

        [JsonProperty(PropertyName = "cityId")]
        public int cityId { get; set; }

        [JsonProperty(PropertyName = "cityName")]
        public string cityName { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime updatedAt { get; set; }

        [JsonProperty(PropertyName = "userUpdated")]
        public string userUpdated { get; set; }

        [JsonProperty(PropertyName = "sessionToken")]
        public string sessionToken { get; set; }

        [JsonProperty(PropertyName = "lstMachine")]
        public List<Machine> lstMachine { get; set; }
    }
}
