using System;
using System.Collections.Generic;
namespace APP.Models
{
    using Newtonsoft.Json;

    public class RotatorSlider
    {
        [JsonProperty(PropertyName = "ID")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "PROYECTOID")]
        public int PROYECTOID { get; set; }

        [JsonProperty(PropertyName = "NOMBRE")]
        public string NOMBRE { get; set; }

        [JsonProperty(PropertyName = "DESCRIPCION")]
        public string DESCRIPCION { get; set; }

        [JsonProperty(PropertyName = "IMAGEN")]
        public string IMAGEN { get; set; }

        [JsonProperty(PropertyName = "ORDEN")]
        public int? ORDEN { get; set; }

        [JsonProperty(PropertyName = "ESTADO")]
        public Boolean ESTADO { get; set; }
    }
}
