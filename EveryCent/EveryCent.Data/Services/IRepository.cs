using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Data.Services
{
    public interface IRepository<T>
    {
        Task<List<T>> Get();
        Task<T> Get(int id);
        Task<int> Insert(T entity);
        Task<int> InsertAll(List<T> enities);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        Task<int> Clear(string tableName);
        Task<bool> HasData();
    }
}
