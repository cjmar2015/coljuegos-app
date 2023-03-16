using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Faq
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "question")]
        public string question { get; set; }

        [JsonProperty(PropertyName = "response")]
        public string response { get; set; }

        [JsonProperty(PropertyName = "published")]
        public bool published { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime updatedAt { get; set; }

        [JsonProperty(PropertyName = "userUpdated")]
        public string userUpdated { get; set; }

        [JsonProperty(PropertyName = "sessionToken")]
        public string sessionToken { get; set; }
    }
}
