using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Validations
{
    public interface IValidate<T>
    {
        string Message { get; set; }
        bool Validate(T value);
    }
}
