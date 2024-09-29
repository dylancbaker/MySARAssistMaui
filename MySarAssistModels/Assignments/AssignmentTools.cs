using MySarAssistModels.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels.Assignments
{
    public static class AssignmentTools
    {
        public static Assignment CreateClone(this Assignment cloneFrom, int NewAssignmentNumber)
        {
            Assignment clone = new Assignment();
            clone = cloneFrom.Clone();
            clone.AssignmentID = Guid.NewGuid();
            clone.teamMembers = new List<Personnel>();
            clone.AssignmentNumber = NewAssignmentNumber;
            clone.DatePrepared = DateTime.Now;
            clone.TeamName = clone.AssignmentNumber.ToString();
            return clone;
        }

        public static List<AssignmentType> GetAssignmentTypes()
        {
            List<AssignmentType> types = new List<AssignmentType>();
            types.Add(new AssignmentType("Initial Response", 0));
            types.Add(new AssignmentType("Tracking", 1));
            types.Add(new AssignmentType("Sound Sweep", 2));
            types.Add(new AssignmentType("Dog", 3));
            types.Add(new AssignmentType("Type 2 Grid", 4));
            types.Add(new AssignmentType("Type 3 Grid", 5));
            types.Add(new AssignmentType("Rope Rescue", 6));
            types.Add(new AssignmentType("Swiftwater", 7));
            types.Add(new AssignmentType("OAR / Avi", 8));
            types.Add(new AssignmentType("Evacuation", 9));
            types.Add(new AssignmentType("Mountain Rescue", 10));
            types.Add(new AssignmentType("Boat", 11));
            types.Add(new AssignmentType("Off-Road Vehicle", 12));
            types.Add(new AssignmentType("CDFL / Hoist", 13));

            return types;
        }

        public static AssignmentType GetAssignmentTypeByName(string name)
        {
            List<AssignmentType> types = GetAssignmentTypes();
            if (types.Any(o => o.TypeName.Equals(name, StringComparison.OrdinalIgnoreCase))) { return types.First(o => o.TypeName.Equals(name, StringComparison.OrdinalIgnoreCase)); }
            return null;
        }
        public static AssignmentType GetAssignmentTypeByIndex(int index)
        {
            List<AssignmentType> types = GetAssignmentTypes();
            if (types.Any(o => o.IndexInList == index)) { return types.First(o => o.IndexInList == index); }
            return null;
        }
    }
}
