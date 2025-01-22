using MySarAssistModels;
using MySarAssistModels.Assignments;
using MySarAssistModels.Clues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.ViewModels.IncidentInfoViewModels
{
    public class IncidentInfoItemViewModel
    {
        private SyncableItem _syncableItem;

        public IncidentInfoItemViewModel(SyncableItem syncableItem)
        {
            _syncableItem = syncableItem;
        }
        private Type _ItemType { get => _syncableItem.GetType(); }
        public Guid Id { get => _syncableItem.ID; }
        public string ItemType
        {
            get
            {
                if (_ItemType == typeof(AssignmentDebrief)) { return "Assignment Debrief"; }
                else if (_ItemType == typeof(IncidentResource)) { return "Incident Resource"; }
                else if (_ItemType == typeof(Clue)) { return "Clue"; }
                else { return "Unknown"; }
            }
        }
        public string ItemTypeInitials
        {
            get
            {
                if (_ItemType == typeof(AssignmentDebrief)) { return "AD"; }
                else if (_ItemType == typeof(IncidentResource)) { return "IR"; }
                else if (_ItemType == typeof(Clue)) { return "CL"; }
                else { return "??"; }
            }

        }
        public string? IncidentIdentifier { get => _syncableItem.IncidentIdentifier; }
        public string ItemName
        {
            get
            {
                if (_syncableItem == null) { return string.Empty; }
                else
                {
                    if (_ItemType == typeof(AssignmentDebrief)) { return (_syncableItem as AssignmentDebrief).TeamName; }
                    else if (_ItemType == typeof(IncidentResource)) { return "Incident Resource"; }
                    else if (_ItemType == typeof(Clue)) { return (_syncableItem as Clue).Description;  }
                    else { return "Unknown"; }

                }
            }
        }

        public Color IconColour
        {
            get
            {
                if (_syncableItem == null) { return Colors.Gray; }
                else
                {
                    if (_ItemType == typeof(AssignmentDebrief)) { return Colors.Purple; }
                    else if (_ItemType == typeof(IncidentResource)) { return Colors.Blue; }
                    else if (_ItemType == typeof(Clue)) { return Colors.Red; }
                    else { return Colors.Gray; }

                }
            }
        }
        public string? CreatedBy { get => _syncableItem.CreatedBy; }
        public DateTime DateCreated { get => _syncableItem.DateCreatedUTC.ToLocalTime(); }
    }
}
