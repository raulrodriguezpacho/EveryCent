using EveryCent.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services
{
    public class MovementRepository : Repository<Movement>, IMovementRepository
    {
        public IList<Movement> GetAll()
        {
            return null;
        }        
    }
}
