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
    public interface IDiscount
    {
        OperationDetails EditDiscount(DiscountDTO dto);
        DiscountDTO GetDiscount(long Id, Langs l, Currency cu);
        List<ProductSKUDTO> GetDiscountSkus(long Id, Langs l, Currency cu);
        OperationDetails AddDiscount(DiscountDTO dto);
        List<ProductDTO> GetProductSKUs(long ProductId, Langs l, Currency cu);
        OperationDetails AddDiscountToSkus(List<long> SkusIds, long discountId);
        OperationDetails DeleteSkuFromDiscount(long skuId, long discountId);
        List<DiscountDTO> Filter(String arabicName, String englishName, bool? IsPercentage, long discountId, Sorts s, Langs l, Currency cu);
        OperationDetails DeleteDiscount(long Id);
    }
}
