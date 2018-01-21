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

namespace EveryCent.ViewModels
{
    public class ChartViewModel : ViewModelBase, IDateViewModel
    {        
        private readonly IMovementRepository _repositoryService;

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

        private bool _monthsVisible = false;
        public bool MonthsVisible
        {
            get { return _monthsVisible; }
            set
            {
                _monthsVisible = value;
                OnPropertyChanged("MonthsVisible");                
            }
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

        private ObservableCollection<List<Tuple<string, decimal>>> _chartData;
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

        public ChartViewModel(IMovementRepository repositoryService)            
        {            
            _repositoryService = repositoryService;

            GetData();
        }

        private void GetData()
        {
            List<Movement> movements;
            if (!_monthsVisible)
            {
                movements = _repositoryService.GetByYear(_selectedYear).ToList();
            }
            else
            {
                movements = _repositoryService.GetByMonth(GetMonth(_selectedMonth), _selectedYear).ToList();
            }
            TransformToChartData(movements, !_monthsVisible);
        }

        private void TransformToChartData(IList<Movement> movements, bool byYear)
        {
            if (movements.IsNullOrEmpty())
                return;

            _serieIncome.Clear();
            _serieSpend.Clear();            
            
            if (byYear)
            {
                for (int i =1; i <=12; i++)
                {
                    var income = movements.Where(m => m.IsPositive && m.Date.Month == i).Sum(m => (decimal)m.Amount/100);
                    _serieIncome.Add(new Tuple<string, decimal>(GetMonthShort(i), income));
                    var spend = movements.Where(m => !m.IsPositive && m.Date.Month == i).Sum(m => (decimal)m.Amount/100);
                    _serieSpend.Add(new Tuple<string, decimal>(GetMonthShort(i), spend));

                }
            }
            else
            {
                var daysOfMonth = DateTime.DaysInMonth(_selectedYear, GetMonth(_selectedMonth));
                for (int i = 1; i <= daysOfMonth; i++)
                {
                    var income = movements.Where(m => m.IsPositive && m.Date.Day == i).Sum(m => (decimal)m.Amount/100);
                    _serieIncome.Add(new Tuple<string, decimal>(i.ToString(), income));
                    var spend = movements.Where(m => !m.IsPositive && m.Date.Day == i).Sum(m => (decimal)m.Amount/100);
                    _serieSpend.Add(new Tuple<string, decimal>(i.ToString(), spend));

                }
            }

            if (!_chartData.IsNullOrEmpty())
                ChartData.Clear();            
            ChartData = new ObservableCollection<List<Tuple<string, decimal>>>()
            {
                _serieIncome, _serieSpend
            };
        }
    }
}
