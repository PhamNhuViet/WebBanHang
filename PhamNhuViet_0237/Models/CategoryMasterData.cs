using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhamNhuViet_0237.Models
{
    public partial class CategoryMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nhập tên danh mục")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }
        [Display(Name = "Hình đại diện")]
        public string Avatar { get; set; }
        public string Slug { get; set; }
        [Display(Name = "Hiển thị trang chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> Deleted { get; set; }
        [Display(Name = "Thời gian tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Thời gian cập nhật")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
        [Display(Name = "Loại danh mục")]
        public Nullable<int> isPopular { get; set; }
    }
}