﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2CantonWP.Model
{
    public class Ruta
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        public string UrlImagen { get; set; }

        [JsonProperty(PropertyName = "idempresa")]
        public string IdEmpresa { get; set; }
    }
}
