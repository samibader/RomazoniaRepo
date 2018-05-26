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
    public interface IProductService
    {
        List<ProductDTO> GetRelatedProducts(long productId, Langs l, Currency c);
        List<TagDTO> GetAllTags();
        OperationDetails AddProductRate(ProductRateDTO productRate);
        OperationDetails EditProductDescription(ProductDTO product,Langs l);
        long GetProductIdFromSKU(string sku);
        MainPageProductsDTO GetMainPageProducts(Langs l, Currency currency,WebSites w);
        void Fix(long productId, ref long? skuId, ref string colorName, ref string sizeName);
        ProductDTO GetProduct(long productId,Langs l,Currency c);
        ProductDTO GetProduct(long productId, Langs l, Currency c, string color);
        List<ProductDTO> GetCategoryTreeProducts(string categoryName, Langs l,Currency c);
         ProductDTO GetProductSKU(long skuId, Langs l,Currency c);
        long GetCategoryIdByName(string categoryName, Langs l);
        List<ProductRateDTO> GetProductRates(long productId);
        int GetProductRate(long productId);
        List<ProductDTO> GetLatestProducts(Langs l, Currency c, WebSites w);
        List<ProductDTO> GetMostPopularProducts(Langs l, Currency c, WebSites w);
        void Dispose();
    }
}
