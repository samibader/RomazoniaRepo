using EShop.BLL.DTO;
using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Interfaces
{
    public interface IProductFilterService
    {
       
        FilterMenuDTO FilterFilters(String categoryName, String[] designers, String[] subCategories, String[] colorNames, String[] sizeNames,String [] tags, double lowPrice, double highPrice, Sorts s, Currency currency, Langs l);
        List<ProductDTO> Filter(String categoryName,String[] designers, String[] subCategories, String[] colorNames,String [] tags, String[] sizeNames,double lowPrice,double highPrice,Sorts s,Currency c,Langs l);
        List<ProductDTO> FilterByName(string prefix, long categoryId, Langs l, Currency currency,WebSites w);
    }
}
