using EShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EShop.WEB.Models
{
    public class AttributeVM
    {
        public long GroupId { get; set; }

        public string GroupName { get; set; }
        
        public long Id { get; set; }

        [Required(ErrorMessage="الاسم العربي مطلوب")]
        public string ArabicName { get; set; }

         [Required(ErrorMessage = "الاسم الانكليزي مطلوب")]
        public string EnglishName { get; set; }
        public int SortOrder { get; set; }
        public string Name { get; set; }

        public List<AttributeGroupDTO> Groups { get; set; }

    }
}
