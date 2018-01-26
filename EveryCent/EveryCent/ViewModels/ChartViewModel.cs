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

namespace EveryCent.ViewModels
{
    public class ChartViewModel : ViewModelBase, IDateViewModel
    {        
        private readonly IMovementRepository _repositoryService;
        private readonly INavigationService _navigationService;

        private List<Tuple<string, decimal>> _serieIncome = new List<Tuple<string, decimal>>();
        private List<Tuple<string, decimal>> _serieSpend = new List<Tuple<string, decimal>>();        

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

        private ObservableCollection<List<Tuple<string, decimal>>> _chartData = new ObservableCollection<List<Tuple<string, decimal>>>();
        public ObservableCollection<List<Tuple<string, decimal>>> ChartData
        {
            get
            {
                return _chartData;
            }
            set
            {
                _chartData = value;
                OnPropertyChanged("ChartData");
            }
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
            INavigationService navigationService)            
        {            
            _repositoryService = repositoryService;
            _navigationService = navigationService;
            GetData();

            MessagingCenter.Subscribe<ViewModelBase, string>(this, "currency", (sender, arg) =>
            {
                CurrentCurrency = arg;
            });
        }

        private void GetData()
        {            
            var movements = _repositoryService.GetByYear(_selectedYear).ToList();
            TransformToChartData(movements);
        }

        private void TransformToChartData(IList<Movement> movements)
        {
            if (movements.IsNullOrEmpty())
                return;

            _serieIncome.Clear();
            _serieSpend.Clear();

            try
            {
                for (int i = 1; i <= 12; i++)
                {
                    var income = movements.Where(m => m.IsPositive && m.Date.Month == i).Sum(m => (decimal)m.Amount / 100);
                    _serieIncome.Add(new Tuple<string, decimal>(GetMonthShort(i), income));
                    var spend = movements.Where(m => !m.IsPositive && m.Date.Month == i).Sum(m => (decimal)m.Amount / 100);
                    _serieSpend.Add(new Tuple<string, decimal>(GetMonthShort(i), spend));
                }
                if (!_chartData.IsNullOrEmpty())
                    ChartData.Clear();
                ChartData.Add(_serieIncome);
                ChartData.Add(_serieSpend);
                //ChartData = new ObservableCollection<List<Tuple<string, decimal>>>()
                //{
                //    _serieIncome, _serieSpend
                //};
            }
            catch { }
        }
    }
}
