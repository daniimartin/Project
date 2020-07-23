using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace APIProyectoAdopta.Models
{
    public class ModeloAnimales
    {
        ContextoAnimales contexto;

        public ModeloAnimales()
        {
            this.contexto = new ContextoAnimales();
        }

        //Listar animales
        public List<Animal> GetAnimales()
        {
            var consulta = from datos in contexto.Animal
                           select datos;

            return consulta.ToList();
        }

        //Buscar un animal
        public Animal BuscarAnimal(int id)
        {
            var consulta = from datos in contexto.Animal
                           where datos.IdAnimal == id
                           select datos;

            return consulta.FirstOrDefault();
        }

        //Insertar usuario
        public void InsertarUsuario(String nombre, String apellidos, DateTime? fecha, String direccion, int? cp, String email, String password)
        {
            Usuario user = new Usuario();
            user.Nombre = nombre;
            user.Apellidos = apellidos;
            user.Fecha_nac = fecha.GetValueOrDefault();
            user.Direccion = direccion;
            user.CodigoPostal = cp.GetValueOrDefault();
            user.Email = email;
            user.Contrasena = password;

            contexto.Usuario.Add(user);
            contexto.SaveChanges();
        }
        
    }
}