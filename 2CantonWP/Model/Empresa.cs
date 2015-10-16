using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2CantonWP.Model
{
    public class Empresa
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "direccion")]
        public string Direccion { get; set; }
          
        [JsonProperty(PropertyName = "longitud")]
        public float Longitud { get; set; }

        [JsonProperty(PropertyName = "latitud")]
        public float Latitud { get; set; }

        [JsonProperty(PropertyName = "telefonoprincipal")]
        public string TelefonoPrincipal { get; set; }

        [JsonProperty(PropertyName = "telefonosecundario")]
        public string TelefonoSecundario { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "web")]
        public string Web { get; set; }

        [JsonProperty(PropertyName = "horario")]
        public string Horario { get; set; }

        [JsonProperty(PropertyName = "idtipoempresa")]
        public string IdTipoEmpresa { get; set; }

        [JsonProperty(PropertyName = "urlimagen")]
        public string UrlImagen { get; set; }
    }
}
