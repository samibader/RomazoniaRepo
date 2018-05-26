using EShop.BLL;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace EShop.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           // DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeAutoMapper.Initialize();
          
        }
    }
}
