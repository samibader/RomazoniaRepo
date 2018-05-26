using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.BLL.DTO;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.WEB.Models;
using PagedList;

namespace EShop.WEB.Controllers
{
    public class TagController:BaseController
    {
        private ITagFilterService _tagFilterService;

        public TagController(ITagFilterService tagFilterService)
        {
            _tagFilterService = tagFilterService;
        }
        public ActionResult Browse(String tagName, String[] cat, String[] c, String[] s, String[] d, int MinPrice = 0, int MaxPrice = 1000, int pNumber = 1, int pSize = PAGE_SIZE, String Show = "g", Sorts SortBy = Sorts.NameUpDown)
        {
            ShopGridVM vm = new ShopGridVM();
            List<ProductDTO> productDtos = new List<ProductDTO>();

            FilterMenuDTO filters = _tagFilterService.FilterFilters(tagName,d, cat, c, s, MinPrice, MaxPrice, SortBy,
                CurrentCurrency, CurrentLanguage);
            #region Category Automatic Filter

            List<Tuple<CategoryMenuDTO, int>> categoryDTO = filters.Categories; // _productFilterService.GetCategoryFilters(categoryName, CurrentLanguage);
            List<CategoryFilterVM> categories = new List<CategoryFilterVM>();
            foreach (var categoryfilter in categoryDTO)
            {
                categories.Add(new CategoryFilterVM()
                {
                    Name = categoryfilter.Item1.Name,
                    EnglishName = categoryfilter.Item1.EnglishName,
                    Checked = cat != null && cat.Contains(categoryfilter.Item1.EnglishName),
                    Count = categoryfilter.Item2

                });
            }

            #endregion
            #region Size Automatic Filter

            List<SizeValueDTO> sizeDtos = filters.Sizes;//_productFilterService.GetSizeFilters(categoryName, CurrentLanguage);
            List<SizeFilterVM> sizes = new List<SizeFilterVM>();
            foreach (var sizeValueDto in sizeDtos)
            {
                sizes.Add(new SizeFilterVM()
                {
                    Name = sizeValueDto.Name,
                    EnglishName = sizeValueDto.EnglishName,
                    Checked = s != null && s.Contains(sizeValueDto.EnglishName)
                });
            }

            #endregion
            #region color Automatic Filter

            List<ColorValuesDTO> colorDtos = filters.Colors;// _productFilterService.GetColorFilters(categoryName, CurrentLanguage);
            List<ColorFilterVM> colors = new List<ColorFilterVM>();
            foreach (var colorValueDto in colorDtos)
            {
                colors.Add(new ColorFilterVM()
                {
                    Name = colorValueDto.ColorName,
                    EnglishName = colorValueDto.ColorName,
                    Checked = c != null && c.Contains(colorValueDto.ColorName),
                    Image = colorValueDto.Image,
                    IsImage = colorValueDto.IsImages
                });
            }

            #endregion
            #region Deisner Automatic Filter

            List<Tuple<DesignerDTO, int>> designerDtos = filters.Designers;//_productFilterService.GetDesignerFilters(categoryName, CurrentLanguage);
            List<DesignerFilterVM> designers = new List<DesignerFilterVM>();
            foreach (var designerDto in designerDtos)
            {
                designers.Add(new DesignerFilterVM()
                {
                    Name = designerDto.Item1.DesignerName,
                    Id = designerDto.Item1.DesignerId,
                    Checked = d != null && d.Contains(designerDto.Item1.DesignerName),
                    Count = designerDto.Item2
                });

            }

            #endregion
            vm.Categories = categories;
            vm.Colors = colors;
            vm.Sizes = sizes;
            vm.Designers = designers;
            productDtos = filters.Products;//_productFilterService.Filter(categoryName, d, cat, c, s,MinPrice,MaxPrice, SortBy,CurrentCurrency,CurrentLanguage);

            vm.ProductDtos = productDtos.ToPagedList(pNumber, pSize);
            vm.PageSize = pSize;
            vm.PageNumber = pNumber;
            vm.Show = Show;
            vm.SortBy = SortBy;
            vm.MinPrice = (Int32)filters.minPrice;
            vm.MaxPrice = (Int32)filters.maxPrice;

           // DesignerDTO designer = _designerFilterService.GetDesigner(designerName);
            var tag = _tagFilterService.GetTag(tagName,CurrentLanguage);
            vm.categoryName = tagName;
            vm.Name = tag.Name;
            //vm.Banner = designer.ImageUrl;
            return View(vm);
        }

    }
}