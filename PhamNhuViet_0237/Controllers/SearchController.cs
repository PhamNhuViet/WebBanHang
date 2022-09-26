using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhamNhuViet_0237.Controllers
{
    public class SearchController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities = new PhamNhuViet_2119110237Entities();
        // GET: Search
        public ActionResult Search(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
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
                //lấy danh sách sản phẩm theo từ khóa tìm kiếm
                lstProduct = objPhamNhuViet_2119110237Entities.Products.Where(n => n.Name.Contains(SearchString)|| n.UnsignName.Contains(SearchString)).ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang = 5
            ViewBag.NumberSearch = lstProduct.Count();
            //số lượng sản phẩm
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
    }
}