using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PhamNhuViet_0237.Common;

namespace PhamNhuViet_0237.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities=new PhamNhuViet_2119110237Entities();
        // GET: Admin/Order
        public ActionResult ListOrder(string currentFilter, string SearchString, int? page)
        {
            var lstOrder = new List<Order>();
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
                //lấy ds user theo từ khóa tìm kiếm
                lstOrder = objPhamNhuViet_2119110237Entities.Orders.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all user trong bảng user
                lstOrder = objPhamNhuViet_2119110237Entities.Orders.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng user của 1 trang = 5
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstOrder = lstOrder.OrderByDescending(n => n.Id).ToList();
            return View(lstOrder.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objOrder = objPhamNhuViet_2119110237Entities.OrderDetails.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objOrder = objPhamNhuViet_2119110237Entities.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }

        [HttpPost]
        public ActionResult Delete(Order objOrd)
        {
            var objOrder = objPhamNhuViet_2119110237Entities.Orders.Where(n => n.Id == objOrd.Id).FirstOrDefault();

            objPhamNhuViet_2119110237Entities.Orders.Remove(objOrder);
            objPhamNhuViet_2119110237Entities.SaveChanges();
            return RedirectToAction("ListOrder");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objOrder = objPhamNhuViet_2119110237Entities.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(objOrder);
        }
        //Edit
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Order objOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objPhamNhuViet_2119110237Entities.Entry(objOrder).State = EntityState.Modified;
                    objPhamNhuViet_2119110237Entities.SaveChanges();
                    return RedirectToAction("ListOrder");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(objOrder);
        }
        void LoadData()
        {
            Common objCommon = new Common();
            //lay du lieu danh muc duoi db
            var lstCategory = objPhamNhuViet_2119110237Entities.Categories.ToList();
            //convert sang chon danh sach dang value, text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCategory);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //Loai san pham
            List<OrderType> lstOrderType = new List<OrderType>();
            OrderType objOrderType = new OrderType();
            objOrderType.Id = 01;
            objOrderType.Name = "Chờ xác nhận";
            lstOrderType.Add(objOrderType);

            objOrderType = new OrderType();
            objOrderType.Id = 02;
            objOrderType.Name = "Đã xác nhận";
            lstOrderType.Add(objOrderType);

            objOrderType = new OrderType();
            objOrderType.Id = 03;
            objOrderType.Name = "Đang giao hàng";
            lstOrderType.Add(objOrderType);

            DataTable dtOrderType = converter.ToDataTable(lstOrderType);
            //Convert sang select list dang value, text
            ViewBag.OrderType = objCommon.ToSelectList(dtOrderType, "Id", "Name");
        }
    }
}