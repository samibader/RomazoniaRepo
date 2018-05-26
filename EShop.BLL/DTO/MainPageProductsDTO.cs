using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class MainPageProductsDTO
    {
        public List<List<ProductDTO>> Products { get; set; }
        public List<CategoryMenuDTO> MainCategories { get; set; }
        public MainPageProductsDTO()
        {
            Products = new List<List<ProductDTO>>();
            MainCategories = new List<CategoryMenuDTO>();
        }
    }
}
