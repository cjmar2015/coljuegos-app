using System;
using System.Collections.Generic;
namespace APP.Models
{
    using Newtonsoft.Json;
    public class Estandar<T>
    {
        [JsonProperty(PropertyName = "codMsg")]
        public int codMsg { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public object msg { get; set; }

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

        [JsonProperty(PropertyName = "id")]
        public int? id { get; set; }

        [JsonProperty(PropertyName = "lst")]
        public List<T> lst { get; set; }

        [JsonProperty(PropertyName = "obj")]
        public T obj { get; set; }

    }
}
