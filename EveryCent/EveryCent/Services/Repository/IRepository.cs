using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<int> InsertAsync(T entity);
        Task<int> InsertAllAsync(List<T> enities);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<int> ClearAsync(string tableName);
        Task<bool> HasDataAsync();
    }
}
