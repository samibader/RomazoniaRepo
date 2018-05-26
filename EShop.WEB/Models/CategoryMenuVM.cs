using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EShop.WEB.Simulation_Data;

namespace EShop.WEB.Models
{
    public class CategoryMenuVM
    {
        public List<CategoryMenuVM> SubCategories { get; set; }
        public String EnglishName { set; get; }
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public String ImageUrl { get; set; }
        public String ImageThumb { get; set; }
        public String Banner { get; set; }
    }
}