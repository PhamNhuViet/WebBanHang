using PhamNhuViet_0237.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhamNhuViet_0237.Controllers
{
    public class PaymentController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities= new PhamNhuViet_2119110237Entities();
        // GET: Payment
        public ActionResult Payment()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //lấy thông tin từ giỏ hàng từ biến sesssion
                var lstCart = (List<CartModel>)Session["Cart"];

                //gán dữ liệu cho bảng order
                Order objOrder=new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss");
                objOrder.UserId=int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objPhamNhuViet_2119110237Entities.Orders.Add(objOrder);
                //lưu thông tin dữ liệu vào bảng order
                objPhamNhuViet_2119110237Entities.SaveChanges();

                //lấy orderId vừa tạo để lưu vào bảng orderDetail
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail obj=new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objPhamNhuViet_2119110237Entities.OrderDetails.AddRange(lstOrderDetail);
                objPhamNhuViet_2119110237Entities.SaveChanges();
            }
            return View();
        }
    }
}