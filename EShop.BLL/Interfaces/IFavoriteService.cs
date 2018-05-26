using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.Common;

namespace EShop.BLL.Infrastructure
{
    public interface IFavoriteService
    {
        OperationDetails AddToFavorite(long productId, long skuId, String userId);
        OperationDetails Remove(long productId, long skuId,String userId);
        List<ProductDTO> GetFavoriteProducts(string userId, Langs l,Currency c);
        int Count(String userId );
    }
}
