using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class SizeHelper
    {
        
            public long Id { get; set; }

            public long OptionId { get; set; }
            public string ArabicSizeCategoryName { get; set; }
            public string EnglishSizeCategoryName { get; set; }

            public string ArabicName { get; set; }
            public string EngishName { get; set; }
           
            public string Value { get; set; }
        
        
    }
}
