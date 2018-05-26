using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public class DesignerController :BaseController
    {
        private IDesignerFilterService _designerFilterService;

        public DesignerController(IDesignerFilterService designerFilterService)
        {
            _designerFilterService = designerFilterService;
        }
        //
        // GET: /Designer/
        public ActionResult Browse(String designerName, String[] cat, String[] c, String[] s, String[] t, int MinPrice = 0, int MaxPrice = 1000, int pNumber = 1, int pSize = PAGE_SIZE, String Show = "g", Sorts SortBy = Sorts.NameUpDown)
        {
            ShopGridVM vm = new ShopGridVM();
            List<ProductDTO> productDtos = new List<ProductDTO>();
           
            FilterMenuDTO filters = _designerFilterService.FilterFilters(designerName, cat, c, s, t, MinPrice, MaxPrice, SortBy,
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
            #region Tags Automatic Filter

            List<TagDTO> tagDtos = filters.Tags;//_productFilterService.GetSizeFilters(categoryName, CurrentLanguage);
            List<TagVM> tags = new List<TagVM>();
            foreach (var tag in tagDtos)
            {
                tags.Add(new TagVM()
                {
                    Name = tag.Name,
                    Checked = t != null && t.Contains(tag.Name)
                });
            }

            #endregion
            vm.Categories = categories;
            vm.Colors = colors;
            vm.Sizes = sizes;
            vm.Tags = tags;
            productDtos = filters.Products;//_productFilterService.Filter(categoryName, d, cat, c, s,MinPrice,MaxPrice, SortBy,CurrentCurrency,CurrentLanguage);

            vm.ProductDtos = productDtos.ToPagedList(pNumber, pSize);
            vm.PageSize = pSize;
            vm.PageNumber = pNumber;
            vm.Show = Show;
            vm.SortBy = SortBy;
            vm.MinPrice = (Int32)filters.minPrice;
            vm.MaxPrice = (Int32)filters.maxPrice;

          
            DesignerDTO designer = _designerFilterService.GetDesigner(designerName,CurrentLanguage);

            vm.categoryName = designerName;
            vm.Name = designer.DesignerName;
            vm.Banner = designer.ImageUrl;
            return View(vm);
        }

        public PartialViewResult Slider()
        {
           
            var vm = _designerFilterService.GetAllDesigners(CurrentWebsite);
            return PartialView("_DesignerSlider",vm);
        }
	}
}