using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryMenuDTO> GetRandomCategories(Langs l, WebSites w);
        CategoryMenuDTO getSubCategories(long categoryId, long languageId);
        CategoryMenuDTO GetRandomCategory(Langs l,WebSites w);
        WebSites getWebsite(long categoryId);
        
        OperationDetails DeleteCategory(long categoryId);
        CategoryMenuDTO GetLatestCategory(Langs l,WebSites w);
        CategoryPathDTO GetCategoryPath(string  categoryName, Langs l);
        Tuple<string,CategoryPathDTO> GetProductPath(long skuId, Langs l);


        CategoryMenuDTO GetAll(Langs l,WebSites w);
        List<CategoryPathDTO> GetAllPathsByWebsite(WebSites website, Langs l);
        CategoryMenuDTO GetCategory(long categoryId, Langs l);

        List<CategoryPathDTO> GetAllPaths(Langs l,WebSites w);


        List<CategoryManegerDTO> GetAllManegerCategories(Sorts s,WebSites w);
        List<CategoryManegerDTO> GetAllManegerCategories(Sorts s);
        CategoryManegerDTO GetCategoryManagerById(long categoryId);
        OperationDetails AddCategory(CategoryManegerDTO categoryManegerDTO);
        OperationDetails EditCategoryManager(CategoryManegerDTO categoryManegerDTO);
        List<CategoryManegerDTO> Filter(string arabicName, string englishName, bool? status,WebSites w, long id, DateTime? dateAdded, DateTime? dateModefied, Sorts s);
        void Dispose();
    }
}
