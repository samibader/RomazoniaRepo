using EShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class AttributeGroupVM
    {
        public long Id { get; set; }
        [Required(ErrorMessage="الا سم العربي مطلوب")]
        public string ArabicName { get; set; }

        [Required(ErrorMessage = "الا سم الانكليزي مطلوب")]
        public string EnglishName { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "ترتيب الفرز مطلوب")]
        public int SortOrder { get; set; }
        public List<AttributeVM> Attributes { get; set; }


    }
}