using EveryCent.Model;
using EveryCent.Services;
using EveryCent.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using EveryCent.Helpers;
using System.Globalization;

namespace EveryCent.ViewModels
{
    public class ListViewModel : ViewModelBase, IDateViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMovementRepository _repositoryService;

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }

        private ObservableCollection<Movement> _movements;
        public ObservableCollection<Movement> Movements
        {
            get
            {
                return GetMovements();    
            }
            set
            {
                _movements = value;
                OnPropertyChanged("Movements");
            }
        }

        public bool MonthsVisible
        {
            get { return true; }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        private string _selectedMonth = DateTimeFormatInfo.CurrentInfo.MonthNames[DateTime.Now.Month - 1];
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged("SelectedMonth");
                OnPropertyChanged("Movements");
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
                OnPropertyChanged("Movements");
            }
        }

        private Balance _balance = new Balance() { Income = 0, Spend = 0 };
        public Balance Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        private ICommand _goToMovementCommand;
        public ICommand GoToMovementCommand
        {
            get
            {
                return _goToMovementCommand ?? (_goToMovementCommand = new Command(() =>
                {
                    _navigationService.NavigateToAsync<MovementViewModel>(new Tuple<int, int>(GetMonth(_selectedMonth), _selectedYear));
                }));
            }
        }

        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new Command(() =>
                {
                    IsRefreshing = true;
                    SearchText = "";                    
                    OnPropertyChanged("Movements");
                    IsRefreshing = false;
                }));
            }
        }

        private ICommand _movementSelectedCommand;
        public ICommand MovementSelectedCommand
        {
            get
            {
                return _movementSelectedCommand ?? (_movementSelectedCommand = new Command((param) =>
                {                    
                    _navigationService.NavigateToAsync<MovementViewModel>(((Movement)param).ID);
                }));
            }
        }

        private ICommand _findMovementCommand;
        public ICommand FindMovementCommand
        {
            get
            {
                return _findMovementCommand ?? (_findMovementCommand = new Command(() =>
                {
                    OnPropertyChanged("Movements");
                }));
            }
        }

        public ListViewModel(
            INavigationService navigationService,
            IMovementRepository repositoryService        
            )
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;

            MessagingCenter.Subscribe<ViewModelBase, string>(this, "currency", (sender, arg) =>
            {
                CurrentCurrency = arg;
                RefreshCommand.Execute(null);
            });
        }        

        private ObservableCollection<Movement> GetMovements()
        {
            ObservableCollection<Movement> movements;
            if (string.IsNullOrEmpty(_searchText))
            {
                movements = _repositoryService.GetByMonth(GetMonth(_selectedMonth), _selectedYear).ToObservableCollection();

            }                
            else
            {
                movements = (from M in _repositoryService.GetByMonth(GetMonth(_selectedMonth), _selectedYear)
                    where M.Description.ToLower().Contains(_searchText.ToLower())
                    select M).ToObservableCollection();
            }

            var income = movements.Where(m => m.IsPositive).Sum(m => (decimal)m.Amount / 100);
            var spend = movements.Where(m => !m.IsPositive).Sum(m => (decimal)m.Amount / 100);
            Balance = new Balance()
            {
                Income = income,
                Spend = spend,
                IsPositive = income - spend > 0
            };

            return movements;
        }
    }
}
