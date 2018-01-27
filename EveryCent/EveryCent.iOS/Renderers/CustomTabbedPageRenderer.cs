using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveryCent.iOS.Renderers;
using EveryCent.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace EveryCent.iOS.Renderers
{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            App.TabHeight = (int)TabBar.Frame.Height;
        }
    }
}