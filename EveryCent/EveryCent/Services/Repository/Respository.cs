using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Services
{
    public class Repository<T> : IRepository<T> where T : new()
    {
        protected readonly SQLiteAsyncConnection db;

        public Repository(SQLiteAsyncConnection db)
        {
            this.db = db;
        }

        public async Task<List<T>> Get() => await db.Table<T>().ToListAsync();

        public async Task<T> Get(int id) => await db.FindAsync<T>(id);

        public async Task<int> Insert(T entity) => await db.InsertAsync(entity);

        public async Task<int> InsertAll(List<T> entities) => await db.InsertAllAsync(entities);

        public async Task<int> Update(T entity) => await db.UpdateAsync(entity);

        public async Task<int> Delete(T entity) => await db.DeleteAsync(entity);

        public async Task<int> Clear(string tableName) => await db.ExecuteAsync("delete from " + tableName);

        public async Task<bool> HasData() => await db.Table<T>().CountAsync() > 0;
    }
}
