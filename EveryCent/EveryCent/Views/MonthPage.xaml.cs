using EveryCent.Base;
using EveryCent.ViewModels;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EveryCent.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthPage : ViewPageBase
    {
        public MonthPage()
        {
            InitializeComponent();
            if (this.BindingContext != null && this.BindingContext is MonthViewModel)
            {
                ((MonthViewModel)this.BindingContext).Position = DateTime.Now.Month - 1;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            meses.PositionSelected += PositionSelected;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            meses.PositionSelected -= PositionSelected;
        }

        public void PositionSelected(object sender, int position)
        {
            if (this.BindingContext != null && this.BindingContext is MonthViewModel)
            {                
                string posicionMes = DateTimeFormatInfo.CurrentInfo.MonthNames[position];
                if (((MonthViewModel)this.BindingContext).SelectedMonth != posicionMes)
                    ((MonthViewModel)this.BindingContext).SelectedMonth = posicionMes;
            }
        }
    }
}