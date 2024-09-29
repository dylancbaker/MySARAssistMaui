using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels.Clues
{
    [Serializable]
    public class Clue : SyncableItem, ICloneable
    {

        public Clue()
        {
            ClueID = Guid.NewGuid();
        }

        private Guid _TaskID;
        private Guid _AssignmentID;
        private int _ClueNumber;
        private bool _Eliminated;
        private string _Description;
        private string _FoundBy;
        private DateTime _DateFound;
        private string _LocationFound;
        private string _DescriptionOfLocation;
        private string _LocationRouteFlagged;
        private string _CurrentStatus;
        private string _Evaluation;
        private string _FollowUpActions;
        private string _ImageBytes;
        private double _Latitude;
        private double _Longitude;
        private string _RecordedBy;
        private DateTime _DatePrepared;
        private string _ClueValue;
        private int _AssignmentNumber;

        public Guid TaskID { get => _TaskID; set => _TaskID = value; }
        [PrimaryKey] public Guid ClueID { get => ID; set => ID = value; }
        public Guid AssignmentID { get => _AssignmentID; set => _AssignmentID = value; }
        public DateTime DatePrepared { get => _DatePrepared; set => _DatePrepared = value; }
        public string RecordedBy { get => _RecordedBy; set => _RecordedBy = value; }
        public int AssignmentNumber { get => _AssignmentNumber; set => _AssignmentNumber = value; }

        public int ClueNumber { get => _ClueNumber; set => _ClueNumber = value; }
        public string ClueValue { get => _ClueValue; set => _ClueValue = value; }
        public bool Eliminated { get => _Eliminated; set => _Eliminated = value; }
        public string Description { get => _Description; set => _Description = value; }
        public string FoundBy { get => _FoundBy; set => _FoundBy = value; }
        public DateTime DateFound { get => _DateFound; set => _DateFound = value; }
        public string LocationFound { get => _LocationFound; set => _LocationFound = value; }
        public string DescriptionOfLocation { get => _DescriptionOfLocation; set => _DescriptionOfLocation = value; }
        public string LocationRouteFlagged { get => _LocationRouteFlagged; set => _LocationRouteFlagged = value; }
        public string CurrentStatus { get => _CurrentStatus; set => _CurrentStatus = value; }
        public string Evaluation { get => _Evaluation; set => _Evaluation = value; }
        public string FollowUpActions { get => _FollowUpActions; set => _FollowUpActions = value; }
        public string ImageBytes { get => _ImageBytes; set => _ImageBytes = value; }
        public double Latitude { get => _Latitude; set => _Latitude = value; }
        public double Longitude { get => _Longitude; set => _Longitude = value; }



        public new Clue? Clone()
        {

            return this.MemberwiseClone() as Clue;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
