using EveryCent.Base;
using EveryCent.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EveryCent.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyPage : ViewPageBase
    {
        public CurrencyPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            currencies.PositionSelected += PositionSelected;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            currencies.PositionSelected -= PositionSelected;
        }

        public void PositionSelected(object sender, int position)
        {
            if (this.BindingContext != null && this.BindingContext is CurrencyViewModel)
            {                
                if (((CurrencyViewModel)this.BindingContext).Position != position)
                    ((CurrencyViewModel)this.BindingContext).Position = position;
            }
        }
    }
}