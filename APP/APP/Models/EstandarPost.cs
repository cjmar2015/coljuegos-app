using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class EstandarPost
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        //[JsonProperty(PropertyName = "sessionToken")]
        //public string sessionToken { get; set; }
    }
}
