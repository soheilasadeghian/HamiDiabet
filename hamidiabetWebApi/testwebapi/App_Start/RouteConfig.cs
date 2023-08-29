using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HamiDiabetWebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           // routes.MapRoute(
           //    name: "testView",
           //    url: "testView",
           //    defaults: new { controller = "testView", action = "testView", id = UrlParameter.Optional }
           //);
           // routes.MapRoute(
           //     name: "Default",
           //     url: "{controller}/{action}/{id}",
           //     defaults: new { action = "Index", id = UrlParameter.Optional }
           // );
        }
    }
}

