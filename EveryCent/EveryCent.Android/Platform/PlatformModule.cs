using Autofac;
using EveryCent.Services;

namespace EveryCent.Droid.Platform
{
    public class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Localize>().As<ILocalize>();
            builder.RegisterType<LocalPath>().As<ILocalPath>();
        }
    }
}