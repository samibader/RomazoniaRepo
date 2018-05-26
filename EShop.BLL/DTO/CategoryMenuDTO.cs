using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class CategoryMenuDTO
    {
        public long Id { get; set; }
        public int SortOrder { get; set; }
        public String Name { get; set; }
        public String EnglishName { set; get; }
        public String ImageUrl { get; set; }
        public String ImageThumb { get; set; }
        public String Banner { get; set; }
        public List<CategoryMenuDTO> SubCategories { get; set; }
    }
}
