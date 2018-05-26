using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(EShop.WEB.Startup))]
namespace EShop.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            ConfigureAuth(app);
        }
    }
}
