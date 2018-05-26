using EShop.BLL.Interfaces;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.Common;
using EShop.DAL.Entities;

namespace EShop.BLL.Services
{
    public class ProductFilterService : IProductFilterService
    {
        IUnitOfWork unitOfWork { get; set; }
        IProductService productService { get; set; }
        private ICategoryService _categoryService { get; set; }
        public ProductFilterService(IUnitOfWork uow, ICategoryService categoryService, IProductService ips)
        {
            unitOfWork = uow;
            productService = ips;
            _categoryService =categoryService;
        }

        private long GetCategoryIdByName(string categoryName, Langs l)
        {
            long languageId = Utils.getLanguage(l);
            CategoryDescription categoryDescription = unitOfWork.CategoryDescriptionRepository.Get(op =>
                                         op.LanguageId == (long)Langs.English).AsEnumerable().Where(op => (op.Name == categoryName || Utils.GenerateSlug(op.Name) == categoryName)).FirstOrDefault();
            return categoryDescription.CategoryId;

        }
        public CategoryMenuDTO GetCategoryFilters(string categoryName, Langs l)
        {
            long categoryId = GetCategoryIdByName(categoryName, l);
            CategoryService categoryService = new CategoryService(unitOfWork);
            return categoryService.GetCategory(categoryId, l);
        }

        public List<ColorValuesDTO> GetColorFilters(List<long> productIds, Langs l)
        {
            List<ColorValuesDTO> colors = new List<ColorValuesDTO>();
            
            IEnumerable<Tuple<string, string>> colorEnglishNamesAndUrls = unitOfWork.OptionValueDescriptionRepository.Get(op =>
                            productIds.Contains(op.ProductId) &&
                            op.OptionValue.Option.IsColor &&
                            op.LanguageId == (long)Langs.English).
                                Select(op =>
                                    new Tuple<string, string>(op.Name, 
                                        op.OptionValue.Value??op.OptionValue.ImageUrl)).Distinct();

            

            for (int i = 0; i < colorEnglishNamesAndUrls.Count(); i++)
            {
                colors.Add(new ColorValuesDTO()
                {
                    ColorName = colorEnglishNamesAndUrls.ElementAt(i).Item1,
                    Image = colorEnglishNamesAndUrls.ElementAt(i).Item2,
                    IsImages = colorEnglishNamesAndUrls.ElementAt(i).Item2[0] != '#'
                });
            }
            return colors;
        }
        public List<SizeValueDTO> GetSizeFilters(List<long> productIds, Langs l)
        {
            IEnumerable<string> sizeEnglishNames = unitOfWork.OptionValueDescriptionRepository.Get(op =>
                            productIds.Contains(op.ProductId) &&
                            op.OptionValue.Option.IsColor == false &&
                            op.LanguageId == (long)Langs.English).
                                Select(op => op.Name.ToLower()).Distinct();
            IEnumerable<string> sizeNames = unitOfWork.OptionValueDescriptionRepository.Get(op =>
                productIds.Contains(op.ProductId) &&
                op.OptionValue.Option.IsColor == false &&
                op.LanguageId == (long)Langs.Arabic).
                    Select(op => op.Name).Distinct();

            List<SizeValueDTO> sizes = new List<SizeValueDTO>();

            for (int i = 0; i < sizeEnglishNames.Count(); i++)
            {
                sizes.Add(new SizeValueDTO()
                {
                    EnglishName = sizeEnglishNames.ElementAt(i),
                    Name = sizeNames.ElementAt(i)
                });
            }
            return sizes;
        }
        public List<DesignerDTO> GetDesignerFilters(List<long> designers, Langs l)
        {
            List<DesignerDTO> designerDTOs = new List<DesignerDTO>();
            foreach (var x in designers)
            {
                designerDTOs.Add(new DesignerDTO()
                {
                    DesignerId = x,
                    DesignerName = unitOfWork.DesignerRepository.GetByID(x).
                    Descriptions.FirstOrDefault(des=>des.LanguageId == (long)l).Text
                });


            }
            return designerDTOs;
        }

