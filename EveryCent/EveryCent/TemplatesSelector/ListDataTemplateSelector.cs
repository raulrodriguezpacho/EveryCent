using EveryCent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EveryCent.TemplatesSelector
{
    public class ListDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PositiveTemplate { get; set; }
        public DataTemplate NegativeTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate template = PositiveTemplate;
            if (item is Movement)
            {                
                if (!((Movement)item).IsPositive)
                    template = NegativeTemplate;                
            }
            return template;
        }
    }
}
