using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class ManageOptionValueVM
    {
        public long OptionId { get; set; }
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Value { get; set; }

    }
}