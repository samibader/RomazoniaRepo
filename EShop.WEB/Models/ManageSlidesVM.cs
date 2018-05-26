using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class ManageSlidesVM
    {
        public IPagedList<SlideVM> Slides { get; set; }
        public SlideFilter filters { get; set; }
    }
    public class SlideFilter
    {
        public int PageSize { get; set; }

        public int PageNum { get; set; }
    }
}