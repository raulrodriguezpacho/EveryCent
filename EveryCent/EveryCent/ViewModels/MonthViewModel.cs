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
                    ShowMonthMovements(GetMonth(value), _selectedYear);
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
                _position = value;
                OnPropertyChanged("Position");
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

        private double _heightCalendar = 200;
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

        private ICommand _viewMomementsDayCommand;
        public ICommand ViewMomementsDayCommand
        {
            get
            {
                return _viewMomementsDayCommand ?? (_viewMomementsDayCommand = new Command((param) =>
                {
                    // TODO: navigate to MovementsDayViewModel..
                }));
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

            ShowMonthMovements(GetMonth(_selectedMonth), _selectedYear);
        }

        private async void ShowMonthMovements(int month, int year)
        {
            IsLoading = true;            
            try
            {
                await CreateMonths(year);
                //if (!_movementsMonths.IsNullOrEmpty())
                //    Position = month - 1;
            }
            catch (Exception ex) { }
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
                
                // TODO: get movements data

                for (int i = 0; i < BlankDays(diaSemana); i++)
                {
                    days.Add(
                        new Day() { Number = 0, WeekDay = "" }
                        );
                }
                for (int i = 1; i <= monthDays; i++)
                {
                    days.Add(
                        new Day()
                        {
                            Number = i,
                            WeekDay = string.Format("{0:ddd}", new DateTime(year, month, i)),
                            
                        });
                }
            }
            catch (Exception ex) { }
            return days;
        }
    }       
}
