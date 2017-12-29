using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EveryCent.TemplatesSelector
{
    public class CalendarDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DayZeroTemplate { get; set; }
        public DataTemplate DayOnlyPositiveTemplate { get; set; }
        public DataTemplate DayOnlyNegativeTemplate { get; set; }
        public DataTemplate DayPositiveAndNegativeTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate template = DayZeroTemplate;
            if (item is object) // TODO : is Day
            {

            }
            return template;
        }
    }
}
