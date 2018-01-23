using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryCent.Model;

namespace EveryCent.Services
{
    public class MovementMockRepository : IMovementRepository
    {
        public Task<int> ClearAsync(string tableName)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Movement entity)
        {
            throw new NotImplementedException();
        }

        public IList<Movement> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Movement>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movement> GetAsync(int id)
        {
            Random r = new Random();            
            return Task.Run(() => new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(2018, 1, 1), Description = "movement 1", IsPositive = true });
        }

        public IList<Movement> GetBetweenMonths(int startMonth, int startYear, int endMonth, int endYear)
        {
            throw new NotImplementedException();
        }

        public IList<Movement> GetByDay(int year, int month, int day)
        {
            Random r = new Random();

            List<Movement> movements = new List<Movement>();
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, day), Description = "movement 1", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, day), Description = "movement 2", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, day), Description = "movement 3", IsPositive = false });

            return movements;
        }

        public IList<Movement> GetByMonth(int month, int year)
        {
            Random r = new Random();
            
            List<Movement> movements = new List<Movement>();
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 1), Description = "movement 1 iweufiwu fkjw jfn knfkjsdnjnsdkfjn ksfn ksdjf ksjf kjsdn kjnsfkns kdfjns kdjf ksnf ksjn", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 2), Description = "movement 2", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 3), Description = "movement 3", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 4), Description = "movement 4", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 4), Description = "movement 5", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 6), Description = "movement 6", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 7), Description = "movement 7", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 8), Description = "movement 8", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 8), Description = "movement 9", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 8), Description = "movement 10", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 12), Description = "movement 11", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 14), Description = "movement 12", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 20), Description = "movement 13", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 23), Description = "movement 14", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 23), Description = "movement 15", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 24), Description = "movement 16", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 25), Description = "movement 17", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 27), Description = "movement 18", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 28), Description = "movement 19", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 28), Description = "movement 20", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, month, 28), Description = "movement 21", IsPositive = false });
            if (month != 2)
                movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 30), Description = "movement 23", IsPositive = true });            

            return movements;
        }

        public Task<IList<Movement>> GetByMonthAsync(int month, int year)
        {
            throw new NotImplementedException();
        }

        public IList<Movement> GetByYear(int year)
        {
            Random r = new Random();
            List<Movement> movements = new List<Movement>();
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 1), Description = "movement 1", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 2), Description = "movement 2", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 3), Description = "movement 3", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 4), Description = "movement 4", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 4), Description = "movement 5", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 6), Description = "movement 6", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 7), Description = "movement 7", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 8), Description = "movement 8", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 8), Description = "movement 9", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 8), Description = "movement 10", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 12), Description = "movement 11", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 14), Description = "movement 12", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 20), Description = "movement 13", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 23), Description = "movement 14", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 23), Description = "movement 15", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 24), Description = "movement 16", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 25), Description = "movement 17", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 27), Description = "movement 18", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 28), Description = "movement 19", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 28), Description = "movement 20", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 28), Description = "movement 21", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 29), Description = "movement 22", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 1, 30), Description = "movement 23", IsPositive = true });

            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 1), Description = "movement 1", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 2), Description = "movement 2", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 3), Description = "movement 3", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 4), Description = "movement 4", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 4), Description = "movement 5", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 6), Description = "movement 6", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 7), Description = "movement 7", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 8), Description = "movement 8", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 8), Description = "movement 9", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 8), Description = "movement 10", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 12), Description = "movement 11", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 14), Description = "movement 12", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 20), Description = "movement 13", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 23), Description = "movement 14", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 23), Description = "movement 15", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 24), Description = "movement 16", IsPositive = true });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 25), Description = "movement 17", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 27), Description = "movement 18", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 28), Description = "movement 19", IsPositive = false });
            movements.Add(new Movement() { Amount = r.Next(0, 50000), Date = new DateTime(year, 2, 28), Description = "movement 20", IsPositive = true });

            return movements;
        }

        public Task<bool> HasDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAllAsync(List<Movement> enities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Movement entity)
        {
            return Task.Run(() => 1);
        }

        public Task<int> UpdateAsync(Movement entity)
        {
            throw new NotImplementedException();
        }
    }
}
