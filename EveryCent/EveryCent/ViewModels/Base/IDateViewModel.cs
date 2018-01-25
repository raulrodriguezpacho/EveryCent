using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.ViewModels.Base
{
    public interface IDateViewModel
    {
        string SelectedMonth { get; set; }
        int SelectedYear { get; set; }
        bool MonthsVisible { get; }
    }
}
