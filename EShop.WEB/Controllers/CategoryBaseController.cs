using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using EShop.BLL.Interfaces;
using EShop.WEB.Models;
using EShop.BLL.DTO;
using EShop.Common;
using EShop.Resources.Views;
using PagedList;

namespace EShop.WEB.Controllers
{
    public class CategoryBaseController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private IProductFilterService _productFilterService;
        public CategoryBaseController(IProductService productService, IProductFilterService productFilterService, ICategoryService categoryService)
        {
            _productService = productService;
            _productFilterService = productFilterService;
            _categoryService = categoryService;

        }
        //
        // GET: /CategoryBase/
        public ActionResult Index()
        {
            return View("Blank");
        }
        /*
         * d => Designer, cat => Sub Categories, c => Colors,s => sizes,pNumber =>PageNumber .. 
         */ 
        public ActionResult Browse(String categoryName, String[] d, String[] cat, String[] c, String[] s,String [] t,int MinPrice=0,int MaxPrice=1000, int pNumber = 1, int pSize = PAGE_SIZE, String Show = "g",Sorts SortBy = Sorts.NameUpDown)
        {
           
            ShopGridVM vm = new ShopGridVM();
            List<ProductDTO> productDtos = new List<ProductDTO>();
           // for (int i = 1; i < 25; i++)
          //  {
               // productDtos.Add(new ProductDTO(i * 100, "Small", -1));
               // productDtos.Add(new ProductDTO(i * 101, "أحمر"));
              //  productDtos.Add(new ProductDTO(i * 102));
              //  productDtos.Add(new ProductDTO(i * 103, "أحمر", "Small"));
                //if (i%2 == 0)
                //{
                //    productDtos.Add(new ProductDTO(i, "أحمر"));
                //}
                //else if(i%3 ==0)
                //{
                //    productDtos.Add(new ProductDTO(i));

                //}
                //else if (i%5 == 0)
                //{
                //    productDtos.Add(new ProductDTO(i, "أحمر","Small"));

                //}
                //else if (i%7 == 0)
                //{
                //    productDtos.Add(new ProductDTO(i, "Small",-1));

                //}

            //}
            FilterMenuDTO filters= _productFilterService.FilterFilters(categoryName, d, cat, c, s,t, MinPrice, MaxPrice, SortBy,
                CurrentCurrency, CurrentLanguage);
            #region Manual Filters 

            //List<CategoryFilterVM> categories = new List<CategoryFilterVM>()
            //{
            //    new CategoryFilterVM()
            //    {
            //        Name = "بنطال",
            //        Checked = false
            //    },
            //                    new CategoryFilterVM()
            //    {
            //        Name = "بلوزة",
            //        Checked = false
            //    }
            //};
            //List<ColorFilterVM> colors = new List<ColorFilterVM>()
            //{
            //    new ColorFilterVM()
            //    {
            //        Name = "أحمر",
            //        Checked = false,
            //        Image = "#00ff00",
            //        IsImage = false

            //    },
            //                    new ColorFilterVM()
            //    {
            //        Name = "أخضر",
            //        Checked = false,
            //        Image = "#0000ff",
            //        IsImage = false

            //    }
            //};
            //List<SizeFilterVM> sizes = new List<SizeFilterVM>()
            //{
            //    new SizeFilterVM()
            //    {
            //        Name = "صغير",
            //        Checked = false
            //    },
            //                    new SizeFilterVM()
            //    {
            //        Name = "XL",
            //        Checked = false
            //    }
            //};
            //List<PriceFilter> prices = new List<PriceFilter>()
            //{
            //    new PriceFilter()
            //    {
            //        Price = new Tuple<int, int>(20,50),
            //        Checked = false
            //    },
            //                    new PriceFilter()
            //    {
            //        Price = new Tuple<int, int>(50,100),
            //        Checked = false
            //    }
            //};
            //List<DesignerFilterVM> designers = new List<DesignerFilterVM>()
            //{
            //    new DesignerFilterVM()
            //    {
            //        Checked = false,
            //        Id = 1,
            //        Name = "هاني"
            //    },
            //    new DesignerFilterVM()
            //    {
            //        Checked = false,
            //        Id = 2,
            //        Name = "عصام"
            //    }

            //};
            //if (d != null)
            //    foreach (String item in d)
            //    {
            //        DesignerFilterVM dd = designers.Where(ddd => ddd.Name == item).FirstOrDefault();
            //        if (dd != null)
            //            dd.Checked = true;
            //    }
            //if (cat != null)

            //    foreach (String item in cat)
            //    {
            //        CategoryFilterVM dd = categories.Where(ddd => ddd.Name == item).FirstOrDefault();
            //        if (dd != null)
            //            dd.Checked = true;
            //    }
            //if (c != null)

            //    foreach (String item in c)
            //    {
            //        ColorFilterVM dd = colors.Where(ddd => ddd.Name == item).FirstOrDefault();
            //        if (dd != null)
            //            dd.Checked = true;
            //    }
            //if (s != null)

            //    foreach (String item in s)
            //    {
            //        SizeFilterVM dd = sizes.Where(ddd => ddd.Name == item).FirstOrDefault();
            //        if (dd != null)
            //            dd.Checked = true;
            //    }


            #endregion
            #region Category Automatic Filter

            List<Tuple<CategoryMenuDTO,int>> categoryDTO = filters.Categories; // _productFilterService.GetCategoryFilters(categoryName, CurrentLanguage);
            List<CategoryFilterVM> categories = new List<CategoryFilterVM>();
            foreach (var categoryfilter in categoryDTO)
            {
                categories.Add(new CategoryFilterVM()
                {
                    Name = categoryfilter.Item1.Name,
                    EnglishName = Utils.GenerateSlug(categoryfilter.Item1.EnglishName),
                    Checked = cat != null && cat.Contains(Utils.GenerateSlug(categoryfilter.Item1.EnglishName)),
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

            List<Tuple<DesignerDTO,int>> designerDtos = filters.Designers;//_productFilterService.GetDesignerFilters(categoryName, CurrentLanguage);
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
            vm.Designers = designers;
            vm.Sizes = sizes;
            vm.Tags = tags;
           // vm.ProductDtos = productDtos.ToPagedList(pNumber, pSize);
          //  vm.ProductDtos = _productService.GetCategoryTreeProducts(categoryName, CurrentLanguage).ToPagedList(pNumber, pSize);
            productDtos = filters.Products;//_productFilterService.Filter(categoryName, d, cat, c, s,MinPrice,MaxPrice, SortBy,CurrentCurrency,CurrentLanguage);
          //  productDtos.AddRange(productDtos);
           // productDtos.AddRange(productDtos);
         //   productDtos.AddRange(productDtos);

            vm.ProductDtos = productDtos.ToPagedList(pNumber, pSize); 
            vm.PageSize = pSize;
            vm.PageNumber = pNumber;
            vm.Show = Show;
            vm.SortBy = SortBy;
            vm.MinPrice = (Int32)filters.minPrice;
            vm.MaxPrice = (Int32)filters.maxPrice;
            long categoryId = _productService.GetCategoryIdByName(categoryName, CurrentLanguage);
            CategoryMenuDTO category = _categoryService.GetCategory(categoryId, CurrentLanguage);

            vm.categoryName = categoryName;
            vm.Name = category.Name;
            vm.Banner = category.Banner;
            ViewBag.Title = "استعراض المنتجات - " + category.Name;
            return View(vm);
        }


        //[HttpPost]
        //public PartialViewResult BrowsePost(String categoryName, String[] d, String[] cat, String[] c, String[] s,Sorts SortBy,int MinPrice=0,int MaxPrice=1000, int pNumber = 1, int pSize = PAGE_SIZE, String Show = "g")
        //{
            
        //    ShopGridVM vm = new ShopGridVM();
        //    List<ProductDTO> productDtos = new List<ProductDTO>();
        //    // for (int i = 1; i < 25; i++)
        //    //  {
        //    // productDtos.Add(new ProductDTO(i * 100, "Small", -1));
        //    // productDtos.Add(new ProductDTO(i * 101, "أحمر"));
        //    //  productDtos.Add(new ProductDTO(i * 102));
        //    //  productDtos.Add(new ProductDTO(i * 103, "أحمر", "Small"));
        //    //if (i%2 == 0)
        //    //{
        //    //    productDtos.Add(new ProductDTO(i, "أحمر"));
        //    //}
        //    //else if(i%3 ==0)
        //    //{
        //    //    productDtos.Add(new ProductDTO(i));

        //    //}
        //    //else if (i%5 == 0)
        //    //{
        //    //    productDtos.Add(new ProductDTO(i, "أحمر","Small"));

        //    //}
        //    //else if (i%7 == 0)
        //    //{
        //    //    productDtos.Add(new ProductDTO(i, "Small",-1));

        //    //}

        //    //}

        //    #region Manual Filters

        //    //List<CategoryFilterVM> categories = new List<CategoryFilterVM>()
        //    //{
        //    //    new CategoryFilterVM()
        //    //    {
        //    //        Name = "بنطال",
        //    //        Checked = false
        //    //    },
        //    //                    new CategoryFilterVM()
        //    //    {
        //    //        Name = "بلوزة",
        //    //        Checked = false
        //    //    }
        //    //};
        //    //List<ColorFilterVM> colors = new List<ColorFilterVM>()
        //    //{
        //    //    new ColorFilterVM()
        //    //    {
        //    //        Name = "أحمر",
        //    //        Checked = false,
        //    //        Image = "#00ff00",
        //    //        IsImage = false

        //    //    },
        //    //                    new ColorFilterVM()
        //    //    {
        //    //        Name = "أخضر",
        //    //        Checked = false,
        //    //        Image = "#0000ff",
        //    //        IsImage = false

        //    //    }
        //    //};
        //    //List<SizeFilterVM> sizes = new List<SizeFilterVM>()
        //    //{
        //    //    new SizeFilterVM()
        //    //    {
        //    //        Name = "صغير",
        //    //        Checked = false
        //    //    },
        //    //                    new SizeFilterVM()
        //    //    {
        //    //        Name = "XL",
        //    //        Checked = false
        //    //    }
        //    //};
        //    //List<PriceFilter> prices = new List<PriceFilter>()
        //    //{
        //    //    new PriceFilter()
        //    //    {
        //    //        Price = new Tuple<int, int>(20,50),
        //    //        Checked = false
        //    //    },
        //    //                    new PriceFilter()
        //    //    {
        //    //        Price = new Tuple<int, int>(50,100),
        //    //        Checked = false
        //    //    }
        //    //};
        //    //List<DesignerFilterVM> designers = new List<DesignerFilterVM>()
        //    //{
        //    //    new DesignerFilterVM()
        //    //    {
        //    //        Checked = false,
        //    //        Id = 1,
        //    //        Name = "هاني"
        //    //    },
        //    //    new DesignerFilterVM()
        //    //    {
        //    //        Checked = false,
        //    //        Id = 2,
        //    //        Name = "عصام"
        //    //    }

        //    //};
        //    //if (d != null)
        //    //    foreach (String item in d)
        //    //    {
        //    //        DesignerFilterVM dd = designers.Where(ddd => ddd.Name == item).FirstOrDefault();
        //    //        if (dd != null)
        //    //            dd.Checked = true;
        //    //    }
        //    //if (cat != null)

        //    //    foreach (String item in cat)
        //    //    {
        //    //        CategoryFilterVM dd = categories.Where(ddd => ddd.Name == item).FirstOrDefault();
        //    //        if (dd != null)
        //    //            dd.Checked = true;
        //    //    }
        //    //if (c != null)

        //    //    foreach (String item in c)
        //    //    {
        //    //        ColorFilterVM dd = colors.Where(ddd => ddd.Name == item).FirstOrDefault();
        //    //        if (dd != null)
        //    //            dd.Checked = true;
        //    //    }
        //    //if (s != null)

        //    //    foreach (String item in s)
        //    //    {
        //    //        SizeFilterVM dd = sizes.Where(ddd => ddd.Name == item).FirstOrDefault();
        //    //        if (dd != null)
        //    //            dd.Checked = true;
        //    //    }


        //    #endregion

        //    #region Category Automatic Filter
        //    CategoryMenuDTO categoryDTO = _productFilterService.GetCategoryFilters(categoryName, CurrentLanguage);
        //    List<CategoryFilterVM> categories = new List<CategoryFilterVM>();
        //    foreach (var subcategory in categoryDTO.SubCategories)
        //    {
        //        categories.Add(new CategoryFilterVM()
        //        {
        //            Name = subcategory.Name,
        //            EnglishName = subcategory.EnglishName,
        //            Checked = cat != null && cat.Contains(subcategory.EnglishName)
        //        });
        //    }

        //    #endregion
        //    #region Size Automatic Filter

        //    List<SizeValueDTO> sizeDtos = _productFilterService.GetSizeFilters(categoryName, CurrentLanguage);
        //    List<SizeFilterVM> sizes = new List<SizeFilterVM>();
        //    foreach (var sizeValueDto in sizeDtos)
        //    {
        //        sizes.Add(new SizeFilterVM()
        //        {
        //            Name = sizeValueDto.Name,
        //            EnglishName = sizeValueDto.EnglishName,
        //            Checked = s != null && s.Contains(sizeValueDto.EnglishName)
        //        });
        //    }

        //    #endregion
        //    #region color Automatic Filter

        //    List<ColorValuesDTO> colorDtos = _productFilterService.GetColorFilters(categoryName, CurrentLanguage);
        //    List<ColorFilterVM> colors = new List<ColorFilterVM>();
        //    foreach (var colorValueDto in colorDtos)
        //    {
        //        colors.Add(new ColorFilterVM()
        //        {
        //            Name = colorValueDto.ColorName,
        //            EnglishName = colorValueDto.ColorName,
        //            Checked = c != null && c.Contains(colorValueDto.ColorName),
        //            Image = colorValueDto.Image,
        //            IsImage = colorValueDto.IsImages
        //        });
        //    }

        //    #endregion
        //    #region Deisner Automatic Filter

        //    List<DesignerDTO> designerDtos = _productFilterService.GetDesignerFilters(categoryName, CurrentLanguage);
        //    List<DesignerFilterVM> designers = new List<DesignerFilterVM>();
        //    foreach (var designerDto in designerDtos)
        //    {
        //        designers.Add(new DesignerFilterVM()
        //        {
        //            Name = designerDto.DesignerName,
        //            Id = designerDto.DesignerId,
        //            Checked = d != null && d.Contains(designerDto.DesignerName)
        //        });
        //    }

        //    #endregion

        //    vm.Categories = categories;
        //    vm.Colors = colors;
        //    vm.Designers = designers;
        //    vm.Sizes = sizes;
        //    // vm.ProductDtos = productDtos.ToPagedList(pNumber, pSize);
        //    //  vm.ProductDtos = _productService.GetCategoryTreeProducts(categoryName, CurrentLanguage).ToPagedList(pNumber, pSize);
        //    productDtos = _productFilterService.Filter(categoryName, d, cat, c, s,MinPrice,MaxPrice, SortBy,CurrentCurrency,CurrentLanguage);
        // //   productDtos.AddRange(productDtos);
        //  //  productDtos.AddRange(productDtos);
        //  //  productDtos.AddRange(productDtos);
        //    vm.ProductDtos = productDtos.ToPagedList(pNumber, pSize);
        //    vm.PageSize = pSize;
        //    vm.PageNumber = pNumber;
        //    vm.Show = Show;
        //    vm.SortBy = SortBy;
        //    vm.MinPrice = MinPrice;
        //    vm.MaxPrice = MaxPrice;
        //    vm.categoryName = categoryName;
        //     return PartialView("Browse", vm);
        //}

        public PartialViewResult CategoryPath(String categoryName)
        {
            CategoryPathDTO Fullpath =  _categoryService.GetCategoryPath(categoryName, CurrentLanguage);
            CategoryPathVM vm = new CategoryPathVM();
            vm.CategoryNames = new List<Tuple<string, string>>();
            foreach (var path in Fullpath.Path)
            {
                vm.CategoryNames.Add(path.Item2);
            }
            return PartialView("_CategoryPathViewModel",vm);
        }
        public PartialViewResult ProductCategoryPath(long skuId)
        {
            Tuple<string,CategoryPathDTO> Fullpath = _categoryService.GetProductPath(skuId, CurrentLanguage);
            CategoryPathVM vm = new CategoryPathVM();
            vm.CategoryNames = new List<Tuple<string, string>>();
            foreach (var path in Fullpath.Item2.Path)
            {
                vm.CategoryNames.Add(path.Item2);
            }
            //productName
            vm.CategoryNames.Add(new Tuple<string, string>(Fullpath.Item1,Fullpath.Item1));
            return PartialView("_CategoryPathViewModel",vm);
        }
    }
}