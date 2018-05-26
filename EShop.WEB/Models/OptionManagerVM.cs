using EShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class OptionManagerVM
    {
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public List<OptionValueManagerDTO> OptionValues { get; set; }
        

    }
}