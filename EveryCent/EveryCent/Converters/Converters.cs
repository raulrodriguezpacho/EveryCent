using EveryCent.Base;
using EveryCent.Model;
using EveryCent.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EveryCent.Converters
{
    public class ItemTappedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as ItemTappedEventArgs;
            if (eventArgs == null)
                return null;
            return eventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            return (bool)value ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DecimalToTwoDecimalPlacesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            if (value is decimal)
            {
                return decimal.Parse(value.ToString()).ToString("N2");
            }            
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return decimal.Parse(value.ToString());
        }
    }

    public class IntlToTwoDecimalPlacesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {                        
            decimal amount = (decimal)int.Parse(value.ToString()) / 100;
            return 
                (parameter.ToString() == "N" ? "-" : "+") +
                decimal.Parse(amount.ToString()).ToString("N2") + " " + 
                (Application.Current.Properties.ContainsKey("Currency") ? Application.Current.Properties["Currency"].ToString() : "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return decimal.Parse(value.ToString());
        }
    }

    public class DateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            return ((DateTime)value).ToString("d", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DateToDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).Day.ToString("d", culture) + " " + culture.DateTimeFormat.GetAbbreviatedDayName(((DateTime)value).DayOfWeek);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DayToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var day = ((Day)value);
            var dateTime = new DateTime(day.Year, day.Month, day.Number);
            return culture.DateTimeFormat.GetAbbreviatedDayName((dateTime).DayOfWeek) + " " +
                ((Day)value).Number.ToString() + " " +                 
                culture.DateTimeFormat.GetAbbreviatedMonthName(day.Month) + " " +
                day.Year.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class BalanceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var balance = ((Balance)value).Income - ((Balance)value).Spend;
            var sign = "";
            if (balance > 0)
                sign = "+ ";
            else if (balance < 0)
                sign = "";
            return sign + decimal.Parse((((Balance)value).Income - ((Balance)value).Spend).ToString()).ToString("N2") + " " +
                (Application.Current.Properties.ContainsKey("Currency") ? Application.Current.Properties["Currency"].ToString() : "");   
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class BalanceToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value ? Color.Green : Color.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var datetime = ((DateTime)value);            
            return
                culture.DateTimeFormat.GetAbbreviatedMonthName(datetime.Month) + " " +
                datetime.Year.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DescriptionToCharsLeftConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("[{0}]", int.Parse(parameter.ToString()) - (value == null ? 0 : value?.ToString().Length));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class IsPositiveToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Resources.AppResources.Income;
            else
                return Resources.AppResources.Spend;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class IsPositiveToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Color.Green;
            else
                return Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