        public List<TagDTO> GetTagsFilter(List<long> tagsIds, Langs l)
        {
            List<TagDTO> tags = new List<TagDTO>();
            foreach (var tagId in tagsIds)
            {
                var tag = unitOfWork.TagRepository.Get(t => t.Id == tagId).FirstOrDefault();
                tags.Add(new TagDTO()
                {
                    Name = tag.Text

                });
            }
            return tags;
            //return new List<TagDTO>();
        }
        public FilterMenuDTO FilterFilters(String categoryName, String[] designers, String[] subCategories, String[] colorNames, String[] sizeNames, String[] tags, double lowPrice, double highPrice, Sorts s, Currency currency, Langs l)
        {
            List<ProductDTO> products = Filter(categoryName, designers, subCategories, colorNames, sizeNames, tags, lowPrice, highPrice, s, currency, l);

            //  long baseCategoryId = GetCategoryIdByName(categoryName, l);
            List<long> productIds;
            if(products==null)
                productIds=new List<long>();
            else     
                productIds = products.Select(p => p.Id).ToList();

            FilterMenuDTO filterMenuDTO = new FilterMenuDTO();
            filterMenuDTO.Products = products;
            filterMenuDTO.Categories = new List<Tuple<CategoryMenuDTO, int>>();
            long categoryId;
            List<Category> categoriess;
            if (!String.IsNullOrWhiteSpace(categoryName))
            {
                categoryId = GetCategoryIdByName(categoryName, l);
                categoriess = unitOfWork.CategoryRepository.Get(c => c.ParentId == categoryId).ToList();
            }
            else
            {

                categoriess = unitOfWork.CategoryRepository.Get(c => true).ToList();
                var realCategories = new List<Category>();
                foreach (var category in categoriess)
                {
                    if (!unitOfWork.CategoryRepository.Get(c => c.ParentId == category.Id).Any())
                    {
                        realCategories.Add(category);
                    }

                }
                categoriess = realCategories;
            }
            foreach (var category in categoriess)
            {
                string ctgryName = category.CategoryDescriptions.Where(c => c.LanguageId == (long)Langs.English).Select(c => c.Name).FirstOrDefault();
                var categoryProducts = productService.GetCategoryTreeProducts(ctgryName,Langs.English, currency);
                List<long> Ids;
                if (categoryProducts == null || categoryProducts.Count==0)
                    Ids = new List<long>();
                else
                    Ids = categoryProducts.Select(c => c.Id).ToList();
                CategoryMenuDTO cat = GetCategoryFilters(ctgryName, l);
                int count = Ids.Intersect(productIds).Count();
                if (count != 0)
                    filterMenuDTO.Categories.Add(new Tuple<CategoryMenuDTO, int>(cat, count));
            }
            List<long> designerIds = unitOfWork.ProductRepository.Get(c => productIds.Contains(c.Id)).Select(c => c.DesignerId).Distinct().ToList();
            List<DesignerDTO> designerss = GetDesignerFilters(designerIds, l);
            filterMenuDTO.Designers = new List<Tuple<DesignerDTO, int>>();
            foreach (var des in designerss)
            {
                Designer dsgnrs = unitOfWork.DesignerDescriptionRepository
                    .Get(c => c.Text == des.DesignerName).FirstOrDefault().Designer;
                List<long> Ids = dsgnrs.Products.Select(c => c.Id).ToList();
                int count = Ids.Intersect(productIds).Count();
                if (count != 0)
                    filterMenuDTO.Designers.Add(new Tuple<DesignerDTO, int>(des, count));
            }

            List<long> tagIds = new List<long>();
            var productss = unitOfWork.ProductRepository.Get(c => productIds.Contains(c.Id)).ToList();
            foreach (var product in productss)
            {
                foreach (var sku in product.ProductSKUs)
                {
                    tagIds = tagIds.Union(sku.Tags.Where(t => t.LanguageId == (long)l).Select(t => t.Id)).Distinct().ToList();
                }

            }
            filterMenuDTO.Tags = GetTagsFilter(tagIds, l);

            filterMenuDTO.Colors = GetColorFilters(productIds, l);

            filterMenuDTO.Sizes = GetSizeFilters(productIds, l);

            filterMenuDTO.minPrice = !products.Any() ? 0 : products.Min(c => c.Price);
            filterMenuDTO.maxPrice = !products.Any() ? 0 : products.Max(c => c.Price);

            return filterMenuDTO;

        }


