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
    public class CategoryController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities=new PhamNhuViet_2119110237Entities();
        // GET: Admin/Category
        public ActionResult ListCategory(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
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
                lstCategory = objPhamNhuViet_2119110237Entities.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all sản phẩm trong bảng product
                lstCategory = objPhamNhuViet_2119110237Entities.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng item của 1 trang = 5
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create(Category objCategory)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    //xu ly them slug
                    objCategory.Slug = Xstring.Str_Slug(objCategory.Name);
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objPhamNhuViet_2119110237Entities.Categories.Add(objCategory);
                    objPhamNhuViet_2119110237Entities.SaveChanges();
                    return RedirectToAction("ListCategory");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objPhamNhuViet_2119110237Entities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objPhamNhuViet_2119110237Entities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(Category objCat)
        {
            var objCategory = objPhamNhuViet_2119110237Entities.Categories.Where(n => n.Id == objCat.Id).FirstOrDefault();

            objPhamNhuViet_2119110237Entities.Categories.Remove(objCategory);
            objPhamNhuViet_2119110237Entities.SaveChanges();
            return RedirectToAction("ListCategory");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objCategory = objPhamNhuViet_2119110237Entities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        //Edit
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Category objCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //xu ly them slug
                    objCategory.Slug = Xstring.Str_Slug(objCategory.Name);
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objPhamNhuViet_2119110237Entities.Entry(objCategory).State = EntityState.Modified;
                    objPhamNhuViet_2119110237Entities.SaveChanges();
                    return RedirectToAction("ListCategory");
                }
                catch (Exception)
                {
                    return View(objCategory);
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
            List<CategoryType> lstCategoryType = new List<CategoryType>();
            CategoryType objCategoryType = new CategoryType();
            objCategoryType.Id = 01;
            objCategoryType.Name = "Phổ biến";
            lstCategoryType.Add(objCategoryType);

            objCategoryType = new CategoryType();
            objCategoryType.Id = 02;
            objCategoryType.Name = "Mặc định";
            lstCategoryType.Add(objCategoryType);

            DataTable dtCategoryType = converter.ToDataTable(lstCategoryType);
            //Convert sang select list dang value, text
            ViewBag.CategoryType = objCommon.ToSelectList(dtCategoryType, "Id", "Name");
        }
    }
}