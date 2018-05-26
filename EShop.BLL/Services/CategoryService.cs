using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{

    public class CategoryService : ICategoryService
    {
        IUnitOfWork unitOfWork { get; set; }

        public CategoryService(IUnitOfWork uow)
        {

            unitOfWork = uow;
        }



        public WebSites getWebsite(long categoryId)
        {
            long? parentID = unitOfWork.CategoryRepository.GetByID(categoryId).ParentId;
            if (parentID == null)
                return (WebSites)categoryId;
            else
                return getWebsite(parentID.Value);
        }
        public CategoryMenuDTO GetLatestCategory(Langs l, WebSites w)
        {
            var category = unitOfWork.ProductRepository.Get(c => c.Status && c.Id > 3)
                .AsEnumerable().Where(c => (long) getWebsite(c.CategoryId) == (long) w)
                .OrderByDescending(c => c.DateAdded).FirstOrDefault();
            if (category == null)
            {
                return null;
            }
            return GetCategory(category.CategoryId, l);
        }
        public List<CategoryManegerDTO> GetAllManegerCategories(Sorts sb, WebSites w)
        {
            return GetAllManegerCategories(sb).AsEnumerable().Where(c => (long)getWebsite(c.Id) == (long)w).ToList();
        }
        public List<CategoryManegerDTO> GetAllManegerCategories(Sorts sb)
        {
            List<CategoryManegerDTO> categories = new List<CategoryManegerDTO>();
            categories.AddRange(getSubManagerCategories((long)WebSites.Fashion));
            categories.AddRange(getSubManagerCategories((long)WebSites.Mobile));
            categories.AddRange(getSubManagerCategories((long)WebSites.Gym));
            sortCategories(ref categories, sb);
            return categories;
        }
        public CategoryManegerDTO GetCategoryManagerById(long categoryId)
        {
            CategoryManegerDTO categoryManegerDTO = new CategoryManegerDTO();
            Category category = unitOfWork.CategoryRepository.GetByID(categoryId);
            CategoryDescription arabicCategoryDescription = category.CategoryDescriptions.Where(c => c.LanguageId == Utils.getLanguage(Langs.Arabic)).FirstOrDefault();
            categoryManegerDTO.ArabicName = arabicCategoryDescription.Name;
            categoryManegerDTO.ArabicDescription = arabicCategoryDescription.Description;
            categoryManegerDTO.ParentId = category.ParentId == null ? 0 : category.ParentId.Value;
            categoryManegerDTO.ArabicMetaDescription = arabicCategoryDescription.MetaDescription;

            CategoryDescription englishCategoryDescription = category.CategoryDescriptions.Where(c => c.LanguageId == Utils.getLanguage(Langs.English)).FirstOrDefault();
            categoryManegerDTO.EnglishName = englishCategoryDescription.Name;
            categoryManegerDTO.EnglishMetaDescription = englishCategoryDescription.MetaDescription;
            categoryManegerDTO.EnglishDescription = englishCategoryDescription.Description;
            categoryManegerDTO.Id = categoryId;
            categoryManegerDTO.ImageUrl = category.ImageUrl =="" ? DefaultImages.Category : category.ImageUrl ;
            categoryManegerDTO.ImageThumb = category.ImageThumb == "" ? DefaultImages.Category : category.ImageThumb;
            categoryManegerDTO.BannerImage = category.Banner == "" ? DefaultImages.Banner : category.Banner;
            categoryManegerDTO.SortOrder = category.SortOrder;
            categoryManegerDTO.Status = category.Status;
            categoryManegerDTO.DateCreation = category.DateAdded == null ? DateTime.Now : category.DateAdded.Value;
            categoryManegerDTO.DateCreation = category.DateModified == null ? DateTime.Now : category.DateModified.Value;

            categoryManegerDTO.ArabicPath = GetCategoryPath(categoryManegerDTO.EnglishName, Langs.Arabic).PathStr;
            categoryManegerDTO.EnglishPath = GetCategoryPath(categoryManegerDTO.EnglishName, Langs.English).PathStr;
            return categoryManegerDTO;
        }
        public OperationDetails EditCategoryManager(CategoryManegerDTO categoryManegerDTO)
        {
            Category category = unitOfWork.CategoryRepository.GetByID(categoryManegerDTO.Id);
            if (category == null)
                return new OperationDetails(false, "حدث خطأ أثناء عملية التعديل", "");

            CategoryDescription arabicCategoryDescription = category.CategoryDescriptions.Where(c => c.LanguageId == Utils.getLanguage(Langs.Arabic)).FirstOrDefault();
            arabicCategoryDescription.Name = categoryManegerDTO.ArabicName;
            arabicCategoryDescription.MetaDescription = categoryManegerDTO.ArabicMetaDescription;
            arabicCategoryDescription.Description = categoryManegerDTO.ArabicDescription;
            arabicCategoryDescription.DateModefied = DateTime.Now;

            CategoryDescription englishCategoryDescription = category.CategoryDescriptions.Where(c => c.LanguageId == Utils.getLanguage(Langs.English)).FirstOrDefault();
            englishCategoryDescription.Name = categoryManegerDTO.EnglishName;
            englishCategoryDescription.MetaDescription = categoryManegerDTO.EnglishMetaDescription;
            englishCategoryDescription.Description = categoryManegerDTO.EnglishDescription;
            englishCategoryDescription.DateModefied = DateTime.Now;

            unitOfWork.CategoryDescriptionRepository.Update(englishCategoryDescription);
            unitOfWork.CategoryDescriptionRepository.Update(arabicCategoryDescription);

            category.ImageUrl = categoryManegerDTO.ImageUrl == "" ? DefaultImages.Category : categoryManegerDTO.ImageUrl;
            category.ImageThumb = categoryManegerDTO.ImageThumb == "" ? DefaultImages.Category : categoryManegerDTO.ImageThumb;
            category.Banner = categoryManegerDTO.BannerImage == "" ? DefaultImages.Banner : categoryManegerDTO.BannerImage;
            category.SortOrder = categoryManegerDTO.SortOrder;
            category.Status = categoryManegerDTO.Status;
            category.DateModified = DateTime.Now;
            category.ParentId = categoryManegerDTO.ParentId;

            unitOfWork.CategoryRepository.Update(category);
            unitOfWork.Save();

            return new OperationDetails(true, "تم التعديل بنجاح", "");
        }
        public OperationDetails DeleteCategory(long categoryId)
        {
            Category category = unitOfWork.CategoryRepository.GetByID(categoryId);

            if (unitOfWork.CategoryRepository.Get(c => c.ParentId == categoryId).Count() > 0)
                return new OperationDetails(false, "هذه الفئة لها فئات أبناء", "");
            if (unitOfWork.ProductRepository.Get(c => c.CategoryId == categoryId).Count() > 0)
                return new OperationDetails(false, "هذه الفئة تحتوي على منتجات", "");
            List<CategoryDescription> descs = unitOfWork.CategoryDescriptionRepository.Get(c => c.CategoryId == categoryId).ToList();
            foreach (var desc in descs)
            {
                unitOfWork.CategoryDescriptionRepository.Delete(desc);
            }
            unitOfWork.CategoryRepository.Delete(category);
            unitOfWork.Save();
            return new OperationDetails(true, "تم الحذف بنجاح", "");

        }

        public OperationDetails AddCategory(CategoryManegerDTO categoryManegerDTO)
        {
            Category category = new Category();
            category.ImageUrl = categoryManegerDTO.ImageUrl == "" ? DefaultImages.Category : categoryManegerDTO.ImageUrl;
            category.ImageThumb = categoryManegerDTO.ImageThumb == "" ? DefaultImages.Category : categoryManegerDTO.ImageThumb;
            category.Banner = categoryManegerDTO.BannerImage == "" ? DefaultImages.Banner : categoryManegerDTO.BannerImage;
            category.SortOrder = categoryManegerDTO.SortOrder;
            category.Status = categoryManegerDTO.Status;
            category.DateModified = categoryManegerDTO.DateModefied;
            category.DateAdded = categoryManegerDTO.DateCreation;

            category.ParentId = categoryManegerDTO.ParentId;
            category = unitOfWork.CategoryRepository.Insert(category);


            CategoryDescription arabicCategoryDescription = new CategoryDescription();
            arabicCategoryDescription.LanguageId = (long)Langs.Arabic;
            arabicCategoryDescription.Description = categoryManegerDTO.ArabicDescription;
            arabicCategoryDescription.Name = categoryManegerDTO.ArabicName;
            arabicCategoryDescription.MetaDescription = categoryManegerDTO.ArabicMetaDescription;
            arabicCategoryDescription.CategoryId = category.Id;
            arabicCategoryDescription.DateAdded = categoryManegerDTO.DateCreation;
            arabicCategoryDescription.DateModefied = categoryManegerDTO.DateModefied;


            CategoryDescription englishCategoryDescription =
                new CategoryDescription();
            englishCategoryDescription.LanguageId = (long)Langs.English;
            englishCategoryDescription.Description = categoryManegerDTO.EnglishDescription;
            englishCategoryDescription.Name = categoryManegerDTO.EnglishName;
            englishCategoryDescription.MetaDescription = categoryManegerDTO.EnglishMetaDescription;
            englishCategoryDescription.CategoryId = category.Id;
            englishCategoryDescription.DateAdded = categoryManegerDTO.DateCreation;
            englishCategoryDescription.DateModefied = categoryManegerDTO.DateModefied;

            unitOfWork.CategoryDescriptionRepository.Insert(englishCategoryDescription);
            unitOfWork.CategoryDescriptionRepository.Insert(arabicCategoryDescription);


            unitOfWork.Save();

            return new OperationDetails(true, "تم الاضافة بنجاح", "");
        }
        public List<CategoryManegerDTO> Filter(string arabicName, string englishName, bool? status, WebSites w, long id, DateTime? dateAdded, DateTime? dateModefied, Sorts s)
        {

            List<long> categoryIds = unitOfWork.CategoryRepository.Get(c => true).AsEnumerable().Where(c => getWebsite(c.Id) == w).Select(c => c.Id).ToList();

            if (!String.IsNullOrEmpty(arabicName))
                FilterName(ref categoryIds, arabicName, Langs.Arabic);
            if (!String.IsNullOrEmpty(englishName))
                FilterName(ref categoryIds, englishName, Langs.English);
            if (status != null)
                FilterStatus(ref categoryIds, status.Value);
            if (id != null && id != 0)
                FilterId(ref categoryIds, id);

            if (dateAdded != null)
                FilterDate(ref categoryIds, dateAdded.Value, true);
            if (dateModefied != null)
                FilterDate(ref categoryIds, dateModefied.Value, false);


            List<CategoryManegerDTO> categories = new List<CategoryManegerDTO>();

            for (int i = 0; i < categoryIds.Count; i++)
            {
                categories.Add(GetCategoryManagerById(categoryIds[i]));
            }
            sortCategories(ref categories, s);
            return categories;
        }
        private void FilterName(ref List<long> productIds, string name, Langs l)
        {
            long s = (long)Utils.getLanguage(l);
            List<long> ids = unitOfWork.CategoryDescriptionRepository.
                                        Get(c => c.Name.Contains(name) && c.LanguageId == s).
                                            Select(op => op.CategoryId).Distinct().ToList();

            productIds = productIds.Intersect(ids).ToList();
        }
        private void FilterStatus(ref List<long> productIds, bool status)
        {
            List<long> ids = unitOfWork.CategoryRepository.
                                        Get(c => c.Status == status).
                                            Select(op => op.Id).Distinct().ToList();
            productIds = productIds.Intersect(ids).ToList();
        }
        private void FilterId(ref List<long> productIds, long id)
        {
            List<long> ids = unitOfWork.CategoryRepository.
                                        Get(c => c.Id == id).
                                            Select(op => op.Id).Distinct().ToList();
            productIds = productIds.Intersect(ids).ToList();
        }
        private void FilterDate(ref List<long> productIds, DateTime date, bool isDateAdded)
        {
            List<long> ids;
            if (isDateAdded)
            {
                ids = unitOfWork.CategoryRepository.
                                        Get(c => c.DateAdded.ToString().Contains(date.ToString())).
                                            Select(op => op.Id).Distinct().ToList();
            }
            else
            {
                ids = unitOfWork.CategoryRepository.
                                        Get(c => c.DateModified.ToString().Contains(date.ToString())).
                                            Select(op => op.Id).Distinct().ToList();
            }

            productIds = productIds.Intersect(ids).ToList();
        }

        private void sortCategories(ref List<CategoryManegerDTO> categories, Sorts s)
        {
            switch (s)
            {
                case Sorts.ArabicNameDown: categories.Sort((a, b) => -1 * a.ArabicName.CompareTo(b.ArabicName));   //products.OrderBy(op => op.Name);
                    break;
                case Sorts.ArabicNameUp: categories.Sort((a, b) => a.ArabicName.CompareTo(b.ArabicName));
                    break;
                case Sorts.EnglishNameDown: categories.Sort((a, b) => -1 * a.EnglishName.CompareTo(b.EnglishName)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.EnglishNameUp: categories.Sort((a, b) => a.EnglishName.CompareTo(b.EnglishName));
                    break;
                case Sorts.IdDown: categories.Sort((a, b) => -1 * a.Id.CompareTo(b.Id));
                    break;
                case Sorts.IdUp: categories.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
                case Sorts.StatusDown: categories.Sort((a, b) => -1 * a.Status.CompareTo(b.Status));
                    break;
                case Sorts.StatusUp: categories.Sort((a, b) => a.Status.CompareTo(b.Status));
                    break;
            }

        }

        private List<CategoryManegerDTO> getSubManagerCategories(long categoryId)
        {
            List<CategoryManegerDTO> categoryManegerDTOs = new List<CategoryManegerDTO>();
            List<Category> subCategories = unitOfWork.CategoryRepository.Get(c => c.ParentId == categoryId).ToList();
            categoryManegerDTOs.Add(GetCategoryManagerById(categoryId));

            foreach (var cat in subCategories)
            {
                categoryManegerDTOs.AddRange(getSubManagerCategories(cat.Id));
            }
            return categoryManegerDTOs;
        }

        public string GenerateSlug(string phrase)
        {
            if (String.IsNullOrEmpty(phrase))
                return "";
            string str = Utils.RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        private long GetCategoryIdByName(string categoryName, Langs l)
        {
            long languageId = Utils.getLanguage(l);
            CategoryDescription categoryDescription = unitOfWork.CategoryDescriptionRepository.Get(op =>
                                         op.LanguageId == (long)Langs.English
                                         ).AsEnumerable().Where(op => (op.Name == categoryName || Utils.GenerateSlug(op.Name) == categoryName)).FirstOrDefault();
            return categoryDescription.CategoryId;

        }
        private string GetCategoryNameById(long categoryId, Langs l)
        {
            long languageId = Utils.getLanguage(l);
            CategoryDescription categoryDescription = unitOfWork.CategoryDescriptionRepository.Get(op =>
                                         op.LanguageId == (long)Langs.English &&
                                         op.CategoryId == categoryId).FirstOrDefault();
            return categoryDescription.Name;
        }
        public List<CategoryPathDTO> GetAllPaths(Langs l, WebSites w)
        {

            return GetAllPaths(l).AsEnumerable().Where(c => (long)getWebsite(c.CategoryId) == (long)w).ToList();
        }
        public List<CategoryPathDTO> GetAllPaths(Langs l)
        {
            List<CategoryPathDTO> paths = new List<CategoryPathDTO>();
            paths.AddRange(getSubPathCategories((long)WebSites.Fashion, l));
            paths.AddRange(getSubPathCategories((long)WebSites.Mobile, l));
            paths.AddRange(getSubPathCategories((long)WebSites.Gym, l));
            return paths;
        }
        public List<CategoryPathDTO> GetAllPathsByWebsite(WebSites website, Langs l)
        {
            List<CategoryPathDTO> paths = new List<CategoryPathDTO>();
            paths.AddRange(getSubPathCategories((long)website, l));
            return paths;
        }
        private List<CategoryPathDTO> getSubPathCategories(long categoryId, Langs l)
        {
            List<CategoryPathDTO> categoryPathDTOs = new List<CategoryPathDTO>();
            List<Category> subCategories = unitOfWork.CategoryRepository.Get(c => c.ParentId == categoryId).ToList();
            categoryPathDTOs.Add(GetCategoryPath(GetCategoryNameById(categoryId, l), l));

            foreach (var cat in subCategories)
            {
                categoryPathDTOs.AddRange(getSubPathCategories(cat.Id, l));
            }
            return categoryPathDTOs;
        }
        private string ToStringPath(CategoryPathDTO categoryPathDTO)
        {
            string result = "";
            for (int i = 0; i < categoryPathDTO.Path.Count; i++)
            {
                if (i != 0)
                    result += " => ";
                result += categoryPathDTO.Path[i].Item2.Item1;
            }
            return result;
        }
        public CategoryPathDTO GetCategoryPath(string categoryName, Langs l)
        {
            long languageId = Utils.getLanguage(l);
            long categoryId = GetCategoryIdByName(categoryName, l);
            CategoryPathDTO categoryPathDTO = new CategoryPathDTO();
            categoryPathDTO.Path = new List<Tuple<long, Tuple<string, string>>>();
            categoryPathDTO.CategoryId = categoryId;

            while (categoryId != null)
            {
                categoryPathDTO.Path.Add(
                    new Tuple<long, Tuple<string, string>>
                        (categoryId,
                        new Tuple<string, string>(
                        unitOfWork.CategoryRepository.GetByID(categoryId).CategoryDescriptions.
                            Where(c => c.LanguageId == languageId).FirstOrDefault().Name,
                        unitOfWork.CategoryRepository.GetByID(categoryId).CategoryDescriptions.
                            Where(c => c.LanguageId == (long)Langs.English).FirstOrDefault().Name)
                        )
                );
                if (unitOfWork.CategoryRepository.GetByID(categoryId).ParentId == null)
                    break;
                categoryId = unitOfWork.CategoryRepository.GetByID(categoryId).ParentId.Value;
            }
            categoryPathDTO.Path.Reverse();
            categoryPathDTO.PathStr = ToStringPath(categoryPathDTO);
            return categoryPathDTO;
        }
        public Tuple<string, CategoryPathDTO> GetProductPath(long skuId, Langs l)
        {
            Product product = unitOfWork.ProductSKURepository.Get(s => s.SKUId == skuId).FirstOrDefault().Product;
            long lang = Utils.getLanguage(l);
            return new Tuple<string, CategoryPathDTO>(
                    product.ProductDescriptions.FirstOrDefault(c => c.LanguageId == lang).Name,
                    GetCategoryPath(
                         product.Category.CategoryDescriptions.Where(op => op.LanguageId == (long)Langs.English).FirstOrDefault().Name, l)
                );
        }

        public CategoryMenuDTO GetAll(Langs l, WebSites w)
        {
            CategoryMenuDTO ctmDTO = GetAll(l);
            ctmDTO.SubCategories =
                ctmDTO.SubCategories.AsEnumerable().Where(c => c.Id == (long)w).FirstOrDefault().SubCategories;
            return ctmDTO;
        }
        public CategoryMenuDTO GetAll(Langs l)
        {
            CategoryMenuDTO allCategories = new CategoryMenuDTO();
            allCategories.SubCategories = new List<CategoryMenuDTO>();
            allCategories.SubCategories.Add(GetClothesCategory(l));
            allCategories.SubCategories.Add(GetTechnoCategory(l));
            allCategories.SubCategories.Add(GetGymCategory(l));
            return allCategories;
        }

        public CategoryMenuDTO GetClothesCategory(Langs l)
        {
            return GetCategory((long)WebSites.Fashion, l);
        }
        public CategoryMenuDTO GetTechnoCategory(Langs l)
        {
            return GetCategory((long)WebSites.Mobile, l);
        }
        public CategoryMenuDTO GetGymCategory(Langs l)
        {
            return GetCategory((long)WebSites.Gym, l);
        }
        public List<CategoryMenuDTO> GetRandomCategories(Langs l, WebSites w)
        {
            List<long> ids=unitOfWork.CategoryRepository.Get(c=>true).Select(c=>c.Id).ToList();
            List<CategoryMenuDTO> list = new List<CategoryMenuDTO>();
            foreach(var id in ids){
                if(unitOfWork.CategoryRepository.Get(c=>c.ParentId==id).Count()==0
                    && (long)getWebsite(id)==(long)w)
                    list.Add(GetCategory(id,l));
            }
            
            Random random = new Random();
            int seed = random.Next();
            list = list.OrderBy(s => (~(s.Id & seed)) & (s.Id | seed)).ToList(); // ^ seed);

            //return list.Take(Math.Min(list.Count,3)).ToList();
            return list;
        }
        public CategoryMenuDTO GetRandomCategory(Langs l, WebSites w)
        {
            long languageId = Utils.getLanguage(l);
            var list = unitOfWork.CategoryRepository.Get(c => true).AsEnumerable().Where(c => (long)getWebsite(c.Id) == (long)w);
            Random random = new Random();
            long seed = random.Next();
            list = list.OrderBy(s => (~(s.Id & seed)) & (s.Id | seed)); // ^ seed);
            if (!list.Any())
                return null;
            var first = list.First();

            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryMenuDTO>()
                .ForMember(dest => dest.EnglishName, opt =>
                    opt.MapFrom(src => src.CategoryDescriptions.Where(dsc => dsc.LanguageId == (long)Langs.English).FirstOrDefault().Name)
                )
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.CategoryDescriptions.Where(dsc => dsc.LanguageId == (long)l).FirstOrDefault().Name))
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom(src => src.ImageUrl ?? DefaultImages.Category)
                ).ForMember(dest => dest.Banner, opt =>
                    opt.MapFrom(src => src.Banner ?? DefaultImages.Banner)
                )
                );
            
            //CategoryMenuDTO menuCategoryDTO = Mapper.Map<Category, CategoryMenuDTO>(first);
            CategoryMenuDTO menuCategoryDTO = Mapper.Map<Category, CategoryMenuDTO>(first, opt =>
        opt.AfterMap((src, dest) => dest.Name = src.CategoryDescriptions.Where(dsc => dsc.LanguageId == (long)l).FirstOrDefault().Name));
            return menuCategoryDTO;
        }

        public CategoryMenuDTO GetCategory(long categoryId, Langs l)
        {
            long languageId = Utils.getLanguage(l);
            if (categoryId == null)
                throw new ValidationException("No ID Specified", "");

            var category = unitOfWork.CategoryRepository.GetByID(categoryId);
            if (category == null)
                throw new ValidationException("No such Category found", "");

            return getSubCategories(categoryId, languageId);
        }

        public CategoryMenuDTO getSubCategories(long categoryId, long languageId)
        {
            List<CategoryMenuDTO> subMenuCategoryDTOs = new List<CategoryMenuDTO>();

            var category = unitOfWork.CategoryRepository.GetByID(categoryId);

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<Category, CategoryMenuDTO>()
                .ForMember(dest => dest.EnglishName, opt =>
                    opt.MapFrom(src => src.CategoryDescriptions.Where(dsc => dsc.LanguageId == (long)Langs.English).FirstOrDefault().Name)
                )
                    //.ForMember(dest => dest.Name, opt =>
                    //    opt.MapFrom(src => src.CategoryDescriptions.Where(dsc => dsc.LanguageId == languageId).FirstOrDefault().Name)
                    //)
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom(src => src.ImageUrl ?? DefaultImages.Category)
                ).ForMember(dest => dest.Banner, opt =>
                    opt.MapFrom(src => src.Banner ?? DefaultImages.Banner)
                )
                );
            var mapper = config.CreateMapper();

            //Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryMenuDTO>()
            //    .ForMember(dest => dest.EnglishName, opt =>
            //        opt.MapFrom(src => src.CategoryDescriptions.Where(dsc => dsc.LanguageId == (long)Langs.English).FirstOrDefault().Name)
            //    )
            //    //.ForMember(dest => dest.Name, opt =>
            //    //    opt.MapFrom(src => src.CategoryDescriptions.Where(dsc => dsc.LanguageId == languageId).FirstOrDefault().Name)
            //    //)
            //    .ForMember(dest => dest.ImageUrl, opt =>
            //        opt.MapFrom(src => src.ImageUrl ?? DefaultImages.Category)
            //    ).ForMember(dest => dest.Banner, opt =>
            //        opt.MapFrom(src => src.Banner ?? DefaultImages.Banner)
            //    )
            //);
            //CategoryMenuDTO menuCategoryDTO = Mapper.Map<Category, CategoryMenuDTO>(category);
            CategoryMenuDTO menuCategoryDTO = mapper.Map<Category, CategoryMenuDTO>(category, opt =>
        opt.AfterMap((src, dest) => dest.Name = src.CategoryDescriptions.Where(dsc => dsc.LanguageId == languageId).FirstOrDefault().Name));

            menuCategoryDTO.Name = category.CategoryDescriptions.Where(dsc => dsc.LanguageId == languageId).FirstOrDefault().Name;
            menuCategoryDTO.EnglishName = category.CategoryDescriptions.Where(dsc => dsc.LanguageId == (long)Langs.English).FirstOrDefault().Name;

            List<Category> subCategories = unitOfWork.CategoryRepository.Get(c => c.Status == true && c.ParentId == categoryId).OrderBy(c=>c.SortOrder).ToList();

            foreach (var cat in subCategories)
            {
                subMenuCategoryDTOs.Add(getSubCategories(cat.Id, languageId));
            }
            subMenuCategoryDTOs.OrderBy(c => c.SortOrder);
            menuCategoryDTO.SubCategories = subMenuCategoryDTOs;

            return menuCategoryDTO;
        }


        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
