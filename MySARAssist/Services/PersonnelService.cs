using MySARAssist.Interfaces;
using MySARAssist.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Services
{
    public class PersonnelService : IDataStore<Personnel>
    {
        private string dbPath = string.Empty;
        SQLiteAsyncConnection conn;

        public PersonnelService()
        {
            conn = new SQLiteAsyncConnection(dbPath);
        }

        public async Task<bool> AddItemAsync(Personnel item)
        {
            int check = await conn.InsertAsync(item);
            if (check > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
           
            await conn.CreateTableAsync<Personnel>();
            Personnel itemToDelete = await conn.Table<Personnel>().FirstOrDefaultAsync(o => o.PersonID == id);

            if (itemToDelete == null) { return false; }
            else
            {
                await conn.DeleteAsync(itemToDelete);
                return true;
            }
        }

        public async Task<Personnel> GetItemAsync(Guid id)
        {
            return await conn.Table<Personnel>().FirstOrDefaultAsync(o => o.PersonID == id);
        }

        public async Task<IEnumerable<Personnel>> GetItems(bool forceRefresh = false)
        {
            List<Personnel> list = await conn.Table<Personnel>().ToListAsync(); 
            list = list.OrderBy(o=>o.Name).ToList();
            return list;
        }

        public async Task<bool> UpdateItemAsync(Personnel item)
        {
            int check = await conn.UpdateAsync(item);
            if (check > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> UpsertItemAsync(Personnel item)
        {
            await conn.CreateTableAsync<Personnel>();
            bool exists = await conn.Table<Personnel>().CountAsync(o => o.PersonID == item.PersonID) > 0;
            if (exists)
            {
                return await UpdateItemAsync(item);
            }
            else
            {
                return await AddItemAsync(item);
            }
        }
    }
}
