using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class SizeAttributeDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage="الاسم العربي مطلوب")]
        public string ArabicName { get; set; }

         [Required(ErrorMessage = "الاسم الانكليزي مطلوب")]
        public string EnglishName { get; set; }

        public string Name { get; set; }

        public long SizeCategoryId { get; set; }

        public DateTime DateModified { get; set; }
    }
}
