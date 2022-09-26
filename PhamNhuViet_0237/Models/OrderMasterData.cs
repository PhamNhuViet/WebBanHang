using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhamNhuViet_0237.Models
{
    public partial class OrderMasterData
    {
        [Display(Name = "Mã đơn hàng")]
        public int Id { get; set; }
        [Display(Name = "Tên đơn hàng")]
        public string Name { get; set; }
        [Display(Name = "Mã người dùng")]
        public Nullable<int> UserId { get; set; }
        [Display(Name = "Trạng thái")]
        public Nullable<int> Status { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
    }
}