using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Login
    {
        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string password { get; set; }
    }
}
