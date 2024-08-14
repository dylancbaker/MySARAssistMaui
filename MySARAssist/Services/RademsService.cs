using MySarAssistModels.Interfaces;
using MySarAssistModels.RADeMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Services
{
    public class RademsService : IDataStore<RADeMSScore>
    {
        public Task<bool> AddItemAsync(RADeMSScore item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RADeMSScore> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RADeMSScore>> GetItems(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(RADeMSScore item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpsertItemAsync(RADeMSScore item)
        {
            throw new NotImplementedException();
        }
    }
}
