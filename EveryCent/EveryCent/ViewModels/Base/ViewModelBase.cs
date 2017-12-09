using EveryCent.Base;
using EveryCent.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
    }
}
