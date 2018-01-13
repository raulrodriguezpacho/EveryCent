using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EveryCent.Validations
{
    public class IsNotAmountValidation<T> : IValidate<T>
    {
        public string Message { get; set; }

        public bool Validate(T value)
        {
            Regex regex = new Regex(@"^\d+(.\d{1,2})?$");
            Match match = regex.Match(value.ToString());
            if (!match.Success)
                return false;
            decimal amount = 0;
            if (!decimal.TryParse(value.ToString(), out amount))
                return false;
            return true;
        }
    }
}
