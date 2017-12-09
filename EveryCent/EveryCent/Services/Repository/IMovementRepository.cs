using EveryCent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services
{
    public interface IMovementRepository
    {
        IList<Movement> GetAll();
    }
}
