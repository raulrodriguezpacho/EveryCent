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
        private readonly IDeviceService _deviceService;

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

        private double _heightCoin = 0;
        public double HeightCoin
        {
            get { return _heightCoin; }
            set
            {
                _heightCoin = value;
                OnPropertyChanged("HeightCoin");
            }
        }

        private double _widthtCoin = 0;
        public double WidthCoin
        {
            get { return _widthtCoin; }
            set
            {
                _widthtCoin = value;
                OnPropertyChanged("WidthCoin");
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
            INavigationService navigationService,
            IDeviceService deviceService
            )
        {
            _navigationService = navigationService;
            _deviceService = deviceService;

            var size = _deviceService.GetDeviceSize();
            WidthCoin = HeightCoin = size.Width - size.Width * 5 / 10;
        }
    }
}
