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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Movement entity)
        {
            throw new NotImplementedException();
        }
    }
}
