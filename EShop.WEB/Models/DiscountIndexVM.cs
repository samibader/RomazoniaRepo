using EShop.BLL.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class DiscountIndexVM
    {
        public IPagedList<DiscountDTO> Discounts { get; set; }
        public DiscountFiltersVM filters { get; set; }
    }
}