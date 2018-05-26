using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.Common;

namespace EShop.BLL.Interfaces
{
    public interface IDesignerFilterService
    {
        List<DesignerDTO> GetAllDesigners(WebSites w);
        FilterMenuDTO FilterFilters(String designerName, String[] subCategories, String[] colorNames, String[] sizeNames, String[] tags, double lowPrice, double highPrice, Sorts s, Currency currency, Langs l);
        DesignerDTO GetDesigner(string designerName,Langs l);
    }
}
