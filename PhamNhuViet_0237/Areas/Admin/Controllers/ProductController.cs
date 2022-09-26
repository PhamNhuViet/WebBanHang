using PagedList;
using PhamNhuViet_0237.Libary;
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
    public class ProductController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities = new PhamNhuViet_2119110237Entities();

        // GET: Admin/Product
        public ActionResult ListProduct(string currentFilter, string SearchString, int? page)
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
                //lấy ds sản phẩm theo từ khóa tìm kiếm
                lstProduct = objPhamNhuViet_2119110237Entities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstProduct = objPhamNhuViet_2119110237Entities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang = 5
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }
        //Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    //xu ly them slug
                    objProduct.Slug = Xstring.Str_Slug(objProduct.Name);
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objPhamNhuViet_2119110237Entities.Products.Add(objProduct);
                    objPhamNhuViet_2119110237Entities.SaveChanges();
                    return RedirectToAction("ListProduct");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objPhamNhuViet_2119110237Entities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objPhamNhuViet_2119110237Entities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            try
            {
                var objProduct = objPhamNhuViet_2119110237Entities.Products.Find(objPro.Id);
                objPhamNhuViet_2119110237Entities.Products.Remove(objProduct);
                objPhamNhuViet_2119110237Entities.SaveChanges();
                return RedirectToAction("ListProduct", "Product");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objPhamNhuViet_2119110237Entities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        //Edit
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //xu ly them slug
                    objProduct.Slug = Xstring.Str_Slug(objProduct.Name);
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objPhamNhuViet_2119110237Entities.Entry(objProduct).State = EntityState.Modified;
                    objPhamNhuViet_2119110237Entities.SaveChanges();
                    return RedirectToAction("ListProduct");
                }
                catch (Exception)
                {
                    return View(objProduct);
                }
            }
            return View();
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
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 00;
            objProductType.Name = "Mặc định";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //Convert sang select list dang value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}