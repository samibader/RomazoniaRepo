using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class OptionManagerDTO
    {
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public List<OptionValueManagerDTO> OptionValues { get; set; }
      

    }
}
