using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
          //  routes.MapRoute(
          //    name: "null",
          //    url: "page{page}",
          //    defaults: new { controller = "Product", action = "List" }
          //);
           // routes.MapRoute(
           //    name: "",
           //    url: "{page}/{category}",
           //    defaults: new { controller = "Product", action = "List",page=1,category=(string)null }
           //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
