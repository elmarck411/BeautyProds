using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeautyProds
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                //url: "{controller}/{action}/{id}",
                url: "TestDeploy.html",
                defaults: new { controller = "BottleRequests", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
