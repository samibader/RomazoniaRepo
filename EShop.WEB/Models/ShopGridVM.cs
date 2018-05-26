using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShop.BLL.DTO;
using EShop.Common;
using PagedList;

namespace EShop.WEB.Models
{
    public class ShopGridVM
    {
        public IPagedList<ProductDTO> ProductDtos { get; set; }
        public List<DesignerFilterVM> Designers { get; set; }
        public List<CategoryFilterVM> Categories { get; set; }
        public List<ColorFilterVM> Colors { get; set; }
        public List<SizeFilterVM> Sizes { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public String Show { get; set; }
        public Sorts SortBy { get; set; }
        public String  categoryName { get; set; }
        public String  Name { get; set; }
        public String Banner { get; set; }
        public List<TagVM> Tags { get; set; }
        public ShopGridVM()
        {
            MinPrice = 0;
            MaxPrice = 9999;
        }
    }
}