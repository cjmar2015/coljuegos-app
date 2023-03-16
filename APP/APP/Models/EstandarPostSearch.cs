using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class EstandarPostSearch
    {
        [JsonProperty(PropertyName = "query")]
        public string query { get; set; }

        [JsonProperty(PropertyName = "currentPag")]
        public int currentPag { get; set; }

        [JsonProperty(PropertyName = "numRecordsByPag")]
        public int numRecordsByPag { get; set; }
    }
}
