using ProyectoAdopta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoAdopta.Controllers
{
    public class ValidacionController : Controller
    {

        ModeloAdopta modelo;

        public ValidacionController()
        {
            this.modelo = new ModeloAdopta();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }
        //Login: POST
        [HttpPost]
        public ActionResult Login(String emailusuario, String password)
        {
            if (modelo.ComprobarUsuario(emailusuario, password) != null)
            {
                String NombreUsuario = modelo.ComprobarUsuario(emailusuario, password).Nombre;
                int IdUsuario = modelo.ComprobarUsuario(emailusuario, password).IdUsuario;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    NombreUsuario,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(3),
                    true,
                    IdUsuario.ToString(),
                    FormsAuthentication.FormsCookiePath
                );
                String ticketcifrado = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie("cookieusuario", ticketcifrado);
                Response.Cookies.Add(cookie);
                Session["idusuario"] = IdUsuario;


                return RedirectToAction("VistaPedido", "Home");
            }
            else
            {
                ViewBag.Mensaje = "El usuario no existe";
            }

            return View();
        }
    }
}