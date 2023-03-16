using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Sesion
    {
        [JsonProperty(PropertyName = "codMsg")]
        public int codMsg { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public string msg { get; set; }

        [JsonProperty(PropertyName = "processIsSuccessful")]
        public bool processIsSuccessful { get; set; }

        [JsonProperty(PropertyName = "numRecordsByPag")]
        public int numRecordsByPag { get; set; }

        [JsonProperty(PropertyName = "currentPag")]
        public int currentPag { get; set; }

        [JsonProperty(PropertyName = "maxPag")]
        public int maxPag { get; set; }

        [JsonProperty(PropertyName = "maxRecords")]
        public int maxRecords { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        [JsonProperty(PropertyName = "rol")]
        public object rol { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string token { get; set; }

        [JsonProperty(PropertyName = "email")]
        public object email { get; set; }

        [JsonProperty(PropertyName = "session")]
        public string session { get; set; }

        [JsonProperty(PropertyName = "loginAt")]
        public string loginAt { get; set; }

        [JsonProperty(PropertyName = "expiredAt")]
        public string expiredAt { get; set; }

        [JsonProperty(PropertyName = "loginOk")]
        public bool loginOk { get; set; }
    }
}
