using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "email")]
        public string email { get; set; }

        [JsonProperty(PropertyName = "newPassword")]
        public string newPassword { get; set; }

        [JsonProperty(PropertyName = "person")]
        public Person person { get; set; }
    }
}
