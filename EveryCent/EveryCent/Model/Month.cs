using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Model
{
    public class Month
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public IList<Day> Days { get; set; }
    }
}
