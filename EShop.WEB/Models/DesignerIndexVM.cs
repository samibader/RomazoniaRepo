using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;


namespace EShop.WEB.Models
{
    public class DesignerIndexVM
    {
        public IPagedList<DesignerVM> Designers { get; set; }
        public DesignerFilter filters { get; set; }
    }
}