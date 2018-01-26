using EveryCent.Base;
using EveryCent.Helpers;
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

        private string _amount;
        public string Amount
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

        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string AmountLabel
        {
            get
            {
                return Resources.AppResources.Amount +
                    (!Application.Current.Properties.ContainsKey("Currency") ?  "" :
                        " (" + Application.Current.Properties["Currency"].ToString() + ")");
            }
        }

        private string _incomeSpendLabel = Resources.AppResources.Income;
        public string IncomeSpendLabel
        {
            get { return _incomeSpendLabel; }
            set
            {
                _incomeSpendLabel = value;
                OnPropertyChanged("IncomeSpendLabel");
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

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new Command(async() =>
                {
                    var keyboardService = LocatorBase.Resolve<IKeyboardService>();
                    keyboardService.HideKeyboard();
                    int result = 0;
                    decimal amountResult = decimal.Zero;                    
                    var ret = decimal.TryParse(_amount, out amountResult);
                    Movement movement = new Movement()
                    {
                        Amount = (int)(amountResult * 100),
                        Date = Date,
                        IsPositive = IsPositive,
                        Description = Description
                    };
                    if (!ret)
                    {
                        ShowAlert(Resources.AppResources.MovementSavingTitle, Resources.AppResources.NotDecimalMsg, Resources.AppResources.Ok);
                        return;
                    }                    
                    if (amountResult == 0)
                    {
                        ShowAlert(Resources.AppResources.MovementSavingTitle, Resources.AppResources.AmountZeroMsg, Resources.AppResources.Ok);
                        return;
                    }
                    if (_movementID == 0)
                    {
                        result = await _repositoryService.InsertAsync(movement);
                        if (result == 1)
                        {                            
                            await _navigationService.NavigateBackAsync();
                        }
                        else
                            ShowAlert(Resources.AppResources.MovementSavingTitle, Resources.AppResources.MovementErrorSavingMsg, Resources.AppResources.Ok);
                    }
                    else
                    {
                        movement.ID = _movementID;
                        result = await _repositoryService.UpdateAsync(movement);
                        if (result == 1)
                        {                            
                            await _navigationService.NavigateBackAsync();
                        }
                        else
                            ShowAlert(Resources.AppResources.MovementSavingTitle, Resources.AppResources.MovementErrorSavingMsg, Resources.AppResources.Ok);
                    }
                    if (result == 1)
                        MessagingCenter.Send<Movement>(movement, "movement");
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

            IsPositive = false;            
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
                Amount = ((decimal)movement.Amount / 100).ToString();
                Description = movement.Description;
                _movementID = movement.ID;
            }
        }
    }
}
