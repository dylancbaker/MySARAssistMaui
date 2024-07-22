using MySARAssist.Interfaces;

/* Unmerged change from project 'MySARAssist (net8.0-ios)'
Before:
using SQLite;
After:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using MySARAssist.Models.Personnel.Personnel.Personnel;
using SQLite;
*/

/* Unmerged change from project 'MySARAssist (net8.0-android)'
Before:
using SQLite;
After:
using MySARAssist.Models.Personnel;
using MySARAssist.Models.Personnel.Personnel;
using SQLite;
*/

/* Unmerged change from project 'MySARAssist (net8.0-windows10.0.19041.0)'
Before:
using SQLite;
After:
using MySARAssist.Models.Personnel;
using SQLite;
*/
using MySARAssist.Models.People;
using MySARAssist.Views.CheckInOut;
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
            await conn.CreateTableAsync<Personnel>();

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

        public async Task<Organization?> GetMostFrequentOrganizationAsync()
        {
            Organization mostPopularOrg = null;

            List<Organization> allOrgs = OrganizationTools.GetOrganizations(Guid.Empty);
            var _AllTeamMembers = await GetItems();

            foreach (Organization org in allOrgs)
            {
                org.UserCount = _AllTeamMembers.Count(o => o.OrganizationID == org.OrganizationID);
            }
            if (allOrgs.Any(o => o.UserCount > 0))
            {
                mostPopularOrg = allOrgs.OrderByDescending(o => o.UserCount).First();
            }

            if(mostPopularOrg == null)
            {
                //when in doubt, use the Unassigned organization
                Guid UnassignedOrg = new Guid("96BA69A4-436C-4DA1-85B1-992E84C36019");
                if (allOrgs.Any(o => o.OrganizationID == UnassignedOrg))
                {
                    mostPopularOrg = allOrgs.First(o => o.OrganizationID == UnassignedOrg);
                }
            }

            return mostPopularOrg;
        }

        public async Task<Personnel?> GetCurrentPersonAsync()
        {
            string selectedID = Preferences.Get("SelectedPersonID", string.Empty);
            if (!string.IsNullOrEmpty(selectedID))
            {
                Guid SelectedPersonID = new Guid(selectedID);

                if (SelectedPersonID != Guid.Empty)
                {
                    return await GetItemAsync(SelectedPersonID);
                }
            }
            return null;
        }

        public void setCurrentPerson(Guid selected_memberID)
        {
            Preferences.Set("SelectedPersonID", selected_memberID.ToString());

        }
    }
}
