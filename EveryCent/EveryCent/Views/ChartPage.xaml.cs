using EveryCent.Base;
using EveryCent.Services;
using Xamarin.Forms.Xaml;

namespace EveryCent.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ViewPageBase
    {
        public ChartPage()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var size = LocatorBase.Resolve<IDeviceService>();
            movementsChart.WidthRequest = size.GetDeviceSize().Width * 2 - movementsChart.Spacing;
        }
    }
}