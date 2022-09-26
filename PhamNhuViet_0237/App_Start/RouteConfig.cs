using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhamNhuViet_0237
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //danh sach danh muc
            routes.MapRoute(
                name: "Category",
                url: "danh-muc",
                new { controller = "Category", action = "Category"},
                new[] { "PhamNhuViet_0237.Controllers" }
            );

            //danh muc san pham
            routes.MapRoute(
                name: "ProductCategory",
                url: "danh-muc-san-pham/{id}",
                new { controller = "Category", action = "ProductCategory" },
                new[] { "PhamNhuViet_0237.Controllers" }
            );
            //chi tiet san pham

            routes.MapRoute(
                name: "ProductDetail",
                url: "chi-tiet-san-pham/{id}",
                new { controller = "Product", action = "ProductDetail"},
                new[] { "PhamNhuViet_0237.Controllers" }
            );

            //dang ky
            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                new { controller = "Home", action = "Register" },
                new[] { "PhamNhuViet_0237.Controllers" }
            );

            //dang nhap
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                new { controller = "Home", action = "Login" },
                new[] { "PhamNhuViet_0237.Controllers" }
            );
            //gio hang
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                new { controller = "Cart", action = "Cart" },
                new[] { "PhamNhuViet_0237.Controllers" }
            );

            routes.MapRoute(
                name: "Home",
                url: "Trang-chu",
                new { controller = "Home", action = "Home" ,},
                new[] { "PhamNhuViet_0237.Controllers" }
            );

            routes.MapRoute(
                name:"Default",
                url:"{controller}/{action}/{id}",
                new { controller = "Home", action = "Home", id = UrlParameter.Optional },
                new[] { "PhamNhuViet_0237.Controllers" }
            );
        }
    }
}
