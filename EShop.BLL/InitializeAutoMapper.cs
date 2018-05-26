using AutoMapper;
using EShop.BLL.DTO;
using EShop.Common;
using EShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL
{
    public static class InitializeAutoMapper
    {
        public static void Initialize()
        {
            CreateModelsToViewModels();
            //CreateViewModelsToModels();
        }

        private static void CreateModelsToViewModels()
        {
            //Mapper.CreateMap<Category, CategoryMenuDTO>().ForMember(dest => dest.EnglishName, opt =>
            //        opt.MapFrom(src => src.CategoryDescriptions.Where(dsc => dsc.LanguageId == (long)Langs.English).FirstOrDefault().Name)
            //    )
            //    .ForMember(dest => dest.Name, opt =>
            //        //opt.MapFrom(src => src.CategoryDescriptions.Where(dsc => dsc.LanguageId == languageId).FirstOrDefault().Name)
            //        opt.Ignore()
            //    )
            //    .ForMember(dest => dest.ImageUrl, opt =>
            //        opt.MapFrom(src => src.ImageUrl ?? DefaultImages.Category)
            //    ).ForMember(dest => dest.Banner, opt =>
            //        opt.MapFrom(src => src.Banner ?? DefaultImages.Banner)
            //    );
        }

    }
}
