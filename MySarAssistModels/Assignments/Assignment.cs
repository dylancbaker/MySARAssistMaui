using MySarAssistModels.People;
using MySarAssistModels.RADeMS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels.Assignments
{
    [Serializable]
    public class Assignment : SyncableItem, ICloneable
    {
         private string _TaskNumber;
         private string _TaskName;
         private string _TeamName;
         private DateTime _DatePrepared;
         private int _AssignmentNumber;
         private string _AssignmentName;
         private int _Priority;
         private string _CreatedBy;
         private string _Description;
         private string _Terrain;
         private int _MemberRequired;
         private string _Coverage;
         private DateTime _PlannedStart;
         private int _PlannedDurationMinutes;
         private string _SpecialEquipment;
         private string _Transportation;
         private string _PrimaryChannel;
         private string _SecondaryChannel;
         private string _EmergencyChannel;
         private string _Repeater;
         private string _ICPCallSign;
         private int _RADeMSOperationalRisk;
         private int _RADeMSResponseCapability;
         private string _AssignmentTypeCheckboxes;
         private string _pdfMapURL;
         private string _pdfMapFile;
         private double _AreaOfAssignment;
         private double _TeamSpacing;
         private double _RangeOfDetection;
         private double _SubjectVisibilityCorrection;
         private double _EstimatedSpeed;
         private List<Personnel> _teamMembers;
         private Guid _MapSegmentID;
         private double _POA;
         private double _POD;
         private Guid _TaskID;
         private string _PDFMapFileNameOnServer;
         private double _assignmentRouteLength;
         private double _estimatedDurationMinutes;
         private string _CoordinateText;
         private double _ManualPOD;
         private int _PlannedMembersRequired;
         private double _DebriefSpacing;
         private double _DebriefTravelSpeedPerHour;
         private double _DebriefRangeOfDetection;
         private Guid _MostRecentRademsID;





        public Assignment()
        {
            AssignmentID = Guid.NewGuid(); OpPeriod = 1; teamMembers = new List<Personnel>();
            LastUpdated = DateTime.UtcNow;
        }

        public Guid AssignmentID { get => ID; set { LastUpdated = DateTime.UtcNow; ID = value; } }
        public string TaskNumber { get => _TaskNumber; set { LastUpdated = DateTime.UtcNow; _TaskNumber = value; } }
        public string TaskName { get => _TaskName; set { LastUpdated = DateTime.UtcNow; _TaskName = value; } }
        public string TeamName { get => _TeamName; set { LastUpdated = DateTime.UtcNow; _TeamName = value; } }
        public string TeamNameWithNumber
        {
            get
            {
                if (AssignmentNumber.ToString().Equals(TeamName, StringComparison.InvariantCultureIgnoreCase)) { return TeamName; }
                else { return AssignmentNumber + " - " + TeamName; }
            }
        }
        public DateTime DatePrepared { get => _DatePrepared; set { LastUpdated = DateTime.UtcNow; _DatePrepared = value; } }
        public DateTime LastUpdated { get => LastUpdatedUTC; set { LastUpdatedUTC = value; } }
        public int AssignmentNumber { get => _AssignmentNumber; set { LastUpdated = DateTime.UtcNow; _AssignmentNumber = value; } }
        public string AssignmentName { get => _AssignmentName; set { LastUpdated = DateTime.UtcNow; _AssignmentName = value; } }
        public string AssignmentNameWithNumber
        {
            get
            {
                if (AssignmentNumber > 0 && !AssignmentNumber.ToString().Equals(AssignmentName, StringComparison.InvariantCultureIgnoreCase)) { return AssignmentNumber.ToString() + " " + AssignmentName; }
                else { return AssignmentName; }
            }
        }
        public string TeamNameWithAssignmentName { get { return TeamName + " " + AssignmentName; } }

        public int Priority { get => _Priority; set { LastUpdated = DateTime.UtcNow; _Priority = value; } }
        public string CreatedBy { get => _CreatedBy; set { LastUpdated = DateTime.UtcNow; _CreatedBy = value; } }
        public string Description { get => _Description; set { LastUpdated = DateTime.UtcNow; _Description = value; } }
        public string Terrain { get => _Terrain; set { LastUpdated = DateTime.UtcNow; _Terrain = value; } }
        public int MembersRequired { get => _MemberRequired; set { LastUpdated = DateTime.UtcNow; _MemberRequired = value; } }
        public int PlannedMembersRequired { get => _PlannedMembersRequired; set { LastUpdated = DateTime.UtcNow; _PlannedMembersRequired = value; } }
        public int FinalMembersRequired { get { if (teamMembers.Count > 0) { return teamMembers.Count; } else { return PlannedMembersRequired; } } }

        public string Coverage { get => _Coverage; set { LastUpdated = DateTime.UtcNow; _Coverage = value; } }
        public DateTime PlannedStart { get => _PlannedStart; set { LastUpdated = DateTime.UtcNow; _PlannedStart = value; } }
        public int PlannedDurrationMinutes { get => _PlannedDurationMinutes; set { LastUpdated = DateTime.UtcNow; _PlannedDurationMinutes = value; } }
        public double PlannedDurrationHours { get => PlannedDurrationMinutes / 60; }
        public string SpecialEquipment { get => _SpecialEquipment; set { LastUpdated = DateTime.UtcNow; _SpecialEquipment = value; } }
        public string Transportation { get => _Transportation; set { LastUpdated = DateTime.UtcNow; _Transportation = value; } }
        public string PrimaryChannel { get => _PrimaryChannel; set { LastUpdated = DateTime.UtcNow; _PrimaryChannel = value; } }
        public string SecondaryChannel { get => _SecondaryChannel; set { LastUpdated = DateTime.UtcNow; _SecondaryChannel = value; } }
        public string EmergencyChannel { get => _EmergencyChannel; set { LastUpdated = DateTime.UtcNow; _EmergencyChannel = value; } }
        public string Repeater { get => _Repeater; set { LastUpdated = DateTime.UtcNow; _Repeater = value; } }
        public string ICPCallSign { get => _ICPCallSign; set { LastUpdated = DateTime.UtcNow; _ICPCallSign = value; } }
        public int RADeMSOperationalRisk { get => _RADeMSOperationalRisk; set { LastUpdated = DateTime.UtcNow; _RADeMSOperationalRisk = value; } }
        public int RADeMSResponseCapability { get => _RADeMSResponseCapability; set { LastUpdated = DateTime.UtcNow; _RADeMSResponseCapability = value; } }


        public string AssignmentTypeCheckboxes
        {
            get
            {
                if (string.IsNullOrEmpty(_AssignmentTypeCheckboxes))
                {
                    for (int x = 0; x < AssignmentTools.GetAssignmentTypes().Count; x++)
                    {
                        _AssignmentTypeCheckboxes = _AssignmentTypeCheckboxes + "0";
                    }
                }
                return _AssignmentTypeCheckboxes;
            }
            set { LastUpdated = DateTime.UtcNow; _AssignmentTypeCheckboxes = value; }
        }

        private bool IsAssignmentTypeByName(string name)
        {


            AssignmentType t = AssignmentTools.GetAssignmentTypeByName(name);

            if (string.IsNullOrEmpty(AssignmentTypeCheckboxes))
            {
                for (int x = 0; x < AssignmentTools.GetAssignmentTypes().Count; x++)
                {
                    AssignmentTypeCheckboxes = AssignmentTypeCheckboxes + "0";
                }
            }
            if (AssignmentTypeCheckboxes.Length <= t.IndexInList)
            {
                for (int x = 0; x < t.IndexInList - AssignmentTypeCheckboxes.Length; x++)
                {
                    AssignmentTypeCheckboxes = AssignmentTypeCheckboxes + "0";
                }
            }


            if (t != null)
            {
                if (AssignmentTypeCheckboxes.Length > t.IndexInList)
                {
                    return AssignmentTypeCheckboxes.Substring(t.IndexInList, 1).Equals("1");
                }
            }
            return false;
        }
        private bool IsAssignmentTypeByIndex(int index)
        {

            AssignmentType t = AssignmentTools.GetAssignmentTypeByIndex(index);
            if (t != null)
            {
                if (AssignmentTypeCheckboxes.Length > t.IndexInList)
                {
                    return AssignmentTypeCheckboxes.Substring(t.IndexInList, 1).Equals("1");
                }
            }
            return false;
        }
        private void SetIsAssignmentTypeByName(string name, bool IsType)
        {
            AssignmentType t = AssignmentTools.GetAssignmentTypeByName(name);
            if (t != null)
            {
                if (string.IsNullOrEmpty(AssignmentTypeCheckboxes))
                {
                    for (int x = 0; x < AssignmentTools.GetAssignmentTypes().Count; x++)
                    {
                        AssignmentTypeCheckboxes = AssignmentTypeCheckboxes + "0";
                    }
                }
                if (AssignmentTypeCheckboxes.Length <= t.IndexInList + 1)
                {
                    for (int x = 0; x < (t.IndexInList + 1) - AssignmentTypeCheckboxes.Length; x++)
                    {
                        AssignmentTypeCheckboxes = AssignmentTypeCheckboxes + "0";
                    }
                }
                string typeCharacter = "0";
                if (IsType) { typeCharacter = "1"; }
                AssignmentTypeCheckboxes = AssignmentTypeCheckboxes.Substring(0, t.IndexInList) + typeCharacter + AssignmentTypeCheckboxes.Substring(t.IndexInList + 1);
            }
        }

        //these break out the individual assignment type checkboxes
        public bool isIRT
        {
            get { return IsAssignmentTypeByName("Initial Response"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Initial Response", value); }
        }
        public bool isTracking
        {
            get { return IsAssignmentTypeByName("Tracking"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Tracking", value); }

        }
        public bool isSoundSweep
        {
            get { return IsAssignmentTypeByName("Sound Sweep"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Sound Sweep", value); }

        }
        public bool isDog
        {
            get { return IsAssignmentTypeByName("Dog"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Dog", value); }

        }
        public bool isType2
        {
            get { return IsAssignmentTypeByName("Type 2 Grid"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Type 2 Grid", value); }

        }
        public bool isType3
        {
            get { return IsAssignmentTypeByName("Type 3 Grid"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Type 3 Grid", value); }

        }
        public bool isRR
        {
            get { return IsAssignmentTypeByName("Rope Rescue"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Rope Rescue", value); }

        }
        public bool isSW
        {
            get { return IsAssignmentTypeByName("Swiftwater"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Swiftwater", value); }

        }
        public bool isAvi
        {
            get { return IsAssignmentTypeByName("OAR / Avi"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("OAR / Avi", value); }

        }
        public bool isEvac
        {
            get { return IsAssignmentTypeByName("Evacuation"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Evacuation", value); }

        }
        public bool isMR
        {
            get { return IsAssignmentTypeByName("Mountain Rescue"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Mountain Rescue", value); }

        }
        public bool isBoat
        {
            get { return IsAssignmentTypeByName("Boat"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Boat", value); }

        }
        public bool isORV
        {
            get { return IsAssignmentTypeByName("Off-Road Vehicle"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("Off-Road Vehicle", value); }

        }
        public bool isCDFL
        {
            get { return IsAssignmentTypeByName("CDFL / Hoist"); }
            set { LastUpdated = DateTime.UtcNow; SetIsAssignmentTypeByName("CDFL / Hoist", value); }

        }

        public bool AnySearchType
        {
            get
            {
                if (isIRT || isTracking || isSoundSweep || isDog || isType2 || isType3) { return true; }
                return false;
            }

        }

        public string AssignmentTypeText
        {
            get
            {
                StringBuilder txt = new StringBuilder();
                List<AssignmentType> types = AssignmentTools.GetAssignmentTypes();

                for (int x = 0; x < AssignmentTypeCheckboxes.Length; x++)
                {
                    if (AssignmentTypeCheckboxes.Substring(x, 1).Equals("1"))
                    {
                        AssignmentType t = AssignmentTools.GetAssignmentTypeByIndex(x);
                        if (txt.Length > 0) { txt.Append(", "); }
                        txt.Append(t.TypeName);
                    }
                }

                return txt.ToString();

            }
        }
        public bool AnyAssignmentTypeSet { get { if (!string.IsNullOrEmpty(AssignmentTypeCheckboxes)) { return !AssignmentTypeCheckboxes.Equals("00000000000"); } else { return false; } } }

        public string pdfMapURL { get => _pdfMapURL; set { LastUpdated = DateTime.UtcNow; _pdfMapURL = value; } }
        public string pdfMapFile { get => _pdfMapFile; set { LastUpdated = DateTime.UtcNow; _pdfMapFile = value; } }
        public double AreaOfAssignment { get => _AreaOfAssignment; set { LastUpdated = DateTime.UtcNow; _AreaOfAssignment = value; } }
        public double TeamSpacing { get => _TeamSpacing; set { LastUpdated = DateTime.UtcNow; _TeamSpacing = value; } }
        public double FinalTeamSpacing { get { if (DebriefSpacing > 0) { return DebriefSpacing; } else { return TeamSpacing; } } }
        public double FinalSpeed { get { if (DebriefTravelSpeedPerHour > 0) { return DebriefTravelSpeedPerHour; } else { return EstimatedSpeed; } } }
        public double FinalRangeOfDetection { get { if (DebriefRangeOfDetection > 0) { return DebriefRangeOfDetection; } else { return RangeOfDetection; } } }


        public double RangeOfDetection { get => _RangeOfDetection; set { LastUpdated = DateTime.UtcNow; _RangeOfDetection = value; } }
        public double SubjectVisibilityCorrection { get => _SubjectVisibilityCorrection; set { LastUpdated = DateTime.UtcNow; _SubjectVisibilityCorrection = value; } }
        public double EstimatedSpeed { get => _EstimatedSpeed; set { LastUpdated = DateTime.UtcNow; _EstimatedSpeed = value; } }
        public List<Personnel> teamMembers { get => _teamMembers; set { LastUpdated = DateTime.UtcNow; _teamMembers = value; } }
        public Guid MapSegmentID { get => _MapSegmentID; set { LastUpdated = DateTime.UtcNow; _MapSegmentID = value; } }
        public double POA { get => _POA; set { LastUpdated = DateTime.UtcNow; _POA = value; } }
        public double POD { get => _POD; set { LastUpdated = DateTime.UtcNow; _POD = value; } }
        public double ManualPOD { get => _ManualPOD; set { LastUpdated = DateTime.UtcNow; _ManualPOD = value; } }
        public double SelectedPOD { get { if (ManualPOD != 0) { return ManualPOD; } else { return POD; } } }
        public double PDen
        {
            get
            {
                if (AreaOfAssignment > 0 && POA > 0)
                {
                    return POA / AreaOfAssignment;
                }
                else
                {
                    return 0;
                }
            }
        }
        public double POS
        {
            get { return POA * SelectedPOD; }
        }
        public double PSR
        {
            get
            {
                if (PDen > 0)
                {
                    return EstimatedSpeed * PDen * (TeamSpacing / 1000); //added /1000 to convert meters to kms for spacing
                }
                else { return 0; }
            }
        }

        public Guid TaskID { get => _TaskID; set { LastUpdated = DateTime.UtcNow; _TaskID = value; } }


        public double DebriefSpacing { get => _DebriefSpacing; set => _DebriefSpacing = value; }
        public double DebriefTravelSpeedPerHour { get => _DebriefTravelSpeedPerHour; set => _DebriefTravelSpeedPerHour = value; }
        public double DebriefRangeOfDetection { get => _DebriefRangeOfDetection; set => _DebriefRangeOfDetection = value; }









        public string PDFMapFileNameOnServer { get => _PDFMapFileNameOnServer; set { LastUpdated = DateTime.UtcNow; _PDFMapFileNameOnServer = value; } }
        public double AssignmentRouteLength { get => _assignmentRouteLength; set { LastUpdated = DateTime.UtcNow; _assignmentRouteLength = value; } }
        public double EstimatedDurrationMinutes { get => _estimatedDurationMinutes; set { LastUpdated = DateTime.UtcNow; _estimatedDurationMinutes = value; } }
        public string CoordinateText { get => _CoordinateText; set { LastUpdated = DateTime.UtcNow; _CoordinateText = value; } }



        public Personnel TeamLeader
        {
            get
            {
                if (teamMembers.Where(o => o.isAssignmentTeamLeader).Any())
                {
                    return teamMembers.Where(o => o.isAssignmentTeamLeader).First();
                }
                else
                {
                    return new Personnel();
                }
            }
        }
        public string TeamLeaderName { get { return TeamLeader.Name; } }

        public bool hasTeamMember(Guid TeamMemberID)
        {
            return teamMembers.Where(o => o.PersonID == TeamMemberID).Count() > 0;
        }


        public new Assignment Clone()
        {
            Assignment cloneTo = this.MemberwiseClone() as Assignment;
            cloneTo.teamMembers = new List<Personnel>();
            cloneTo.LastUpdated = this.LastUpdated;
            foreach (Personnel member in this.teamMembers) { cloneTo.teamMembers.Add(member.Clone()); }
            return cloneTo;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }


        public bool Has40HrFirstAid
        {
            get
            {
                return teamMembers.Any(o => o.FirstAid40Plus);
            }
        }


        public bool IsIdentical(Assignment compareTo)
        {
            bool Identical = true;

            if (TaskNumber != compareTo.TaskNumber) { Identical = false; }
            if (TaskName != compareTo.TaskName) { Identical = false; }
            if (AssignmentNumber != compareTo.AssignmentNumber) { Identical = false; }
            if (TeamName != compareTo.TeamName) { Identical = false; }
            if (OpPeriod != compareTo.OpPeriod) { Identical = false; }
            if (DatePrepared != compareTo.DatePrepared) { Identical = false; }
            if (AssignmentName != compareTo.AssignmentName) { Identical = false; }
            if (Priority != compareTo.Priority) { Identical = false; }
            if (CreatedBy != compareTo.CreatedBy) { Identical = false; }
            if (Description != compareTo.Description) { Identical = false; }
            if (Terrain != compareTo.Terrain) { Identical = false; }
            if (MembersRequired != compareTo.MembersRequired) { Identical = false; }
            if (Coverage != compareTo.Coverage) { Identical = false; }
            if (PlannedStart != compareTo.PlannedStart) { Identical = false; }
            if (PlannedDurrationMinutes != compareTo.PlannedDurrationMinutes) { Identical = false; }
            if (SpecialEquipment != compareTo.SpecialEquipment) { Identical = false; }
            if (Transportation != compareTo.Transportation) { Identical = false; }
            if (PrimaryChannel != compareTo.PrimaryChannel) { Identical = false; }
            if (SecondaryChannel != compareTo.SecondaryChannel) { Identical = false; }
            if (EmergencyChannel != compareTo.EmergencyChannel) { Identical = false; }
            if (Repeater != compareTo.Repeater) { Identical = false; }
            if (ICPCallSign != compareTo.ICPCallSign) { Identical = false; }
            if (RADeMSOperationalRisk != compareTo.RADeMSOperationalRisk) { Identical = false; }
            if (RADeMSResponseCapability != compareTo.RADeMSResponseCapability) { Identical = false; }
            if (AssignmentTypeCheckboxes != compareTo.AssignmentTypeCheckboxes) { Identical = false; }
            if (pdfMapURL != compareTo.pdfMapURL) { Identical = false; }
            if (pdfMapFile != compareTo.pdfMapFile) { Identical = false; }
            if (TeamSpacing != compareTo.TeamSpacing) { Identical = false; }
            if (AreaOfAssignment != compareTo.AreaOfAssignment) { Identical = false; }
            if (RangeOfDetection != compareTo.RangeOfDetection) { Identical = false; }
            if (EstimatedSpeed != compareTo.EstimatedSpeed) { Identical = false; }
            if (MapSegmentID != compareTo.MapSegmentID) { Identical = false; }
            if (teamMembers.Count != compareTo.teamMembers.Count) { Identical = false; }
            else
            {
                for (int x = 0; x < teamMembers.Count; x++)
                {
                    if (!teamMembers[x].IsIdentical(compareTo.teamMembers[x])) { Identical = false; }
                }
            }

            return Identical;
        }
    }
}
