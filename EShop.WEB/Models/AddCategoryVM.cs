using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB.Models
{
    public class AddCategoryVM
    {
        public CategoryDetailsVm Category { get; set; }
        public List<AddCategoryPathVM> ParentCategories { get; set; }

    }
    public class AddCategoryPathVM
    {
        public List<Tuple<long, Tuple<string, string>>> Path { get; set; }
        public string PathStr { get; set; }
        public long CategoryId { get; set; }
        
    }
}