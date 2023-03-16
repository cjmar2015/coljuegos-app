using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class EstandarLstFile
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string url { get; set; }

        [JsonProperty(PropertyName = "nameFile")]
        public string nameFile { get; set; }

        [JsonProperty(PropertyName = "contentB64")]
        public byte[] contentB64 { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? updatedAt { get; set; }

        [JsonProperty(PropertyName = "userUpdated")]
        public string userUpdated { get; set; }

        [JsonProperty(PropertyName = "sessionToken")]
        public string sessionToken { get; set; }
    }
}
