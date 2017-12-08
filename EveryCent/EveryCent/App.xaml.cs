using Autofac;
using Autofac.Core;
using EveryCent.Services;
using EveryCent.Views;
using Xamarin.Forms;

namespace EveryCent
{
    public partial class App : Application
    {
        //public App()
        //{
        //    InitializeComponent();

        //    if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
        //    {
        //        var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        //        EveryCent.Resources.AppResources.Culture = ci; // set the RESX for resource localization
        //        DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
        //    }

        //    MainPage = new EveryCent.Views.DashboardPage();
        //}

        private static IContainer _container;
        private readonly ILocalize _language;

        public App(IModule platformModule)
        {
            Register(platformModule);
            InitializeComponent();
            SetLanguage();
            MainPage = new DashboardPage();
        }

        private static void Register(IModule platformModule)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(platformModule);
            _container = builder.Build();
        }

        private void SetLanguage()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var language = _container.Resolve<ILocalize>();
                var culture = language.GetCurrentCultureInfo();
                EveryCent.Resources.AppResources.Culture = culture;
                language.SetLocale(culture);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}