using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhamNhuViet_0237.Controllers
{
    public class ProductController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities=new PhamNhuViet_2119110237Entities();
        // GET: Product

        public ActionResult ProductDetail(int id)
        {
            var objProduct=objPhamNhuViet_2119110237Entities.Products.Where(n=>n.Id==id).First();
            return View(objProduct);
        }
    }
}