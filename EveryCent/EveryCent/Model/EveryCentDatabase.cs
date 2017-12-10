using EveryCent.Model;
using EveryCent.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryCent.Data
{
    public class EveryCentDatabase
    {
        readonly SQLiteAsyncConnection _connection;      
        public SQLiteAsyncConnection Connection
        {
            get { return _connection; }
        }

        public EveryCentDatabase(ILocalPath databasePath)
        {
            try
            {
                var pathToDatabase = databasePath.GetLocalFilePath("everycent.db3");
                _connection = new SQLiteAsyncConnection(pathToDatabase, false);
                _connection.CreateTableAsync<Movement>().Wait();                
            }
            catch (Exception) { }
        }
    }
}
