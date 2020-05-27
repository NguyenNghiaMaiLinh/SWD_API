using System;
using System.Collections.Generic;

namespace DemoApp_API.Models
{
    public partial class Task
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public bool? IsDelete { get; set; }
        public int? Status { get; set; }
    }
}
