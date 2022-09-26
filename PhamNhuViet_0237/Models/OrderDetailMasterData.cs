using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhamNhuViet_0237.Models
{
    public partial class OrderDetailMasterData
    {
        [Display(Name = "Mã đơn hàng")]
        public int Id { get; set; }
        [Display(Name = "Mã đơn hàng")]
        public int OrderId { get; set; }
        [Display(Name = "Mã sản phẩm")]
        public int ProductId { get; set; }
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
    }
}