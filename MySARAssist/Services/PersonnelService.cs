

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

using MySARAssist.Models.Exceptions;
using MySARAssist.Views.CheckInOut;
using MySarAssistModels.Interfaces;
using MySarAssistModels.People;
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
        
        SQLiteAsyncConnection conn;

        public PersonnelService()
        {
            conn = new SQLiteAsyncConnection(Constants.dbPath);
        }

        public async Task<bool> AddItemAsync(Personnel item)
        {
            await conn.CreateTableAsync<Personnel>();

            int check = await conn.InsertAsync(item);
            if (check > 0) { return true; }
            else { throw new Exception("Error adding item"); }// return false; }
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
            Personnel p = await conn.Table<Personnel>().FirstOrDefaultAsync(o => o.PersonID == id);
            if(p!=null && p.OrganizationID != Guid.Empty)
            {
                p.MemberOrganization = await new OrganizationService().GetItemAsync(p.OrganizationID);//OrganizationTools.GetStaticOrganization(p.OrganizationID);
            }
            if (p == null) { throw new PersonnelNotFoundException(id.ToString()); }
            return p;
        }

        public async Task<IEnumerable<Personnel>> GetItems(bool forceRefresh = false)
        {
            await conn.CreateTableAsync<Personnel>();

            List<Personnel> list = await conn.Table<Personnel>().ToListAsync();
            if (list != null && list.Any())
            {
                list = list.OrderBy(o => o.Name).ToList();

                return list;
            }
            else
            {
                throw new PersonnelNotFoundException("No records found");
            }
        }

        public async Task<bool> UpdateItemAsync(Personnel item)
        {
            await conn.CreateTableAsync<Personnel>();

            int check = await conn.UpdateAsync(item);
            if (check > 0) { return true; }
            else { throw new Exception("Error updating item"); }
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
            await conn.CreateTableAsync<Personnel>();

            Organization? mostPopularOrg = null;

            List<Organization> allOrgs = await new OrganizationService().GetItems() as List<Organization>; //OrganizationTools.GetStaticOrganizations(Guid.Empty);
            try
            {
                var _AllTeamMembers = await GetItems();

                foreach (Organization org in allOrgs)
                {
                    org.UserCount = _AllTeamMembers.Count(o => o.OrganizationID == org.OrganizationID);
                }
                if (allOrgs.Any(o => o.UserCount > 0))
                {
                    mostPopularOrg = allOrgs.OrderByDescending(o => o.UserCount).First();
                }

                if (mostPopularOrg == null)
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
            catch (PersonnelNotFoundException ex)
            {
                return null;
            }
        }

        public async Task<Personnel?> GetCurrentPersonAsync()
        {
            await conn.CreateTableAsync<Personnel>();

            string selectedID = Preferences.Get("SelectedPersonID", string.Empty);
            if (!string.IsNullOrEmpty(selectedID))
            {
                try
                {
                    Guid SelectedPersonID = new Guid(selectedID);

                    if (SelectedPersonID != Guid.Empty)
                    {
                        Personnel person = await GetItemAsync(SelectedPersonID);
                        return person;
                    }
                }
                catch (PersonnelNotFoundException pne)
                {
                    ;
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
