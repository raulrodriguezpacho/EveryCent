using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using EveryCent.Model;

namespace EveryCent.TemplatesSelector
{
    public class CalendarDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DayZeroTemplate { get; set; }
        public DataTemplate DayNoneTemplate { get; set; }
        public DataTemplate DayOnlyPositiveTemplate { get; set; }
        public DataTemplate DayOnlyNegativeTemplate { get; set; }
        public DataTemplate DayPositiveAndNegativeTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate template = DayZeroTemplate;
            if (item is Day)
            {
                var day = (Day)item;
                if (day.Number == 0)
                {
                    template = DayZeroTemplate;
                }
                else
                {
                    template = DayNoneTemplate;
                    if (day.IsNegative && day.IsNegative)
                    {
                        template = DayPositiveAndNegativeTemplate;
                    }
                    else
                    {
                        if (day.IsPositive)
                        {
                            template = DayOnlyPositiveTemplate;
                        }
                        else if (day.IsNegative)
                        {
                            template = DayOnlyNegativeTemplate;
                        }
                    }
                }
            }
            return template;
        }
    }
}
