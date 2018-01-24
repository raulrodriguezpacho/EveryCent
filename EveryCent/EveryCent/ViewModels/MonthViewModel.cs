using EveryCent.Services;
using EveryCent.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryCent.Helpers;
using System.Collections.ObjectModel;
using EveryCent.Model;
using System.Windows.Input;
using Xamarin.Forms;

namespace EveryCent.ViewModels
{
    public class MonthViewModel : ViewModelBase, IDateViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMovementRepository _repositoryService;
        private readonly IDeviceService _deviceService;

        private DateTime _now = DateTime.Now;

        private ObservableCollection<Month> _movementsMonths = new ObservableCollection<Month>();
        public ObservableCollection<Month> MovementsMonths
        {
            get { return _movementsMonths; }
            set
            {
                _movementsMonths = value;
                OnPropertyChanged("MovementsMonths");
            }
        }

        private string _selectedMonth = DateTimeFormatInfo.CurrentInfo.MonthNames[DateTime.Now.Month - 1];
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    OnPropertyChanged("SelectedMonth");
                    if (_position != Months.IndexOf(_selectedMonth))                    
                        Position = Months.IndexOf(_selectedMonth);                                        
                }                
            }
        }

        private int _selectedYear = DateTime.Now.Year;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged("SelectedYear");
                    ShowMonthMovements(GetMonth(_selectedMonth), value);
                }                
            }
        }

        private int _position;
        public int Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        private double _sizeCalendarDay = 50;
        public double SizeCalendarDay
        {
            get { return _sizeCalendarDay; }
            set
            {
                _sizeCalendarDay = value;
                OnPropertyChanged("SizeCalendarDay");
            }
        }

        private double _heightCalendar = 6 * 50;
        public double HeightCalendar
        {
            get { return _heightCalendar; }
            set
            {
                _heightCalendar = value;
                OnPropertyChanged("HeightCalendar");
            }
        }

        private double _startPositionX = 0;
        public double StartPositionX
        {
            get { return _startPositionX; }
            set
            {
                _startPositionX = value;
                OnPropertyChanged("StartPositionX");
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private string[] _dayLetters = new string[7];
        public string[] DayLetters
        {
            get { return _dayLetters; }
            set
            {
                _dayLetters = value;
                OnPropertyChanged("DayLetters");
            }
        }

        private Balance _balance;
        public Balance Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public MonthViewModel(
            INavigationService navigationService,
            IMovementRepository repositoryService,
            IDeviceService deviceService
            )
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;
            _deviceService = deviceService;

            ShowDayLetters();
            ShowMonthMovements(GetMonth(_selectedMonth), _selectedYear);
        }

        private void ShowDayLetters()
        {
            if (!DayLetters.IsNullOrEmpty())                
            for (int i = 1; i <= 7; i++)
            {
                DayLetters[i - 1] = DateTimeFormatInfo.CurrentInfo.ShortestDayNames[i - 1];
            }
        }

        private async void ShowMonthMovements(int month, int year)
        {
            IsLoading = true;            
            try
            {
                await CreateMonths(year);                
            }
            catch { }
            finally
            {
                IsLoading = false;
            }
        }

        private int BlankDays(DayOfWeek firstDayOfMonth)
        {
            int daysPass = 0;
            switch (firstDayOfMonth)
            {
                case DayOfWeek.Monday:
                    daysPass = 0;
                    break;
                case DayOfWeek.Tuesday:
                    daysPass = 1;
                    break;
                case DayOfWeek.Wednesday:
                    daysPass = 2;
                    break;
                case DayOfWeek.Thursday:
                    daysPass = 3;
                    break;
                case DayOfWeek.Friday:
                    daysPass = 4;
                    break;
                case DayOfWeek.Saturday:
                    daysPass = 5;
                    break;
                case DayOfWeek.Sunday:
                    daysPass = 6;
                    break;
            }
            return daysPass;
        }

        private async Task<bool> CreateMonths(int year)
        {
            bool ret = false;
            var auxSelectedMonth = _selectedMonth;
            var auxSelectedPosition = _position;
            if (!MovementsMonths.IsNullOrEmpty())
                MovementsMonths.Clear();
            for (int i = 1; i <= 12; i++)
            {
                MovementsMonths.Add(new Month()
                {
                    Number = i,
                    Name = DateTimeFormatInfo.CurrentInfo.MonthNames[i - 1],
                    Days = new ObservableCollection<Day>(await CreateMonthCalendar(i, year))
                });
            }
            SelectedMonth = auxSelectedMonth;
            Position = auxSelectedPosition;
            ret = true;
            return ret;
        }

        private async Task<List<Day>> CreateMonthCalendar(int month, int year)
        {
            List<Day> days = new List<Day>();
            try
            {
                DateTime firstDayOfMonth = new DateTime(year, month, 1).FirstDayOfMonth(month, year);
                DayOfWeek diaSemana = firstDayOfMonth.DayOfWeek;
                int monthDays = DateTime.DaysInMonth(year, month);
                
                var movements = _repositoryService.GetByMonth(GetMonth(_selectedMonth), _selectedYear);

                var income = movements.Where(m => m.IsPositive).Sum(m => (decimal)m.Amount / 100);
                var spend = movements.Where(m => !m.IsPositive).Sum(m => (decimal)m.Amount / 100);
                Balance = new Balance()
                {
                    Income = income,
                    Spend = spend,
                    IsPositive = income - spend > 0
                };                

                for (int i = 0; i < BlankDays(diaSemana); i++)
                {
                    days.Add(
                        new Day() { Number = 0, WeekDay = "", Month = month, Year = year, IsNegative = false, IsPositive = false }
                        );
                }
                for (int i = 1; i <= monthDays; i++)
                {
                    days.Add(
                        new Day()
                        {
                            Number = i,
                            WeekDay = string.Format("{0:ddd}", new DateTime(year, month, i)),
                            Month = month,
                            Year = year,
                            IsPositive =  movements.Any(m => m.IsPositive && m.Date.Day == i),
                            IsNegative = movements.Any(m => !m.IsPositive && m.Date.Day == i)
                        });
                }
            }
            catch { }
            return days;
        }        
    }       
}
