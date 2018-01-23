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

        public IList<Movement> GetBetweenMonths(int startMonth, int startYear, int endMonth, int endYear)
        {
            throw new NotImplementedException();
        }

        public IList<Movement> GetByDay(int year, int month, int day)
        {
            var result = _connection.Table<Movement>().ToListAsync();
            return result.Result.Where(m => m.Date.Day == day && m.Date.Month == month && m.Date.Year == year).ToList();
        }

        public IList<Movement> GetByMonth(int month, int year)
        {
            var result = _connection.Table<Movement>().ToListAsync();
            return result.Result.Where(m => m.Date.Month == month && m.Date.Year == year).ToList();
        }

        public async Task<IList<Movement>> GetByMonthAsync(int month, int year)
        {
            //return await _connection.Table<Movement>().ToListAsync().Result.Where(m => m.Date.Month == month && m.Date.Year == year);
            var query = _connection.Table<Movement>().Where(m => m.Date.Month == month && m.Date.Year == year);
            await query.ToListAsync().ContinueWith(q =>
            {
                return q.Result;
            });
            return null;
        }

        public IList<Movement> GetByYear(int year)
        {
            var result = _connection.Table<Movement>().ToListAsync();
            return result.Result.Where(m => m.Date.Year == year).ToList();
        }
    }
}
