using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB.Models
{
    public class SearchVM
    {
        public String SearchText { get; set; }
        public Int64 CategoryId { get; set; }

        //Should be changed to CategoryLink .. 
        public CategoryMenuVM Categories { get; set; }
    }
}