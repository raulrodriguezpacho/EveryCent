﻿using Autofac;
using Autofac.Core;
using EveryCent.Base;
using EveryCent.Services;
using EveryCent.Views;
using Xamarin.Forms;

namespace EveryCent
{
    public partial class App : Application
    {                
        public App(IModule platformModule)
        {            
            LocatorBase.Register(platformModule);
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