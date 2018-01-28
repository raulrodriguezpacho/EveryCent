using EveryCent.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryCent.Services;
using System.Globalization;
using EveryCent.Model;
using EveryCent.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using EveryCent.Base;
using Microcharts;

namespace EveryCent.ViewModels
{
    public class ChartViewModel : ViewModelBase, IDateViewModel
    {        
        private readonly IMovementRepository _repositoryService;
        private readonly INavigationService _navigationService;
        private readonly IDeviceService _deviceService;
       
        private List<Microcharts.Entry> _chartEntries = new List<Microcharts.Entry>();

        public event EventHandler<EventArgs> UpdateData;

        private string _selectedMonth = DateTimeFormatInfo.CurrentInfo.MonthNames[DateTime.Now.Month - 1];
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged("SelectedMonth");
                GetData();
            }
        }
        
        public bool MonthsVisible
        {
            get { return false; }            
        }

        private int _selectedYear = DateTime.Now.Year;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                OnPropertyChanged("SelectedYear");
                GetData();
            }
        }

        private double _heightChart = 0;
        public double HeightChart
        {
            get { return _heightChart; }
            set
            {
                _heightChart = value;
                OnPropertyChanged("HeightChart");
            }
        }        
        
        public BarChart ChartData
        {
            get; private set;
        }

        private ICommand _goToCurrencyCommand;
        public ICommand GoToCurrencyCommand
        {
            get
            {
                return _goToCurrencyCommand ?? (_goToCurrencyCommand = new Command(() =>
                {                    
                    _navigationService.NavigateToAsync<CurrencyViewModel>();
                }));
            }
        }

        public ChartViewModel(
            IMovementRepository repositoryService,
            INavigationService navigationService,
            IDeviceService deviceService)            
        {            
            _repositoryService = repositoryService;
            _navigationService = navigationService;
            _deviceService = deviceService;
            GetData();
            SetChart();

            MessagingCenter.Subscribe<ViewModelBase, string>(this, "currency", (sender, arg) =>
            {
                CurrentCurrency = arg;
                GetData();
            });
            MessagingCenter.Subscribe<Movement>(this, "movement", (sender) =>
            {
                GetData();
            });
        }

        private void GetData()
        {            
            var movements = _repositoryService.GetByYear(_selectedYear).ToList();
            TransformToChartData(movements);
        }

        private void TransformToChartData(IList<Movement> movements)
        {
            try
            {                
                ChartData = null;
                ChartData = new BarChart();

                if (!_chartEntries.IsNullOrEmpty())
                    _chartEntries.Clear();                
                for (int i = 1; i <= 12; i++)
                {
                    var income = movements.Where(m => m.IsPositive && m.Date.Month == i).Sum(m => (decimal)m.Amount / 100);
                    var spend = movements.Where(m => !m.IsPositive && m.Date.Month == i).Sum(m => (decimal)m.Amount / 100);
                    Microcharts.Entry entry = new Microcharts.Entry((float)(income - spend))
                    {
                        Label = GetMonthShort(i),
                        ValueLabel = (income > spend ? "+" : "") + decimal.Parse((income - spend).ToString()).ToString("N2") +
                            (Application.Current.Properties.ContainsKey("Currency") ? " " + Application.Current.Properties["Currency"].ToString() : ""),
                        Color = (income > spend ? SkiaSharp.SKColor.Parse("#008200") : 
                             (income < spend ? SkiaSharp.SKColor.Parse("#FF0000") : SkiaSharp.SKColor.Parse("#ADAAAD"))),
                        TextColor = SkiaSharp.SKColor.Parse("#000000")
                    };
                    _chartEntries.Add(entry);
                }
                ChartData.Entries = _chartEntries;
                if (UpdateData != null)
                    UpdateData(this, null);
            }
            catch { }            
        }        

        private void SetChart()
        {
            var size = _deviceService.GetDeviceSize();
            HeightChart = size.Width;            
        }
    }
}
