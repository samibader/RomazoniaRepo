using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.Common;

namespace EShop.BLL.Interfaces
{
   public interface ICartService
   {
       OperationDetails AddToCart(long skuId, long productId, String userId, int quantity);
       OperationDetails Remove(long skuId, long productId,String userId);
       OperationDetails RemoveAll( String userId);
       List<ProductDTO> GetCartProducts(string userId, Langs l,Currency c);
       OperationDetails UpdateCart(string userId, List<ProductDTO> products);
   }
}
