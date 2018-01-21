using Autofac;
using Autofac.Core;
using EveryCent.Data;
using EveryCent.Services;
using EveryCent.ViewModels;

namespace EveryCent.Base
{
    public static class LocatorBase
    {
        private static bool _mock;

        private static IContainer _container;        
        public static IContainer Container
        {
            get
            {
                return _container;
            }
        }

        public static void Register(IModule module = null, bool mock = false)
        {
            _mock = mock;
            var builder = new ContainerBuilder();
            RegisterServices(builder);
            RegisterViewModels(builder);
            if (module != null) RegisterPlatformModule(builder, module);            
            _container = builder.Build();                
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            if (!_mock)
            {
                builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
                builder.RegisterType<EveryCentDatabase>().SingleInstance();
                builder.RegisterType<MovementRepository>().As<IMovementRepository>().SingleInstance();
            }
            else
            {
                builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
                builder.RegisterInstance(new MovementMockRepository()).As<IMovementRepository>();
            }
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<ChartViewModel>();
            builder.RegisterType<ListViewModel>();
            builder.RegisterType<MonthViewModel>();
            builder.RegisterType<MovementViewModel>();
            builder.RegisterType<CurrencyViewModel>();
            builder.RegisterType<MovementsDayViewModel>();
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
