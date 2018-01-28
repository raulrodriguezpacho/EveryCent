using EveryCent.Base;
using EveryCent.Services;
using EveryCent.ViewModels;
using EveryCent.ViewModels.Base;
using Xamarin.Forms.Xaml;

namespace EveryCent.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ViewPageBase
    {
        private ChartViewModel _vm;

        public ChartPage()
        {
            InitializeComponent();            
            _vm = (ChartViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
            if (_vm != null)
                _vm.UpdateData += OnUpdateData;
            GetData();
        }

        private void OnUpdateData(object sender, System.EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            try
            {                
                chartView.Chart = _vm.ChartData;
            }
            catch { }
        }
    }
}