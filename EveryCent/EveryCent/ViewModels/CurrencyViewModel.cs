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
    public class CurrencyViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;        

        public string[] Currencies
        {
            get
            {
                return App.Currency;
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

        private ICommand _cancelPropertyCommand;
        public ICommand CancelPropertyCommand
        {
            get
            {
                return _cancelPropertyCommand ?? (_cancelPropertyCommand = new Command(() =>
                {
                    _navigationService.NavigateBackAsync();
                }));
            }
        }

        private ICommand _savePropertyCommand;
        public ICommand SavePropertyCommand
        {
            get
            {
                return _savePropertyCommand ?? (_savePropertyCommand = new Command(() =>
                {
                    Application.Current.Properties["Currency"] = Currencies[_position];
                    Application.Current.SavePropertiesAsync();
                    MessagingCenter.Send<ViewModelBase, string>(this, "currency", Application.Current.Properties["Currency"].ToString());
                    _navigationService.NavigateBackAsync();
                }));
            }
        }

        public CurrencyViewModel(
            INavigationService navigationService            
            )
        {
            _navigationService = navigationService;                        
        }
    }
}
