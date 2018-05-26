using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;
using EShop.BLL.DTO;

namespace EShop.WEB.Models
{
    public class ProductIndexVM
    {
        public IPagedList<ProductIndexManagerVM> Products { get; set; }
        public ProductManagerFiltersVM filters { get; set; }
        public List<SizeCategoryDTO> SizeCategories { get; set; }
       
    }
}