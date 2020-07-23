using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoAdopta.Atributos
{
    public class ValidacionAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                GenericPrincipal usuario = HttpContext.Current.User as GenericPrincipal;
            }
            else
            {
                RouteValueDictionary rutalogin =
                    new RouteValueDictionary(new
                    {
                        controller = "Validacion",
                        action = "Login"
                    });
                RedirectToRouteResult direccionlogin =
                    new RedirectToRouteResult(rutalogin);
                filterContext.Result = direccionlogin;
            }
        }
    }
}