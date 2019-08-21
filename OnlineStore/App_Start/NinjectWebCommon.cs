[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OnlineStore.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OnlineStore.App_Start.NinjectWebCommon), "Stop")]

namespace OnlineStore.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Moq;
    using Ninject;
    using Ninject.Web.Common;
    using OnlineStore.Domain.Abstract;
    using OnlineStore.Domain.Concrete;
    using OnlineStore.Domain.Entities;

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
            var kernel = new StandardKernel();
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
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {

                new Product(){Name="T-shirt",Description="T-shirts for summer", Category="Men", Price=73},
                new Product(){Name="Socks",Description="Socks for brilliants", Category="Men", Price=23},
                new Product(){Name="T-shirt",Description="A great fitted shoes for soccer players", Category="Woman", Price=223},
                 new Product(){Name="Soccer Shoes",Description="T-shirts for summer", Category="Men", Price=13},
                 new Product(){Name="Braclet",Description="fits parties and big events", Category="Woman", Price=23},
                 new Product(){Name="Parfum",Description="perfect for work environemnt", Category="Woman", Price=23},
                 new Product(){Name="Splider Man",Description="Toys for kids", Category="Kids", Price=23},
                 new Product(){Name="Car",Description="health freindly and kids lovely", Category="Kids", Price=23},
            });
           kernel.Bind<IProductRepository>().ToConstant(mock.Object);

           // kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();
            kernel.Bind<IAuthentication>().To<FormsAuthenticationProvider>();
        }        
    }
}
