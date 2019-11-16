using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace hn.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           // //路由规则匹配是从上到下的，优先匹配的路由一定要写在最上面。因为路由匹配成功以后，他不会继续匹配下去。
           // routes.MapRoute(
           //    "Admin", // 路由名称，这个只要保证在路由集合中唯一即可
           //    "Admin/{controller}/{action}/{id}", //路由规则,匹配以Admin开头的url
           //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
           //    new string[] { "hn.Mvc.Controllers.Admin" }// 
           //);

            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "hn.Mvc.Controllers" }// 
            );
        }
    }
}