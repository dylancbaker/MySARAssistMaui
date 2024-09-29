using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels.Assignments
{
    [Serializable]
    public class AssignmentType
    {
        private string _TypeName;
        private int _IndexInList;
        public string TypeName { get => _TypeName; set => _TypeName = value; }
        public int IndexInList { get => _IndexInList; set => _IndexInList = value; }

        public AssignmentType() { }
        public AssignmentType(string name, int index) { TypeName = name; IndexInList = index; }

        public bool AssignmentIsThisType(Assignment assignment)
        {
            if (string.IsNullOrEmpty(assignment.AssignmentTypeCheckboxes) || assignment.AssignmentTypeCheckboxes.Length <= IndexInList)
            {
                return false;
            }
            return (assignment.AssignmentTypeCheckboxes.Substring(IndexInList, 1).Equals("1"));
        }

      
        public string getUpdatedCheckboxString(string currentString, bool check_this_type)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(currentString))
            {
                for (int x = 0; x < AssignmentTools.GetAssignmentTypes().Count; x++) { sb.Append("0"); }

            }
            else { sb = new StringBuilder(currentString); }
            if (check_this_type)
            {
                sb[IndexInList] = '1';
            }
            else { sb[IndexInList] = '0'; }
            return sb.ToString();
        }


    }
}
