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
    public class CartService : ICartService
    {
        IUnitOfWork unitOfWork { get; set; }
        private IProductService _productService;
        public CartService(IUnitOfWork uow, IProductService productService)
        {

            unitOfWork = uow;
            _productService = productService;
        }

        public OperationDetails AddToCart(long skuId, long productId, String userId, int quantity)
        {
            ProductSKU sku = unitOfWork.ProductSKURepository.Get(s => s.SKUId == skuId).FirstOrDefault();
            if (sku == null)
            {
                return new OperationDetails(false, "المنتج غير موجود", "SkuId");
            }
            if (sku.Quentity < quantity || quantity == 0)
            {
                return new OperationDetails(false, "لايوجد هذه الكمية", "Qty");
            }
            // sku.Quentity -= quantity;
            ShoppingCart userCart =
                unitOfWork.ShoppingCartRepository.Get(s => s.SKUId == skuId && s.UserId == userId).FirstOrDefault();
            if (userCart == null)
                unitOfWork.ShoppingCartRepository.Insert(new ShoppingCart() { ProductId = productId, SKUId = skuId, UserId = userId, Quantity = quantity });
            else
            {
                userCart.Quantity += quantity;
            }
            unitOfWork.Save();
            return new OperationDetails(true, "تم اضافة المنتج إلى السلة", "");

        }

        public OperationDetails Remove(long skuId, long productId, string userId)
        {
            ShoppingCart cart = unitOfWork.ShoppingCartRepository.Get(s => s.SKUId == skuId && s.ProductId == productId && s.UserId == userId).FirstOrDefault();

            if (cart == null)
            {
                return new OperationDetails(false, "حدث خطأ أثناء الحذف", "");
            }
            ProductSKU product = cart.ProductSKU;
            product.Quentity += (int)cart.Quantity;
            unitOfWork.ShoppingCartRepository.Delete(cart);
            unitOfWork.Save();
            return new OperationDetails(true, "تم الحذف بنجاح", "");
        }
        public OperationDetails RemoveAll(string userId)
        {
            List<ShoppingCart> carts = unitOfWork.ShoppingCartRepository.Get(s => s.UserId == userId).ToList();

            if (carts.Count == 0)
            {
                return new OperationDetails(false, "سلة الشراء فارغة", "");
            }
            foreach (var cart in carts)
            {
                Remove(cart.SKUId, cart.ProductId, userId);
            }
            unitOfWork.Save();
            return new OperationDetails(true, "تم الحذف بنجاح", "");
        }
        public List<ProductDTO> GetCartProducts(string userId, Langs l, Currency currency)
        {
            List<ShoppingCart> userCartItems = unitOfWork.ShoppingCartRepository.Get(c => c.UserId == userId).ToList();

            List<ProductDTO> productDtos = new List<ProductDTO>();
            foreach (var userCartItem in userCartItems)
            {
                ProductDTO product = _productService.GetProductSKU(userCartItem.SKUId, l, currency);
                product.Quantity = (int)userCartItem.Quantity;
                productDtos.Add(product);
            }
            return productDtos;
        }

        public OperationDetails UpdateCart(string userId, List<ProductDTO> products)
        {

            foreach (var cartItem in products)
            {
                if (cartItem.Quantity == 0)
                    return new OperationDetails(false, "لايمكن للكمية أن تكون معدومة", "");
                ProductSKU sku = unitOfWork.ProductSKURepository.Get(s => s.SKUId == cartItem.SKUId).FirstOrDefault();
                if(sku==null)
                    return new OperationDetails(false, "حدث خطأ أثناء تعديل السلة", "skuId");
                if(sku.Quentity < cartItem.Quantity)
                    return new OperationDetails(false,"الكميات المطلوبة غير متوفرة","");
                var realItem = unitOfWork.ShoppingCartRepository.Get(c => c.UserId == userId && c.SKUId == cartItem.SKUId && c.ProductId == cartItem.Id).FirstOrDefault();
                if (realItem == null)
                    return new OperationDetails(false, "حدث خطأ أثناء تعديل السلة", "skuId");
                realItem.Quantity = cartItem.Quantity;
            }
            unitOfWork.Save();
            return new OperationDetails(true, "تم تعدل السلة بنجاح", "");
        }
    }
}
