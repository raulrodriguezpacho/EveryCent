using EveryCent.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryCent.Services;
using System.Globalization;

namespace EveryCent.ViewModels
{
    public class ChartViewModel : ViewModelBase, IDateViewModel
    {        
        private readonly IMovementRepository _repositoryService;

        private string _selectedMonth = DateTimeFormatInfo.CurrentInfo.MonthNames[DateTime.Now.Month - 1];
        public string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged("SelectedMonth");                
            }
        }

        private int _selectedYear = DateTime.Now.Year;
        public int SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                OnPropertyChanged("SelectedYear");                
            }
        }

        public ChartViewModel(IMovementRepository repositoryService)            
        {            
            _repositoryService = repositoryService;

        }

        
    }
}
