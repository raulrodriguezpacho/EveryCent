using Autofac.Core;
using EveryCent.Base;
using EveryCent.Data;
using EveryCent.Services;
using EveryCent.Views;
using Xamarin.Forms;

namespace EveryCent
{    
    public partial class App : Application
    {
        public static string[] Currency =
        {
            "€",
            "$",            
            "£"            
        };        

        static EveryCentDatabase database;
        public static EveryCentDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = LocatorBase.Resolve<EveryCentDatabase>();
                }
                return database;
            }
        }

        public App(IModule platformModule)
        {            
            LocatorBase.Register(platformModule, true); // NOW MOCKING!!
            InitializeComponent();
            SetLanguage();                        
            MainPage = new DashboardPage();            
        }        

        private void SetLanguage()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {                
                var language = LocatorBase.Resolve<ILocalize>();
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