using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhamNhuViet_0237.Controllers
{
    public class CategoryController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities=new PhamNhuViet_2119110237Entities();  
        // GET: Category
        public ActionResult Category()
        {
            var lstCategory=objPhamNhuViet_2119110237Entities.Categories.ToList();
            return View(lstCategory);
        }

        public ActionResult ProductCategory(int id, int? page, string currentFilter, string SearchString)
        {
            var listProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                listProduct = objPhamNhuViet_2119110237Entities.Products.Where(n=>n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                listProduct = objPhamNhuViet_2119110237Entities.Products.ToList();
            }
            if (page == null)
            {
                page = 1;
            }
            ViewBag.currentFilter = SearchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            listProduct =objPhamNhuViet_2119110237Entities.Products.Where(n=>n.CategoryId==id).ToList(); 
            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }
    }
}