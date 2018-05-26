using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.BLL.DTO
{
   public class AttributeDTO
    {
       public long GroupId { get; set; }
       public string GroupName { get; set; }

        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Name { get; set; }

        public int SortOrder { get; set; }
        public List<AttributeGroupDTO> Groups { get; set; }

    }
}
