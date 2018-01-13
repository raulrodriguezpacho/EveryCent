using EveryCent.Services;
using EveryCent.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EveryCent.ViewModels
{
    public class MovementViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMovementRepository _repositoryService;

        private bool _isPositive;
        public bool IsPositive
        {
            get { return _isPositive; }
            set
            {
                _isPositive = value;
                OnPropertyChanged("IsPositive");
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }        


        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private ICommand _backToDashboardCommand;
        public ICommand BackToDashboardCommand
        {
            get
            {
                return _backToDashboardCommand ?? (_backToDashboardCommand = new Command(() =>
                {
                    _navigationService.NavigateBackAsync();
                }));
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new Command(async() =>
                {                    
                    var result = await _repositoryService.InsertAsync(new Model.Movement()
                    {
                        Amount = (int)(Amount * 100),
                        Date = DateTime.Now,
                        Positive = IsPositive,
                        Description = Description
                    });
                    base.ShowAlert("save", result.ToString(), "ok");
                }));
            }
        }

        public MovementViewModel(
            INavigationService navigationService,
            IMovementRepository repositoryService
            )
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;

            IsPositive = true;
            if (_navigationService.NavigationData != null)
            {
                Date = new DateTime(
                    ((Tuple<int, int>)_navigationService.NavigationData).Item2,
                    ((Tuple<int, int>)_navigationService.NavigationData).Item1,
                    1);
            }
        }
    }
}
