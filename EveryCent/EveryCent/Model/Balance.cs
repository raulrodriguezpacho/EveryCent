using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Model
{
    public class Balance
    {
        public decimal Income { get; set; }
        public decimal Spend { get; set; }
        public bool IsPositive { get; set; }
    }
}
