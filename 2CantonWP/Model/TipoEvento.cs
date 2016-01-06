using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2CantonWP.Model
{
    class TipoEvento
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "urlimagen")]
        public string UrlImagen { get; set; }

        [JsonProperty(PropertyName = "cantidad_eventos")]
        public int Cantidad { get; set; }
    }
}
