using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace EShop.WEB.Models
{
    public class AttributeIndexVM
    {
        public IPagedList<AttributeVM> Attributes { get; set; }
        public AttributeFilter filters { get; set; }
    }
}