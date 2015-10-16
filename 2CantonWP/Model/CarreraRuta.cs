using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2CantonWP.Model
{
    public class CarreraRuta
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "idruta")]
        public string idRuta { get; set; }

        [JsonProperty(PropertyName = "idsitiosalida")]
        public string idSitioSalida { get; set; }

        [JsonProperty(PropertyName = "idhorario")]
        public string idHorario { get; set; }

        [JsonProperty(PropertyName = "nota")]
        public string nota { get; set; }

        [JsonProperty(PropertyName = "hora")]
        public string hora { get; set; }
    }
}
