using MySarAssistModels.Clues;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Services
{


    public class ClueService
    {
        SQLiteAsyncConnection conn;

        public ClueService()
        {
            conn = new SQLiteAsyncConnection(Constants.dbPath);
        }
        public async Task<bool> AddItemAsync(Clue item)
        {
            await conn.CreateTableAsync<Clue>();
            int check = await conn.InsertAsync(item);
            if (check > 0) { return true; }
            else { throw new Exception("Error adding item"); }// return false; }
        }
        public async Task<bool> DeleteItemAsync(Guid id)
        {
            await conn.CreateTableAsync<Clue>();
            Clue itemToDelete = await conn.Table<Clue>().FirstOrDefaultAsync(o => o.ClueID == id);
            if (itemToDelete == null) { return false; }
            else
            {
                await conn.DeleteAsync(itemToDelete);
                return true;
            }
        }
        public async Task<IEnumerable<Clue>?> GetItemsAsync(bool forceRefresh = false)
        {
            await conn.CreateTableAsync<Clue>();
            List<Clue>? items = await conn.Table<Clue>().ToListAsync();
            return items;
        }
        public async Task<Clue?> GetItemAsync(Guid id)
        {
            await conn.CreateTableAsync<Clue>();
            Clue? item = await conn.Table<Clue>().FirstOrDefaultAsync(o => o.ClueID == id);
            return item;
        }
        public async Task<bool> UpdateItemAsync(Clue item)
        {
            await conn.CreateTableAsync<Clue>();
            int check = await conn.UpdateAsync(item);
            if (check > 0) { return true; }
            else { throw new Exception("Error updating item"); }// return false;  
        }
    }
}
