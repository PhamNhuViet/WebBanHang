using PhamNhuViet_0237.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhamNhuViet_0237
{
    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {

    }
    [MetadataType(typeof(OrderMasterData))]
    public partial class Order
    {

    }
    [MetadataType(typeof(OrderDetailMasterData))]
    public partial class OrderDetail
    {

    }

    [MetadataType(typeof(ProductMasterData))]
    public partial class Product
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(CategoryMasterData))]
    public partial class Category
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}