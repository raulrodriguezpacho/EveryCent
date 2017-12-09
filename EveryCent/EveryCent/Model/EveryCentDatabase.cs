using EveryCent.Model;
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
        readonly SQLiteAsyncConnection database;

        public EveryCentDatabase(string dbPath)
        {
            try
            {
                database = new SQLiteAsyncConnection(dbPath, false);                

                database.CreateTableAsync<Movement>().Wait();

            }
            catch (Exception ex) { }

        }
    }
}
