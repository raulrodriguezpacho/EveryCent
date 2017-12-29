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
        private readonly IMovementRepository _repositoryService;

        public ChartViewModel(IMovementRepository repositoryService)            
        {            
            _repositoryService = repositoryService;

        }     
        
    }
}
