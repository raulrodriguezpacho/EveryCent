using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Validations
{
    public class IsNotNullOrEmptyValidation<T> : IValidate<T>
    {
        public string Message { get; set; }

        public bool Validate(T value)
        {
            if (value == null)
                return false;            
            return !string.IsNullOrWhiteSpace(value.ToString());
        }
    }
}
