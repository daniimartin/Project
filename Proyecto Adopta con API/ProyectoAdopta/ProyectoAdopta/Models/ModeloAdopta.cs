using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProyectoAdopta.Models
{
    public class ModeloAdopta
    {
        ContextoAnimales contexto;

        //Para que vaya con la API:
        private String UrlApi;
        MediaTypeWithQualityHeaderValue mediaheader;

        public ModeloAdopta()
        {
            this.contexto = new ContextoAnimales();

            //Para que vaya con la API:
            //http://localhost:59859/api/Animales
            this.UrlApi = "http://localhost:59859/";
            this.mediaheader = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //Método para devolver todos los animales en el index
        //public List<Animal> GetAnimales()
        //{
        //    var consulta = from datos in contexto.Animal
        //                   select datos;

        //    return consulta.ToList();
        //}
        //Para que vaya con la API:
        public async Task<List<Animal>> GetAnimales()
        {
            using (HttpClient cliente = new HttpClient())
            {
                String peticion = "api/Animales";
                cliente.BaseAddress = new Uri(this.UrlApi);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(mediaheader);

                HttpResponseMessage respuesta = await cliente.GetAsync(peticion);

                if (respuesta.IsSuccessStatusCode)
                {
                    List<Animal> animales = await respuesta.Content.ReadAsAsync<List<Animal>>();

                    return animales;
                }
                else
                {
                    return null;
                }
            }
        }

        //Método para buscar un animal para mostrar sus detalles
        //public Animal BuscarAnimal(int idanimal)
        //{
        //    var consulta = from datos in contexto.Animal
        //                   where datos.IdAnimal == idanimal
        //                   select datos;
        //    return consulta.FirstOrDefault();
        //}
        //Para que vaya con la API:
        public async Task<Animal> BuscarAnimal(int idanimal)
        {
            using (HttpClient cliente = new HttpClient())
            {
                String peticion = "api/Animales/" + idanimal;
                cliente.BaseAddress = new Uri(this.UrlApi);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(mediaheader);

                HttpResponseMessage respuesta = await cliente.GetAsync(peticion);

                if (respuesta.IsSuccessStatusCode)
                {
                    Animal ani = await respuesta.Content.ReadAsAsync<Animal>();

                    return ani;
                }
                else
                {
                    return null;
                }
            }
        }


        //Método para visualizar el carrito
        public List<Animal> GetAnimalesCarrito(List<int> carrito)
        {
            var consulta = from datos in contexto.Animal
                           where carrito.Contains(datos.IdAnimal)
                           select datos;
            return consulta.ToList();
        }

        //Método para insertar un usuario
        //public void InsertarUsuario (String nombre, String apellidos, DateTime? fecha, String direccion, int? cp, String email, String password)
        //{
        //    Usuario user = new Usuario();
        //    user.Nombre = nombre;
        //    user.Apellidos = apellidos;
        //    user.Fecha_nac = fecha.GetValueOrDefault();
        //    user.Direccion = direccion;
        //    user.CodigoPostal = cp.GetValueOrDefault();
        //    user.Email = email;
        //    user.Contrasena = password;

        //    contexto.Usuario.Add(user);
        //    contexto.SaveChanges();
        //}
        //Para que vaya con la API:
        public async Task InsertarUsuario(String nombre, String apellidos, DateTime? fecha, String direccion, int? cp, String email, String password)
        {
            using (HttpClient cliente = new HttpClient())
            {
                String peticion = "Home/Registro";
                cliente.BaseAddress = new Uri(this.UrlApi);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(mediaheader);
                Usuario user = new Usuario
                {
                    Nombre = nombre,
                    Apellidos = apellidos,
                    Fecha_nac = fecha,
                    Direccion = direccion,
                    CodigoPostal = cp,
                    Email = email,
                    Contrasena = password
                };
                await cliente.PostAsJsonAsync(peticion, user);
            }
        }


        //Método para el login y su cifrado
        //public Usuario usuario { get; set; }
        public Usuario ComprobarUsuario (String emailusuario, String password)
        {
            //Buscamos el usuario por su username
            var consulta = from datos in contexto.Usuario
                           where datos.Email == emailusuario
                           select datos;

            Usuario usuario = consulta.FirstOrDefault();
            if (usuario != null)
            {
               if(usuario.Contrasena == password)
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}