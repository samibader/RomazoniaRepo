using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class CategoryPathVM
    {
        public  List<Tuple<string,string>> CategoryNames { get; set; }
    }
}