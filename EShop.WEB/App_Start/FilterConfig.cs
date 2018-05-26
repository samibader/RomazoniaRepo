using EShop.WEB.Models.Filters;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SpecificWebsiteViewActionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
