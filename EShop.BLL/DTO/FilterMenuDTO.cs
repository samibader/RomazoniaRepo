using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class FilterMenuDTO
    {
        public List<Tuple<CategoryMenuDTO, int>> Categories { get; set; }
        public List<Tuple<DesignerDTO, int>> Designers { get; set; }
        public List<ColorValuesDTO> Colors { get; set; }
        public List<SizeValueDTO> Sizes { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<TagDTO> Tags { get; set; }
    }
}
