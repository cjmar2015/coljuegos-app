using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Modules.Establecimientos.Models
{
    public class Machine
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "cod")]
        public string cod { get; set; }

        [JsonProperty(PropertyName = "brand")]
        public string brand { get; set; }

        [JsonProperty(PropertyName = "codBet")]
        public string codBet { get; set; }

        [JsonProperty(PropertyName = "isMetOnline")]
        public bool isMetOnline { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime updatedAt { get; set; }

        [JsonProperty(PropertyName = "userUpdated")]
        public string userUpdated { get; set; }

        [JsonProperty(PropertyName = "establishmentId")]
        public int establishmentId { get; set; }

        [JsonProperty(PropertyName = "establishmentName")]
        public string establishmentName { get; set; }
    }
}
