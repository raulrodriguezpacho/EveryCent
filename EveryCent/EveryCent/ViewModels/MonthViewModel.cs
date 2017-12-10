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

        private int PasarDias(DayOfWeek caediaunodemes)
        {
            int diaspasan = 0;
            switch (caediaunodemes)
            {
                case DayOfWeek.Monday:
                    diaspasan = 0;
                    break;
                case DayOfWeek.Tuesday:
                    diaspasan = 1;
                    break;
                case DayOfWeek.Wednesday:
                    diaspasan = 2;
                    break;
                case DayOfWeek.Thursday:
                    diaspasan = 3;
                    break;
                case DayOfWeek.Friday:
                    diaspasan = 4;
                    break;
                case DayOfWeek.Saturday:
                    diaspasan = 5;
                    break;
                case DayOfWeek.Sunday:
                    diaspasan = 6;
                    break;
            }
            return diaspasan;
        }
    }
}
