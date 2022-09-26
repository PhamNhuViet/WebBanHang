using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static PhamNhuViet_0237.Common;

namespace PhamNhuViet_0237.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities = new PhamNhuViet_2119110237Entities();
        // GET: Admin/User
        public ActionResult ListUser(string currentFilter, string SearchString, int? page)
        {
            var lstUser = new List<User>();
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
                lstUser = objPhamNhuViet_2119110237Entities.Users.Where(n => n.FirstName.Contains(SearchString)).ToList();
            }
            else
            {
                //lấy all user trong bảng user
                lstUser = objPhamNhuViet_2119110237Entities.Users.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //số lượng user của 1 trang = 5
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //sắp xếp theo id sản phẩm, sp mới đưa lên đầu
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }

        //tạo người dùng mới
        [HttpGet]
        public ActionResult CreateUser()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User objUser)
        {
            string pattern = "^[a-z0-9](\\.?[a-z0-9]){5,}@g(oogle)?mail\\.com$";
            if (ModelState.IsValid && Regex.IsMatch(objUser.Email, pattern))
            {
                var check = objPhamNhuViet_2119110237Entities.Users.FirstOrDefault(s => s.Email == objUser.Email);
                if(check == null)
                {
                    objUser.Password = GetMD5(objUser.Password);
                    objPhamNhuViet_2119110237Entities.Configuration.ValidateOnSaveEnabled = false;
                    objPhamNhuViet_2119110237Entities.Users.Add(objUser);
                    objPhamNhuViet_2119110237Entities.SaveChanges();
                    return RedirectToAction("ListUser");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "Email không hợp lệ";
            }
            return View(objUser);
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        //chi tiết người dùng
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objUser = objPhamNhuViet_2119110237Entities.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }

        //xóa người dùng
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objUser = objPhamNhuViet_2119110237Entities.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }

        [HttpPost]
        public ActionResult Delete(User objUse)
        {
            var objUser = objPhamNhuViet_2119110237Entities.Users.Where(n => n.Id == objUse.Id).FirstOrDefault();

            objPhamNhuViet_2119110237Entities.Users.Remove(objUser);
            objPhamNhuViet_2119110237Entities.SaveChanges();
            return RedirectToAction("ListUser");
        }

        //chỉnh sửa thông tin người dùng
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objUser = objPhamNhuViet_2119110237Entities.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(objUser);
        }

        [HttpPost]
        public ActionResult Edit(User objUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objPhamNhuViet_2119110237Entities.Entry(objUser).State = EntityState.Modified;
                    objPhamNhuViet_2119110237Entities.SaveChanges();
                    return RedirectToAction("ListUser");
                }
                catch (Exception)
                {
                    return View(objUser);
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
            List<UserType> lstUserType = new List<UserType>();
            UserType objUserType = new UserType();
            objUserType.IsAdmin = true;
            objUserType.Name = "Quản trị viên";
            lstUserType.Add(objUserType);

            objUserType = new UserType();
            objUserType.IsAdmin = false;
            objUserType.Name = "Người dùng";
            lstUserType.Add(objUserType);

            DataTable dtUserType = converter.ToDataTable(lstUserType);
            //Convert sang select list dang value, text
            ViewBag.UserType = objCommon.ToSelectList(dtUserType, "isAdmin", "Name");
        }
    }
}