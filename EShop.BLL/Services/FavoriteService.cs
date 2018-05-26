using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;

namespace EShop.BLL.Services
{
    public class FavoriteService : IFavoriteService
    {
        IUnitOfWork unitOfWork { get; set; }
        private IProductService _productService;
        public FavoriteService(IUnitOfWork uow, IProductService productService)
        {

            unitOfWork = uow;
            _productService = productService;
        }

        public OperationDetails AddToFavorite(long productId, long skuId, string userId)
        {
            Favorite userFavorite =
                unitOfWork.FavoriteRepository.Get(s => s.ProductId == productId && s.SKUId == skuId && s.UserId == userId).FirstOrDefault();
            if (userFavorite == null)
                unitOfWork.FavoriteRepository.Insert(new Favorite() { ProductId = productId, SKUId = skuId, UserId = userId });
            else
            {
                return new OperationDetails(true, "المنتج موجود مسبقاً في المفضلة ", "");
            }
            unitOfWork.Save();
            return new OperationDetails(true, "تم اضافة المنتج إلى المفضلة", "");
        }

        public OperationDetails Remove(long productId,long skuId, String userId)
        {
            Favorite fav = unitOfWork.FavoriteRepository.Get(s => s.ProductId == productId &&s.SKUId==skuId && s.UserId == userId).FirstOrDefault();

            if (fav == null)
            {
                return new OperationDetails(false, "حدث خطأ أثناء الحذف", "");
            }

            unitOfWork.FavoriteRepository.Delete(fav);
            unitOfWork.Save();
            return new OperationDetails(true, "تم الحذف بنجاح", "");
        }

        public List<ProductDTO> GetFavoriteProducts(string userId, Langs l, Currency currency)
        {
            List<Favorite> userFavorites = unitOfWork.FavoriteRepository.Get(c => c.UserId == userId).ToList();

            List<ProductDTO> productDtos = new List<ProductDTO>();
            foreach (var userFavorite in userFavorites)
            {
                ProductDTO product = _productService.GetProductSKU(userFavorite.SKUId, l, currency);
                productDtos.Add(product);
            }
            return productDtos;
        }

        public int Count(string userId)
        {
            return unitOfWork.FavoriteRepository.Get(c => c.UserId == userId).ToList().Count;

        }
    }
}
