using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class AttributeGroupDTO
    {
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Name { get; set; }

        public int SortOrder { get; set; }
        public List<AttributeDTO> Attributes { get; set; }

    }
}
