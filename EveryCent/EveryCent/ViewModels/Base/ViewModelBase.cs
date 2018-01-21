using EveryCent.Base;
using EveryCent.Helpers;
using EveryCent.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EveryCent.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly INavigationService NavigationService;        

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase()
        {            
            NavigationService = LocatorBase.Resolve<INavigationService>();
        }

        public async void ShowAlert(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title ?? string.Empty, message, cancel);
        }

        public async Task<bool> ShowToDo(string title, string message, string accept, string cancel)
        {
            return await App.Current.MainPage.DisplayAlert(title ?? string.Empty, message, accept, cancel);
        }

        private ObservableCollection<string> _months = new ObservableCollection<string>();
        public ObservableCollection<string> Months
        {
            get
            {
                if (_months.IsNullOrEmpty())
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        _months.Add(DateTimeFormatInfo.CurrentInfo.MonthNames[i - 1]);
                    }
                }
                return _months;
            }
        }

        private ObservableCollection<int> _years = new ObservableCollection<int>();
        public ObservableCollection<int> Years
        {
            get
            {
                if (_years.IsNullOrEmpty())
                {
                    for (int i = 2013; i <= DateTime.Now.Year; i++)
                    {
                        _years.Add(i);
                    }
                }
                return _years;
            }
        }

        protected string GetMonth(int month)
        {
            return DateTimeFormatInfo.CurrentInfo.MonthNames[month - 1];
        }

        protected string GetMonthShort(int month)
        {
            return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
        }

        protected int GetMonth(string month)
        {
            return Convert.ToDateTime("01-" + month + "-2018").Month;
        }        
    }
}
