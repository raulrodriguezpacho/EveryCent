using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using EveryCent.Services;
using Xamarin.Forms;

namespace EveryCent.Droid.Platform
{
    public class DeviceService : IDeviceService
    {
        public Size GetDeviceSize()
        {
            var context = Forms.Context;
            return new Size()
            {
                Width = context.Resources.DisplayMetrics.WidthPixels / context.Resources.DisplayMetrics.Density,
                Height = context.Resources.DisplayMetrics.HeightPixels / context.Resources.DisplayMetrics.Density
            };
        }        
    }
}