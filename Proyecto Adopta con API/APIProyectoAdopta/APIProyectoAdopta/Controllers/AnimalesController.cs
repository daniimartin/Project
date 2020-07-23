using APIProyectoAdopta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIProyectoAdopta.Controllers
{
    public class AnimalesController : ApiController
    {
        ModeloAnimales modelo;

        public AnimalesController()
        {
            this.modelo = new ModeloAnimales();
        }

        //GET: api/Animales
        public List<Animal> GetAnimales()
        {
            List<Animal> lista = modelo.GetAnimales();
            return lista;
        }

        //GET: api/Animales/2
        public Animal Get(int id)
        {
            return modelo.BuscarAnimal(id);
        }

        // POST: api/Usuarios
        [Route("Registro")]
        public void Post(Usuario user)
        {
            this.modelo.InsertarUsuario(user.Nombre, user.Apellidos, user.Fecha_nac, user.Direccion, user.CodigoPostal, user.Email, user.Contrasena);
        }
    }
}
