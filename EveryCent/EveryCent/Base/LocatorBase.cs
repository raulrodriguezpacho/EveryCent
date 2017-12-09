using Autofac;
using Autofac.Core;
using EveryCent.Data;
using EveryCent.Services;
using EveryCent.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Base
{
    public static class LocatorBase
    {
        private static IContainer _container;        
        public static IContainer Container
        {
            get
            {
                return _container;
            }
        }

        public static void Register(IModule module = null)
        {
            var builder = new ContainerBuilder();
            RegisterServices(builder);
            RegisterViewModels(builder);
            if (module != null) RegisterPlatformModule(builder, module);
            _container = builder.Build();                
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<Navigation>().As<INavigationService>();
            builder.RegisterType<MovementRepository>().As<IMovementRepository>();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<ChartViewModel>();
            builder.RegisterType<ListViewModel>();
            builder.RegisterType<MonthViewModel>();
            builder.RegisterType<MovementViewModel>();
        }

        private static void RegisterPlatformModule(ContainerBuilder builder, IModule module)
        {
            builder.RegisterModule(module);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
