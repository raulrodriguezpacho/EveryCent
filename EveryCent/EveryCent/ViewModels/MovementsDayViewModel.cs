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

namespace EveryCent.ViewModels
{
    public class MovementsDayViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMovementRepository _repositoryService;

        private Day _day;
        public Day Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged("Day");
            }
        }

        private ObservableCollection<Movement> _movements;
        public ObservableCollection<Movement> Movements
        {
            get
            {
                return _movements;
            }
            set
            {
                _movements = value;
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
                    _navigationService.NavigateToAsync<MovementViewModel>(new Tuple<int, int, int>(_day.Number, _day.Month, _day.Year));
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

        private ICommand _cancelMovementCommand;
        public ICommand CancelMovementCommand
        {
            get
            {
                return _cancelMovementCommand ?? (_cancelMovementCommand = new Command(() =>
                {
                    _navigationService.NavigateBackAsync();
                }));
            }
        }

        public MovementsDayViewModel(
            INavigationService navigationService,
            IMovementRepository repositoryService
            )
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;
            if (_navigationService.NavigationData != null)
            {
                if (_navigationService.NavigationData is Day)
                {
                    Day = (Day)_navigationService.NavigationData;
                    GetData(_day);
                }
            }
        }

        private void GetData(Day day)
        {
            var movements = _repositoryService.GetByDay(_day.Year, day.Month, day.Number);
            Movements = new ObservableCollection<Movement>(movements);

            var income = movements.Where(m => m.IsPositive).Sum(m => (decimal)m.Amount / 100);
            var spend = movements.Where(m => !m.IsPositive).Sum(m => (decimal)m.Amount / 100);
            Balance = new Balance()
            {
                Income = income,
                Spend = spend,
                IsPositive = income - spend > 0
            };
        }        
    }
}
