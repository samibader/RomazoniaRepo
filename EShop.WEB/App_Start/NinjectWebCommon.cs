[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EShop.WEB.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(EShop.WEB.App_Start.NinjectWebCommon), "Stop")]

namespace EShop.WEB.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using EShop.WEB.Util;
    using Ninject.Modules;
    using EShop.BLL.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.AspNet.Identity.Owin;
    using EShop.DAL.EF;
    using EShop.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var modules = new INinjectModule[] { new ServiceModule("DefaultConnection") };
            var kernel = new StandardKernel(modules);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            ///////////
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            //kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            // Binding trick to avoid having to bind all usages of IGenericRepository. This binds them all!
            //kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));

            // Identity
            kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>)).InRequestScope().WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind<ApplicationUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());

            // Mail
            //kernel.Bind<IRazorEngineService>().ToMethod(m => RazorEngineService.Create());
            //kernel.Bind<IMailHandler>().To<MailHandler>();
            kernel.Bind<IIdentityMessageService>().To<EmailService>();

            //kernel.Bind<MapperConfiguration>()
            //    .ToSelf()
            //    .WithConstructorArgument<Action<IMapperConfigurationExpression>>(cfg => cfg.AddProfile<MappingProfile>());
            //kernel.Bind<IMapper>().ToMethod(mapper => kernel.Get<MapperConfiguration>().CreateMapper()).InSingletonScope();
        }        
    }
}
