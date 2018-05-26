using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class OptionHelper
    {
        public virtual ICollection<SizeAttribute> SizeAttributes { get; set; }
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

    }
}