        public List<ProductDTO> Filter(String categoryName, String[] designers, String[] subCategories, String[] colorNames, String[] sizeNames, String[] tags, double lowPrice, double highPrice, Sorts s, Currency currency, Langs l)
        {
            long categoryId;
            List<long> productIds = new List<long>();
            if (!String.IsNullOrEmpty(categoryName))
            {
                categoryId = GetCategoryIdByName(categoryName, Langs.English);
                productIds = unitOfWork.ProductRepository.Get(op => op.Status == true && (
                            (op.CategoryId == categoryId) ||
                            (op.Category.ParentId != null && op.Category.ParentId == categoryId) ||
                            (op.Category.ParentId != null && op.Category.ParentCategory.ParentId != null && op.Category.ParentCategory.ParentId == categoryId) ||
                            (op.Category.ParentId != null && op.Category.ParentCategory.ParentId != null && op.Category.ParentCategory.ParentCategory.ParentId != null && op.Category.ParentCategory.ParentCategory.ParentId == categoryId)
                            )
                            ).Select(c => c.Id).ToList();
            }
            else
            {
                productIds = unitOfWork.ProductRepository.Get(c => c.Status == true).Select(c => c.Id).ToList();
            }
            if (designers != null)
                FilterDesigners(ref productIds, designers,l);
            if (subCategories != null)
                FilterCategories(ref productIds, subCategories);
            if (colorNames != null)
                FilterColors(ref productIds, colorNames);
            if (sizeNames != null)
                FilterSizes(ref productIds, sizeNames);
            if (tags != null)
                FilterTags(ref productIds, tags);
            FilterPrices(ref productIds, lowPrice, highPrice);

            List<ProductDTO> products = new List<ProductDTO>();

            var filteredColor = colorNames!=null ? colorNames[0] : "";

            for (int i = 0; i < productIds.Count; i++)
            {
                products.Add(productService.GetProduct(productIds[i], l, currency,filteredColor));
            }
            sortProducts(ref products, s);
            return products;
        }

