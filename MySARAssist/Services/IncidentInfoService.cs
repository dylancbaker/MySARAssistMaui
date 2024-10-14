using MySARAssist.ViewModels.IncidentInfoViewModels;
using MySarAssistModels.Assignments;
using MySarAssistModels.Clues;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Services
{
    public class IncidentInfoService
    {
        SQLiteAsyncConnection conn;

        public IncidentInfoService()
        {
            conn = new SQLiteAsyncConnection(Constants.dbPath);
        }

        public async Task< List<IncidentInfoItemViewModel>> GetAllIncidentInfoVMs()
        {
            List<IncidentInfoItemViewModel> viewModels = new List<IncidentInfoItemViewModel>();
            ClueService clueService = new ClueService();
            var clueList = await clueService.GetItemsAsync();
            if (clueList != null) { foreach (Clue clue in clueList) { viewModels.Add(new IncidentInfoItemViewModel(clue)); } }
            
            List<AssignmentDebrief>? debriefs = new List<AssignmentDebrief>();
            List<Assignment>? assignments = new List<Assignment>();


            return viewModels;
        }
    }

}
