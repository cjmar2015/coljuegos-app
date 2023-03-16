using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Models
{
    public class Notification
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string subject { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string message { get; set; }

        [JsonProperty(PropertyName = "channel")]
        public string channel { get; set; }

        [JsonProperty(PropertyName = "userDstId")]
        public int userDstId { get; set; }

        [JsonProperty(PropertyName = "userDstUsername")]
        public string userDstUsername { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? createdAt { get; set; }

        [JsonProperty(PropertyName = "readAt")]
        public DateTime? readAt { get; set; }

        [JsonProperty(PropertyName = "lstUserDstUsername")]
        public List<string> lstUserDstUsername { get; set; }
    }
}
