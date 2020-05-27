using System;
using System.Collections.Generic;

namespace DemoApp_API.Models
{
    public partial class Product
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public decimal? Price { get; set; }
        public bool? IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
