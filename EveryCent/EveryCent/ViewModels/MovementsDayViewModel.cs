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
                    _day = (Day)_navigationService.NavigationData;                    
                    GetMovements(_day.Month, _day.Year);
                }
            }
        }

        private async void GetMovements(int month, int year)
        {
            Movements = new ObservableCollection<Movement>(await _repositoryService.GetByMonthAsync(month, year));
        }
    }
}
