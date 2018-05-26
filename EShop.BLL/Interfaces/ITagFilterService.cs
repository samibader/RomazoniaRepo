using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.Common;

namespace EShop.BLL.Interfaces
{
    public interface ITagFilterService
    {
        FilterMenuDTO FilterFilters(String tagName,String[]d, String[] subCategories, String[] colorNames, String[] sizeNames, double lowPrice, double highPrice, Sorts s, Currency currency, Langs l);
        TagDTO GetTag(string tagName,Langs l);
    }
}
