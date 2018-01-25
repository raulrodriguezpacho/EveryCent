using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;

namespace EveryCent.Droid
{
	[Activity (Label = "EveryCent", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();
            ImageCircleRenderer.Init();
            LoadApplication(new EveryCent.App(new Platform.PlatformModule()));            
		}
	}
}

