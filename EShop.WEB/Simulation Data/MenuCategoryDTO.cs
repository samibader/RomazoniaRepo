using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EShop.WEB.Simulation_Data
{
    public class MenuCategoryDTO
    {
        public List<MenuCategoryDTO>  SubCategories { get; set; }
        
        public Int64 Id { get; set; }
        public String ArabicName { get; set; }
        public String EnglishName { get; set; }
        public String Image { get; set; }  
        public String ImageThumb { get; set; }  
        public bool  HasChildren { get; set; }

        public MenuCategoryDTO()
        {
            Id = 1;
            ArabicName = "فئة";
            EnglishName = "Home";
            Image = "/Assets/images/menu-img2.jpg";
            ImageThumb = "/Assets/images/menu-img2.jpg";
            HasChildren = true;
        }

        public MenuCategoryDTO Create(int depth)
        {
            if (depth <= 0)
                return new MenuCategoryDTO();
            
            
                 SubCategories = new List<MenuCategoryDTO>(3);
            for (int i = 0; i < depth; i++)
            {

               
               MenuCategoryDTO menuCategoryDto = new MenuCategoryDTO();
                menuCategoryDto.Create(depth - 1);
                SubCategories.Add(menuCategoryDto);
            }

            return this;
        }
    }

}