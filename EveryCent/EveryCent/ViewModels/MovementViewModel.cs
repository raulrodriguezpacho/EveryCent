using EveryCent.Model;
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

        private int _movementID = 0;

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

        private decimal _amount = 0;
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
                return _saveCommand ?? (_saveCommand = new Command(() =>
                {
                    if (_movementID == 0)
                    {
                        var result = _repositoryService.InsertAsync(new Model.Movement()
                        {
                            Amount = (int)(Amount * 100),
                            Date = Date,
                            IsPositive = IsPositive,
                            Description = Description
                        });
                        //ShowAlert("save", result.ToString(), "ok");
                    }
                    else
                    {
                        var result = _repositoryService.UpdateAsync(new Model.Movement()
                        {
                            ID = _movementID,
                            Amount = (int)(Amount * 100),
                            Date = Date,
                            IsPositive = IsPositive,
                            Description = Description
                        });
                        //ShowAlert("update", result.ToString(), "ok");
                    }
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
                if (_navigationService.NavigationData is int)
                {
                    GetMovement((int)_navigationService.NavigationData);
                }
                else if (_navigationService.NavigationData is Tuple<int, int>)
                {
                    Date = new DateTime(
                        ((Tuple<int, int>)_navigationService.NavigationData).Item2,
                        ((Tuple<int, int>)_navigationService.NavigationData).Item1,
                        1);
                } 
                else if (_navigationService.NavigationData is Tuple<int, int, int>)
                {
                    Date = new DateTime(
                        ((Tuple<int, int, int>)_navigationService.NavigationData).Item3,
                        ((Tuple<int, int, int>)_navigationService.NavigationData).Item2,
                        ((Tuple<int, int, int>)_navigationService.NavigationData).Item1);
                }
            }
        }

        private async void GetMovement(int id)
        {
            var movement =  await _repositoryService.GetAsync(id);
            if (movement != null)
            {
                IsPositive = movement.IsPositive;
                Date = movement.Date;
                Amount = (decimal)movement.Amount / 100;
                Description = movement.Description;
                _movementID = movement.ID;
            }
        }
    }
}
