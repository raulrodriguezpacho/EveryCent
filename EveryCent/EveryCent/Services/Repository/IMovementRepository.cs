using EveryCent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services
{
    public interface IMovementRepository : IRepository<Movement>
    {
        IList<Movement> GetAll();
        IList<Movement> GetByMonth(int month, int year);
        Task<IList<Movement>> GetByMonthAsync(int month, int year);
        IList<Movement> GetBetweenMonths(int startMonth, int startYear, int endMonth, int endYear);
        IList<Movement> GetByYear(int year);
        IList<Movement> GetByDay(int year, int month, int day);
    }
}
