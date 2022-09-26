using PagedList;
using PhamNhuViet_0237.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PhamNhuViet_0237.Controllers
{
    public class HomeController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities=new PhamNhuViet_2119110237Entities();
        // GET: Home
        public ActionResult Home()
        {
            //ket loi csdl de lay ds danh muc
            var lstCategory=new List<Category>();
            lstCategory=objPhamNhuViet_2119110237Entities.Categories.ToList();

            var lstProduct=new List<Product>();
            //lay san pham giam gia soc
            lstProduct=objPhamNhuViet_2119110237Entities.Products.ToList();

            HomeModel objHomeModel=new HomeModel();
            objHomeModel.ListCategories=lstCategory;
            objHomeModel.ListProducts=lstProduct;

            return View(objHomeModel);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            string pattern = "^[a-z0-9](\\.?[a-z0-9]){5,}@g(oogle)?mail\\.com$";
            if (ModelState.IsValid && Regex.IsMatch(_user.Email,pattern))
            {               
                var check = objPhamNhuViet_2119110237Entities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objPhamNhuViet_2119110237Entities.Configuration.ValidateOnSaveEnabled = false;
                    objPhamNhuViet_2119110237Entities.Users.Add(_user);
                    objPhamNhuViet_2119110237Entities.SaveChanges();
                    return RedirectToAction("Home");
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
            return View();
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

        // LOGIN
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = objPhamNhuViet_2119110237Entities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["Admin"]=data.FirstOrDefault().isAdmin;
                    return RedirectToAction("Home");
                }
                else
                {
                    ViewBag.error = "Đăng nhập thất bại";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}