using EShop.BLL.DTO;
using EShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB.Models
{
    public class ProductVM
    {
        public long Id { get; set; }
        public long SKUId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string CurrencyName { get; set; }
        public double DiscountPercentage { get; set; }
        public double TotalPrice { get; set; }
        public double OrginalPrice { get; set; }
        public bool IsNew { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDiscounted { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        [AllowHtml]
        public String Text { set; get; }
        public String MetaDescriptions { set; get; }
        public DateTime DateAdded { get; set; }

        public List<Tag> Tags { get; set; }
        public List<ProductRate> Rates { get; set; }
        public List<String> Images { get; set; }


        public int SelectedColor { get; set; }
        public List<ColorValuesDTO> Colors { get; set; }
        public int SelectedSize { get; set; }
        public List<SizeValueDTO> Sizes { get; set; }
    }
}