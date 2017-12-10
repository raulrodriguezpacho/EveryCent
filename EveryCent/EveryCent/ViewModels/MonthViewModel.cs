using EveryCent.Services;
using EveryCent.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.ViewModels
{
    public class MonthViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMovementRepository _repositoryService;

        public MonthViewModel(
            INavigationService navigationService,
            IMovementRepository repositoryService
            )
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;
        }
    }
}
