using PhamNhuViet_0237.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhamNhuViet_0237.Controllers
{
    public class CartController : Controller
    {
        PhamNhuViet_2119110237Entities objPhamNhuViet_2119110237Entities=new PhamNhuViet_2119110237Entities();
        // GET: Cart
        public ActionResult Cart()
        {
            return View((List<CartModel>)Session["Cart"]);
        }
        public ActionResult AddToCart(int Id,int quantity)
        {
            if (Session["cart"] == null)
            {
                List<CartModel> cart=new List<CartModel>();
                cart.Add(new CartModel { Product=objPhamNhuViet_2119110237Entities.Products.Find(Id),Quantity= quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel>cart=(List<CartModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa
                int index = isExist(Id);
                if (index != -1)
                {
                    //nếu sản phẩm tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel { Product = objPhamNhuViet_2119110237Entities.Products.Find(Id), Quantity = quantity });
                    //tính lại số sản phẩm trong giỏ hàng
                    Session["count"]=Convert.ToInt32(Session["count"])+1;
                }
                Session["cart"]=cart;
            }

            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for(int i = 0; i < cart.Count; i++)
            {
                if(cart[i].Product.Id.Equals(id))
                    return i;
            }
            return -1;
        }

        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(int id)
        {
            List<CartModel> li = (List<CartModel>)Session["Cart"];
            li.RemoveAll(n=>n.Product.Id==id);
            Session["Cart"]=li;
            Session["Count"]=Convert.ToInt32(Session["Count"])-1;
            return Json(new { Message ="Thành công",JsonRequestBehavior.AllowGet});
        }
    }
}