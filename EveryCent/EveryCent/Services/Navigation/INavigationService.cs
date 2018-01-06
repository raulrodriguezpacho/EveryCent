using EveryCent.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EveryCent.Services
{
    public interface INavigationService
    {
        Page NavigationPage { get; }

        object NavigationData { get; }

        Task NavigateBackAsync();

        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;        
    }
}
