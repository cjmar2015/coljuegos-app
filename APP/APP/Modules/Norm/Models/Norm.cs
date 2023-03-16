using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Norm
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "description")] 
        public string description { get; set; }

        [JsonProperty(PropertyName = "publish")]
        public bool publish { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? updatedAt { get; set; }

        [JsonProperty(PropertyName = "userUpdated")]
        public string userUpdated { get; set; }

        [JsonProperty(PropertyName = "sessionToken")]
        public string sessionToken { get; set; }

        [JsonProperty(PropertyName = "lstFiles")]
        public List<EstandarLstFile> lstFiles { get; set; }
    }
}
