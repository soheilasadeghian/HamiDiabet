using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HamiDiabet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "LoginPage",
                url: "LoginPage",
                defaults: new { controller = "Home", action = "LoginPage", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ErrorPage",
                url: "ErrorPage",
                defaults: new { controller = "Home", action = "ErrorPage", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Articles",
            //    url: "Articles",
            //    defaults: new { controller = "Home", action = "Articles", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Articles",
                url: "Articles/{page}",
                defaults: new { controller = "Home", action = "Articles", page = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "SignIn",
                url: "SignIn",
                defaults: new { controller = "Home", action = "SignIn", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "SignUp",
               url: "SignUp",
               defaults: new { controller = "Home", action = "SignUp", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "SignOut",
                url: "SignOut",
                defaults: new { controller = "Home", action = "SignOut", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Default",
               url: "",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}
