using EveryCent.Data.Model;
using EveryCent.Data.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Data
{
    public class MovementRepository : Repository<Movement>
    {
        public MovementRepository(SQLiteAsyncConnection db) : base(db) { }


    }
}
