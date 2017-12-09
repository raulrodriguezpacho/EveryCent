using EveryCent.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryCent.Services;

namespace EveryCent.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMovementRepository _repositoryService;

        public ChartViewModel(
            INavigationService navigationService,
            IMovementRepository repositoryService)
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;

            //var temp = _repositoryService.GetAll();
        }        
    }
}
