

using EShop.BLL.Interfaces;
using EShop.BLL.Services;
using EShop.DAL.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.BLL.Infrastructure;

namespace EShop.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<ISampleService>().To<SampleService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<ICartService>().To<CartService>();
            kernel.Bind<IProductFilterService>().To<ProductFilterService>();
            kernel.Bind<IDesignerFilterService>().To<DesignerFilterService>();
            kernel.Bind<ITagFilterService>().To<TagFilterService>();
            kernel.Bind<IFavoriteService>().To<FavoriteService>();
            kernel.Bind<IAddressService>().To<AddressService>();
            kernel.Bind<ICheckOutService>().To<CheckOutService>();
            kernel.Bind<ICouponService>().To<CouponService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IProductManagerService>().To<ProductManagerService>();
            kernel.Bind<IManageSizeAttributes>().To<ManageSizeAttributes>();

            kernel.Bind<IManageAttributeGroup>().To<ManageAttributeGroupService>(); kernel.Bind<IPaypalService>().To<PaypalService>();
            kernel.Bind<IManageDesigner>().To<ManageDesignerService>();
            kernel.Bind<IDiscount>().To<DiscountService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ISliderService>().To<SliderService>();


        }
    }
}