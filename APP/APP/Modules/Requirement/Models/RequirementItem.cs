using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Modules.Requirement.Models
{
    public class RequirementItem
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "itemTitle")]
        public string itemTitle { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? updatedAt { get; set; }

        [JsonProperty(PropertyName = "userUpdated")]
        public string userUpdated { get; set; }

        [JsonProperty(PropertyName = "requerimentId")]
        public int requerimentId { get; set; }

        [JsonProperty(PropertyName = "sessionToken")]
        public string sessionToken { get; set; }
    }
}
