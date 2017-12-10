using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryCent.ViewModels.Base;
using Xamarin.Forms;

namespace EveryCent.Services
{
    public class NavigationService : INavigationService
    {
        public Page NavigationPage => Application.Current.MainPage;

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {            
            return this.NavigationPage.Navigation.PushModalAsync(GetPageFromViewModel(typeof(TViewModel)));
        }

        public Task NavigateBackAsync()
        {
            return this.NavigationPage.Navigation.PopModalAsync();
        }

        private Page GetPageFromViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace(".ViewModels.", ".Views.").Replace("ViewModel", "Page");
            var viewType = Type.GetType(viewName);            
            return (Page)Activator.CreateInstance(viewType);
        }
    }
}
