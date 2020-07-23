using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace ProyectoAdopta.Models
{
    public class Animals
    {
        [JsonProperty("IdAnimal")]
        public int IdAnimal { get; set; }
        [JsonProperty("IdCategoria")]
        public int IdCategoria { get; set; }
        [JsonProperty("Nombre")]
        public String Nombre { get; set; }
        [JsonProperty("Fecha_nac")]
        public DateTime Fecha_nac { get; set; }
        [JsonProperty("Descripcion")]
        public String Descripcion { get; set; }
        [JsonProperty("Imagen")]
        public String Imagen { get; set; }
        [JsonProperty("Peso")]
        public int Peso { get; set; }

    }
}