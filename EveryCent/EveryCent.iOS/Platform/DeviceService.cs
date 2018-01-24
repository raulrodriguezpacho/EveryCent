using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveryCent.Services;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace EveryCent.iOS.Platform
{
    public class DeviceService : IDeviceService
    {
        public Size GetDeviceSize()
        {
            return new Size()
            {
                Width = UIScreen.MainScreen.Bounds.Width,
                Height = UIScreen.MainScreen.Bounds.Height
            };
        }        
    }
}