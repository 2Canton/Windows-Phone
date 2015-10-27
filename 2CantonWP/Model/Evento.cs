using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2CantonWP.Model
{
    public class Evento
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty(PropertyName = "costo")]
        public string Costo { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public string Fecha { get; set; }


        [JsonProperty(PropertyName = "fecha_aux")]
        public DateTime FechaAux { get; set; }

        [JsonProperty(PropertyName = "hora")]
        public string Hora { get; set; }

        [JsonProperty(PropertyName = "urlimagen")]
        public string UrlImagen { get; set; }

        [JsonProperty(PropertyName = "idtipoevento")]
        public string IdTipoEvento { get; set; }

        [JsonProperty(PropertyName = "longitud")]
        public double Longitud { get; set; }

        [JsonProperty(PropertyName = "latitud")]
        public double Latitud { get; set; }

        [JsonProperty(PropertyName = "telefono")]
        public string Telefono { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "website")]
        public string WebSite { get; set; }

        [JsonProperty(PropertyName = "visible")]
        public bool Visible { get; set; }
    }
}
