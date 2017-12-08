using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Data.Model
{
    public class Movement : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int Cost { get; set; } // x100

    }
}
