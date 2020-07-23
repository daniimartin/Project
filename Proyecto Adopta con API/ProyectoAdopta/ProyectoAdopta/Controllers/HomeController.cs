using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProyectoAdopta.Models;
using ProyectoAdopta.Atributos;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ProyectoAdopta.Controllers
{
    public class HomeController : Controller
    {

        ModeloAdopta modelo;

        public HomeController()
        {
            this.modelo = new ModeloAdopta();
        }

        //public ActionResult Index()
        //{
        //    List<Animal> lista = modelo.GetAnimales();

        //    return View(lista);
        //}
        //Para que vaya con la API:
        public async Task<ActionResult> Index()
        {
            List<Animal> animales = await modelo.GetAnimales();

            return View(animales);
        }

        //public ActionResult Detalles (int? idanimal)
        //{
        //    if (idanimal == null)
        //    {
        //        idanimal = (int)TempData["IDANIMAL"];
        //    }
        //    Animal animal = modelo.BuscarAnimal(idanimal.GetValueOrDefault());

        //    return View(animal);
        //}
        //Para que vaya con la API:
        public async Task<ActionResult> Detalles(int? idanimal)
        {
            if (idanimal == null)
            {
                idanimal = (int)TempData["IDANIMAL"];
            }

            Animal ani = await modelo.BuscarAnimal(idanimal.GetValueOrDefault());

            return View(ani);
        }


        //Este action no tiene vista, es para almacenar en un list los ids de los animales adoptados
        public ActionResult AdoptaAnimal(int idanimal)
        {
            //SESSION
            List<int> animalesadoptados;
            if (Session["CARRITO"] == null)
            {
                animalesadoptados = new List<int>();
            }
            else
            {
                animalesadoptados = Session["CARRITO"]
                    as List<int>;
            }
            animalesadoptados.Add(idanimal);
            //Dentro de Session["CARRITO"] guardo los ids de los animales que le de a adoptar
            Session["CARRITO"] = animalesadoptados;

            TempData["IDANIMAL"] = idanimal;

            return RedirectToAction("Detalles");
        }

        //Action para el carrito
        public ActionResult Carrito()
        {
            List<int> animalesadoptados;

            if (Session["CARRITO"] != null)
            {
                animalesadoptados = Session["CARRITO"] as List<int>;

                if (animalesadoptados.Count != 0)
                {
                    List<Animal> lista = modelo.GetAnimalesCarrito(animalesadoptados);

                    return View(lista);
                }

                return View();
            }

            return View();

        }

        //Vaciar el carrito
        public ActionResult VaciarCarrito()
        {
            Session["CARRITO"] = null;

            return RedirectToAction("Carrito");
        }

        //Cerrar la sesión de usuario
        public ActionResult CerrarSesion()
        {
            HttpContext.User = new GenericPrincipal(new GenericIdentity(""), null);
            System.Web.Security.FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies["cookieusuario"];
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            Session["idusuario"] = null;

            Session["CARRITO"] = null;

            return RedirectToAction("index", "Home");
        }

        //Registro
        public ActionResult Registro()
        {
            return View();
        }
        //Registro: POST
        //[HttpPost]
        //public ActionResult Registro(Usuario user)
        //{
        //    modelo.InsertarUsuario(user.Nombre, user.Apellidos, user.Fecha_nac, user.Direccion, user.CodigoPostal, user.Email, user.Contrasena);

        //    return View();
        //}
        //Insertar con la API:
        [HttpPost]
        public async Task<ActionResult> Registro(Usuario user)
        {
            await modelo.InsertarUsuario(user.Nombre, user.Apellidos, user.Fecha_nac, user.Direccion, user.CodigoPostal, user.Email, user.Contrasena);

            return View();
        }

        //VistaPedido
        [Validacion] //Se pone esto porque esta es la vista que quiero mostrar sólo si está logueado
        public ActionResult VistaPedido()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Ayuda()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}