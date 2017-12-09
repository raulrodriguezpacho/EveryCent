using EveryCent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services
{
    public class MovementRepository : IMovementRepository
    {
        public IList<Movement> GetAll()
        {
            List<Movement> temp = new List<Movement>();
            temp.Add(new Movement() { ID = 1, Cost = 33, Date = DateTime.Now });
            temp.Add(new Movement() { ID = 2, Cost = 22, Date = DateTime.Now });
            temp.Add(new Movement() { ID = 3, Cost = 11, Date = DateTime.Now });
            return temp;
        }
    }
}
