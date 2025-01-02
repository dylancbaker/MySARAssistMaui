using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels.IncidentItems
{
    public static class IncidentItemTools
    {
        public static List<IncidentItemType> GetItemTypes(bool includeAll)
        {
            List<IncidentItemType> types = new List<IncidentItemType>();
            if (includeAll) { types.Add(new IncidentItemType() { Id = 0, Name = "All" }); }
            types.Add(new IncidentItemType() { Id = 1, Name = "Clues" });
            types.Add(new IncidentItemType() { Id = 2, Name = "Assignment Debriefs" });
            types.Add(new IncidentItemType() { Id = 3, Name = "Team Briefings" });
            types.Add(new IncidentItemType() { Id = 4, Name = "Incident Briefings" });
            return types;
        }
    }
}