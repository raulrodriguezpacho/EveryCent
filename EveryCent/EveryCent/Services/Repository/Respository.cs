using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services
{
    public abstract class Repository<T> : IRepository<T> where T : new()
    {
        protected readonly SQLiteAsyncConnection _connection;

        public Repository()
        {
            this._connection = App.Database.Connection;
        }

        public virtual async Task<List<T>> GetAsync() => await _connection.Table<T>().ToListAsync();

        public virtual async Task<T> GetAsync(int id) => await _connection.FindAsync<T>(id);

        public virtual async Task<int> InsertAsync(T entity) => await _connection.InsertAsync(entity);

        public virtual async Task<int> InsertAllAsync(List<T> entities) => await _connection.InsertAllAsync(entities);

        public virtual async Task<int> UpdateAsync(T entity) => await _connection.UpdateAsync(entity);

        public virtual async Task<int> DeleteAsync(T entity) => await _connection.DeleteAsync(entity);

        public virtual async Task<int> ClearAsync(string tableName) => await _connection.ExecuteAsync("delete from " + tableName);

        public virtual async Task<bool> HasDataAsync() => await _connection.Table<T>().CountAsync() > 0;
    }
}
