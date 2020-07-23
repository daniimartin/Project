using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAdopta.Models
{
    public class Usuarios
    {
        [JsonProperty("IdUsuario")]
        public String IdUsuario { get; set; }
        [JsonProperty("Nombre")]
        public String Nombre { get; set; }
        [JsonProperty("Apellidos")]
        public String Apellidos { get; set; }
        [JsonProperty("Email")]
        public String Email { get; set; }
        [JsonProperty("Fecha_nac")]
        public DateTime Fecha_nac { get; set; }
        [JsonProperty("Direccion")]
        public String Direccion { get; set; }
        [JsonProperty("CodigoPostal")]
        public int CodigoPostal { get; set; }
        [JsonProperty("Contrasena")]
        public String Contrasena { get; set; }
    }
}