using DemoApp.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoApp.Core.ViewModel
{
    public class BasePagingRequestViewModel
    {

        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? PageSize { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int? PageIndex { get; set; }
        public string Search { get; set; }
        public void SetDefaultPage()
        {
            PageSize = PageSize ?? Constant.DEFAULT_PAGE_SIZE;
            PageIndex = PageIndex ?? Constant.DEFAULT_PAGE_INDEX;
            PageSize = PageSize > Constant.MAX_PAGE_SIZE ? Constant.MAX_PAGE_SIZE : PageSize;
        }

    }
}
