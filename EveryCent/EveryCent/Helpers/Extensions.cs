using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Helpers
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return !(list?.Any() ?? false);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            var c = new ObservableCollection<T>();
            if (coll.IsNullOrEmpty())
                return c;
            foreach (var e in coll)
                c.Add(e);
            return c;
        }

        public static DateTime FirstDayOfMonth(this DateTime input, int month, int year)
        {
            return new DateTime(year, month, 1);
        }

        public static bool Between(this DateTime input, DateTime minDate, DateTime maxDate)
        {
            return input >= minDate && input <= maxDate;
        }
    }
}