        public List<ProductDTO> FilterByName(string prefix, long categoryId, Langs l, Currency currency,WebSites w)
        {
            List<long> productIds = new List<long>();
            if (categoryId > 0)
            {
                productIds = unitOfWork.ProductRepository.Get(op => op.Status == true && ((op.CategoryId == categoryId) ||
                                                                     (op.Category.ParentId != null &&
                                                                      op.Category.ParentId == categoryId) ||
                                                                     (op.Category.ParentId != null &&
                                                                      op.Category.ParentCategory.ParentId != null &&
                                                                      op.Category.ParentCategory.ParentId == categoryId)))
                                                                      .AsEnumerable().Where(p=>_categoryService.getWebsite(p.CategoryId)==w)
                    .Select(c => c.Id)
                    .ToList();
                ;
            }
            List<long> productNameIds =
                unitOfWork.ProductDescriptionRepository.Get(
                    pd => pd.Name.ToLower().Contains(prefix.ToLower()) && pd.LanguageId == (long)l)
                    .AsEnumerable().Where(p=>_categoryService.getWebsite(p.Product.CategoryId)==w)
                    .Select(pd => pd.ProductId).ToList();
            if (categoryId > 0)
            {
                productIds = productIds.Intersect(productNameIds).ToList();
            }
            else
            {
                productIds = productNameIds;
            }
            List<ProductDTO> products = new List<ProductDTO>();

            for (int i = 0; i < productIds.Count; i++)
            {
                ProductDTO dto = productService.GetProduct(productIds[i], l, currency);
                if(dto!=null)
                    products.Add(dto);
            }

            return products;

        }
        private void FilterPrices(ref List<long> productIds, double lowPrice, double highPrice)
        {
            List<long> productPricesIds = unitOfWork.ProductRepository.Get(op => op.Status == true &&
                                                op.Price >= lowPrice && op.Price <= highPrice)
                                                        .Select(op => op.Id).ToList();
            productIds = productIds.Intersect(productPricesIds).ToList();
        }
        private void FilterDesigners(ref List<long> productIds, String[] designers,Langs l)
        {
            List<long> productDesignerIds = new List<long>();
            for (int i = 0; i < designers.Count(); i++)
            {
                String d = designers[i];
                long desId = unitOfWork.DesignerDescriptionRepository
                    .Get(cc => cc.Text == d).Select(c => c.DesignerId).FirstOrDefault();
                productDesignerIds = productDesignerIds.Union(unitOfWork.ProductRepository.
                                        Get(c => c.Status == true &&
                                            c.DesignerId == desId).
                                            Select(op => op.Id).ToList()).Distinct().ToList();
            }
            productIds = productIds.Intersect(productDesignerIds).ToList();
        }
        private void FilterCategories(ref List<long> productIds, String[] subCategories)
        {
            List<long> productCategoryIds = new List<long>();
            for (int i = 0; i < subCategories.Count(); i++)
            {
                String s = subCategories[i];
                productCategoryIds = productCategoryIds.Union(unitOfWork.ProductRepository.
                                         Get(c => c.Status == true).AsEnumerable().Where(c=>
                                             (
                                                 (Utils.GenerateSlug(c.Category.CategoryDescriptions.
                                                     Where(op => op.LanguageId == (long)Langs.English).FirstOrDefault().Name) == s) ||
                                                 (c.Category.ParentCategory != null && Utils.GenerateSlug(c.Category.ParentCategory.CategoryDescriptions.
                                                     Where(op => op.LanguageId == (long)Langs.English).FirstOrDefault().Name) == s) ||
                                                 (c.Category.ParentCategory.ParentCategory != null && Utils.GenerateSlug(c.Category.ParentCategory.ParentCategory.CategoryDescriptions.
                                                     Where(op => op.LanguageId == (long)Langs.English).FirstOrDefault().Name) == s)
                                             )
                                       ).Select(op => op.Id).ToList()).Distinct().ToList();
            }
            productIds = productIds.Intersect(productCategoryIds).ToList();
        }
        private void FilterColors(ref List<long> productIds, String[] colorNames)
        {
            List<long> productColorIds = new List<long>();
            for (int i = 0; i < colorNames.Count(); i++)
            {
                String cc = colorNames[i];
                productColorIds = productColorIds.Union(unitOfWork.OptionValueRepository.
                                        Get(c => c.Product.Status == true && c.OptionValueDescriptions.
                                                Where(op => op.LanguageId == (long)Langs.English).FirstOrDefault().Name == cc).
                                            Select(op => op.ProductId).ToList()).Distinct().ToList();
            }
            productIds = productIds.Intersect(productColorIds).ToList();
        }
        private void FilterSizes(ref List<long> productIds, String[] sizeNames)
        {
            List<long> productSizeIds = new List<long>();
            for (int i = 0; i < sizeNames.Count(); i++)
            {
                String s = sizeNames[i];
                productSizeIds = productSizeIds.Union(unitOfWork.OptionValueRepository.
                                        Get(c => c.Product.Status == true && c.OptionValueDescriptions.
                                                Where(op => op.LanguageId == (long)Langs.English).FirstOrDefault().Name == s).
                                            Select(op => op.ProductId).ToList()).Distinct().ToList();
            }
            productIds = productIds.Intersect(productSizeIds).ToList();
        }
        private void sortProducts(ref List<ProductDTO> products, Sorts s)
        {
            switch (s)
            {
                case Sorts.NameDownUp: products.Sort((a, b) => a.Name.CompareTo(b.Name));   //products.OrderBy(op => op.Name);
                    break;
                case Sorts.NameUpDown: products.Sort((a, b) => -1 * a.Name.CompareTo(b.Name));
                    break;
                case Sorts.PriceDownUp: products.Sort((a, b) => a.Price.CompareTo(b.Price)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.PriceUpDown: products.Sort((a, b) => -1 * a.Price.CompareTo(b.Price));
                    break;
                case Sorts.DateAddedUp: products.Sort((a, b) => a.DateAdded.CompareTo(b.DateAdded));
                    break;
                case Sorts.DateAddedDown: products.Sort((a, b) => -1 * a.DateAdded.CompareTo(b.DateAdded));
                    break;
            }

        }

        private void FilterTags(ref List<long> productIds, String[] tags)
        {
            List<long> productTagsIds = new List<long>();
            for (var i = 0; i < tags.Length; i++)
            {
                String tag = tags[i];
                var tagDescription =
                    unitOfWork.TagRepository.Get(
                        td => td.Text == tag).FirstOrDefault();
                var ids = tagDescription.ProductSKUs.Where(tt => tt.Product.Status).Select(tt => tt.ProductId).Distinct();
                productTagsIds = productTagsIds.Union(ids).Distinct().ToList();
            }
            productIds = productIds.Intersect(productTagsIds).ToList();

        }
    }
}
