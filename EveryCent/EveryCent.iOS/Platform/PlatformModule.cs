using Autofac;
using EveryCent.Services;

namespace EveryCent.iOS.Platform
{
    public class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Localize>().As<ILocalize>();
            builder.RegisterType<LocalPath>().As<ILocalPath>();
            builder.RegisterType<DeviceService>().As<IDeviceService>();
        }
    }
}