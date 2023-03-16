using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string lastName { get; set; }

        [JsonProperty(PropertyName = "phoneNumber")]
        public string phoneNumber { get; set; }
    }
}
