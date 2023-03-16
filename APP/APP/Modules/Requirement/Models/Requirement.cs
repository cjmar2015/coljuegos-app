using APP.Modules.Requirement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Requirement
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public string profile { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? updatedAt { get; set; }

        [JsonProperty(PropertyName = "userUpdated")]
        public string userUpdated { get; set; }

        [JsonProperty(PropertyName = "sessionToken")]
        public string sessionToken { get; set; }

        [JsonProperty(PropertyName = "lstItem")]
        public List<RequirementItem> lstItem { get; set; }
    }
}
