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

            Temp();
        }     
        
        async void Temp()
        {
            await _repositoryService.InsertAsync(new Model.Movement()
            {
                Cost = DateTime.Now.Second, Date = DateTime.Now
            });

            var temp = await _repositoryService.GetAsync();
            
        }
    }
}
