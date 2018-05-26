using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
namespace EShop.WEB.Models
{
    public class AttributeGroupIndexVM
    {
        public IPagedList<AttributeGroupVM> AttributeGroups { get; set; }
        public AttributeGroupFilter filters { get; set; }
        
    }
}