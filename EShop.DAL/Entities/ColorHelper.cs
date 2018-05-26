using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ColorHelper
    {
        public long Id { get; set; }
        public long OptionId { get; set; }
        public string ArabicName { get; set; }
        public string EngishName { get; set; }
        public string ImageUrl { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

    }
}
