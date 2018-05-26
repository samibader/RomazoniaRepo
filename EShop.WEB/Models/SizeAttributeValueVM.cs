using EShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class SizeAttributeValueVM
    {
        public long Id { get; set; }

        public long SizeValueId { get; set; }
        public string SizeValueName { get; set; }
        public string SizeCategoryName { get; set; }
        public long SizeAttributeId { get; set; }
        public int Value { get; set; }
        public DateTime DateModified { get; set; }
        public List<OptionManagerDTO> SizesCategories { get; set; }
    }
}