using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ProyectoAdopta
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["cookieusuario"];
            if (cookie != null)
            {
                String datos = cookie.Value;
                Debug.Write("--------------------------->" + cookie.Value + "<-----------------------");
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(datos);
                String nombreusuario = ticket.Name;
                int IdUsuario = int.Parse(ticket.UserData);
                GenericIdentity identidad = new GenericIdentity(nombreusuario);
                GenericPrincipal usuario = new GenericPrincipal(identidad,
                    new String[] { IdUsuario.ToString() });
                HttpContext.Current.User = usuario;
            }
        }
    }
}
