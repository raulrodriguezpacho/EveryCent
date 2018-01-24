using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using EveryCent.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace EveryCent.Droid.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        ViewPager _pager;
        TabLayout _layout;

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);            
            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }
            if (e.NewElement != null)
            {
                e.NewElement.PropertyChanged += OnElementPropertyChanged;
            }                        
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Renderer")
            {
                _pager = (ViewPager)ViewGroup.GetChildAt(0);
                _layout = (TabLayout)ViewGroup.GetChildAt(1);
                ColorStateList colors = null;
                if ((int)Build.VERSION.SdkInt >= 23)
                {
                    colors = Resources.GetColorStateList(Resource.Color.icon_tab, Forms.Context.Theme);
                }
                else
                {
                    colors = Resources.GetColorStateList(Resource.Color.icon_tab);
                }
                for (int i = 0; i < _layout.TabCount; i++)
                {
                    var tab = _layout.GetTabAt(i);                    
                    var icon = tab.Icon;                                        
                    if (icon != null)
                    {                        
                        icon = Android.Support.V4.Graphics.Drawable.DrawableCompat.Wrap(icon);
                        Android.Support.V4.Graphics.Drawable.DrawableCompat.SetTintList(icon, colors);
                    }
                }
            }              
        }        
    }
}