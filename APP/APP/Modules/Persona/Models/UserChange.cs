using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class UserChange
    {
        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        [JsonProperty(PropertyName = "tmpPassword")]
        public string tmpPassword { get; set; }

        [JsonProperty(PropertyName = "newPassword")]
        public string newPassword { get; set; }
    }
}
