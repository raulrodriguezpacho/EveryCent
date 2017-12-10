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
    public class ListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMovementRepository _repositoryService;

        private ICommand _goToMovementCommand;
        public ICommand GoToMovementCommand
        {
            get
            {
                return _goToMovementCommand ?? (_goToMovementCommand = new Command(() =>
                {
                    _navigationService.NavigateToAsync<MovementViewModel>(null);
                }));
            }
        }

        public ListViewModel(
            INavigationService navigationService,
            IMovementRepository repositoryService        
            )
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;
        }
    }
}
