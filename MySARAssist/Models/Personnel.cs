using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MySARAssist.Models
{
    public class Personnel : IncidentResource, IEquatable<Personnel>, ICloneable
    {
        public Personnel()
        {
            PersonID = Guid.NewGuid();
            MemberActive = true;
            QualificationList = new bool[28];
        }

        private string? _Name;
        private string? _Group;
        private string? _Callsign;
        private string? _Phone;
        private string? _SpecialSkills;
        private bool _isAssignmentTeamLeader;
        private string? _Reference;
        private string? _barcode;
        private bool _signedIn;
        private Guid _organizationID;
        private Guid _userID;
        private string? _Email;
        private Guid _CreatedByOrgID;
        private string? _Address;
        private string? _NOKName;
        private string? _NOKRelation;
        private string? _NOKPhone;
        private string? _D4HStatus;
        private int _D4HID;
        private string? _Dietary;
        private bool _Vegetarian;
        private bool _NoGluten;
        private string? _Pronouns;
        private bool[] _QualificationList = new bool[28];
        private double _PacesPer100;
        private bool _CurrentlySelected;

        [PrimaryKey] public Guid PersonID { get => ID; set => ID = value; }
        public int D4HID { get => _D4HID; set => _D4HID = value; }

        public string? Name { get => _Name; set => _Name = value; }
        public string? Pronouns { get => _Pronouns; set => _Pronouns = value; }

        public string? Group { get => _Group; set => _Group = value; } //Use OrganizationName for this value
        public string? NameWithGroup
        {
            get
            {
                if (!string.IsNullOrEmpty(Group)) { return Name + " (" + Group + ")"; }
                else { return Name; }
            }
        }
        public string? NameWithTL
        {
            get
            {
                if (GSTL) { return Name + " (GSTL)"; }
                else { return Name; }
            }
        }
        public string? NameWithSARM
        {
            get
            {
                if (SARM) { return Name + " (SAR Mgr)"; }
                else { return Name; }
            }
        }
        public string? NameWithGroupAndCurrent
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (CurrentlySelected) { sb.Append("*"); }
                sb.Append(Name);
                if (!string.IsNullOrEmpty(Group))
                {
                    sb.Append(" ("); sb.Append(Group); sb.Append(")");
                }
                return sb.ToString();
            }
        }

        [Ignore] public bool[] QualificationList { get { if (_QualificationList == null) { _QualificationList = new bool[28]; } return _QualificationList; } set => _QualificationList = value; }

        public string? Callsign { get => _Callsign; set => _Callsign = value; }
        public string? Phone { get => _Phone; set => _Phone = value; }
        public bool RopeRescue { get { if (QualificationList != null && (QualificationList[13] || QualificationList[14] || QualificationList[15])) { return true; } return false; } }
        public bool Tracker { get { if (QualificationList != null && (QualificationList[18] || QualificationList[19] || QualificationList[20])) { return true; } return false; } }
        public bool FirstAid { get { if (QualificationList != null && (QualificationList[4] || QualificationList[5] || QualificationList[6] || QualificationList[7] || QualificationList[8] || QualificationList[9])) { return true; } return false; } }
        public bool FirstAid40Plus { get { if (QualificationList != null && (QualificationList[5] || QualificationList[6] || QualificationList[7] || QualificationList[8] || QualificationList[9])) { return true; } return false; } }
        public bool GSAR { get => (QualificationList != null && QualificationList[0]); }
        public string? SpecialSkills { get => _SpecialSkills; set => _SpecialSkills = value; }
        public bool isAssignmentTeamLeader { get => _isAssignmentTeamLeader; set => _isAssignmentTeamLeader = value; }
        public string? Reference { get { return _Reference; } set { _Reference = value; } }
        public bool GSTL { get { return QualificationList != null && QualificationList[1]; } }
        public bool SARM { get { return QualificationList != null && (QualificationList[2] || QualificationList[3]); } }
        public bool Swiftwater { get { if (QualificationList != null && (QualificationList[10] || QualificationList[11] || QualificationList[12])) { return true; } return false; } }
        public bool MountainRescue { get { if (QualificationList != null && (QualificationList[21] || QualificationList[22] || QualificationList[23])) { return true; } return false; } }
        public bool BoatOperator { get { if (QualificationList != null && QualificationList[24]) { return true; } return false; } }
        public bool CDFL { get { if (QualificationList != null && QualificationList[26]) { return true; } return false; } }
        public bool K9 { get { if (QualificationList != null && QualificationList[27]) { return true; } return false; } }
        public bool OffRoadVehicleOperator { get { if (QualificationList != null && QualificationList[24]) { return true; } return false; } }


        public string? Barcode { get { return _barcode; } set { _barcode = value; } }
        public bool SignedInToTask { get { return _signedIn; } set { _signedIn = value; } }
        public Guid OrganizationID { get => _organizationID; set => _organizationID = value; }
        public Guid UserID { get => _userID; set => _userID = value; }
        public bool MemberActive { get => Active; set => Active = value; }
        public string? Email { get => _Email; set => _Email = value; }
        public Guid CreatedByOrgID { get => _CreatedByOrgID; set => _CreatedByOrgID = value; } //used when a team creates members from outside their group for a task.
        public string? Address { get => _Address; set => _Address = value; }
        public string? AddressWithPhone
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(Address))
                {
                    sb.Append(Address.Replace("\r\n", " "));
                    if (!string.IsNullOrEmpty(Phone)) { sb.Append("\n"); }
                }
                if (!string.IsNullOrEmpty(Phone)) { sb.Append(Phone); }

                return sb.ToString();
            }
        }
        public string? NOKName { get => _NOKName; set => _NOKName = value; }
        public string? NOKRelation { get => _NOKRelation; set => _NOKRelation = value; }
        public string? NOKPhone { get => _NOKPhone; set => _NOKPhone = value; }
        public string? NOKOneLine
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(NOKName)) { sb.Append(NOKName); sb.Append(" "); }
                if (!string.IsNullOrEmpty(NOKRelation)) { sb.Append("("); sb.Append(NOKRelation); sb.Append(") "); }
                if (!string.IsNullOrEmpty(NOKPhone)) { sb.Append(NOKPhone); }
                return sb.ToString();
            }
        }
        public string? D4HStatus { get => _D4HStatus; set => _D4HStatus = value; }
        public string? Dietary { get => _Dietary; set => _Dietary = value; }
        public bool Vegetarian { get => _Vegetarian; set => _Vegetarian = value; }
        public bool NoGluten { get => _NoGluten; set => _NoGluten = value; }
        public double PacesPer100 { get => _PacesPer100; set => _PacesPer100 = value; }
        public bool CurrentlySelected { get => _CurrentlySelected; set => _CurrentlySelected = value; }
        [Ignore]
        public Organization? MemberOrganization { get => _MemberOrganization; set => _MemberOrganization = value; }
        private Organization? _MemberOrganization;


        /* this is so qualifications get saved in sqllite */
        public bool Qualification0 { get => _QualificationList[0]; set => _QualificationList[0] = value; }
        public bool Qualification1 { get => _QualificationList[1]; set => _QualificationList[1] = value; }
        public bool Qualification2 { get => _QualificationList[2]; set => _QualificationList[2] = value; }
        public bool Qualification3 { get => _QualificationList[3]; set => _QualificationList[3] = value; }
        public bool Qualification4 { get => _QualificationList[4]; set => _QualificationList[4] = value; }
        public bool Qualification5 { get => _QualificationList[5]; set => _QualificationList[5] = value; }
        public bool Qualification6 { get => _QualificationList[6]; set => _QualificationList[6] = value; }
        public bool Qualification7 { get => _QualificationList[7]; set => _QualificationList[7] = value; }
        public bool Qualification8 { get => _QualificationList[8]; set => _QualificationList[8] = value; }
        public bool Qualification9 { get => _QualificationList[9]; set => _QualificationList[9] = value; }
        public bool Qualification10 { get => _QualificationList[10]; set => _QualificationList[10] = value; }
        public bool Qualification11 { get => _QualificationList[11]; set => _QualificationList[11] = value; }
        public bool Qualification12 { get => _QualificationList[12]; set => _QualificationList[12] = value; }
        public bool Qualification13 { get => _QualificationList[13]; set => _QualificationList[13] = value; }
        public bool Qualification14 { get => _QualificationList[14]; set => _QualificationList[14] = value; }
        public bool Qualification15 { get => _QualificationList[15]; set => _QualificationList[15] = value; }
        public bool Qualification16 { get => _QualificationList[16]; set => _QualificationList[16] = value; }
        public bool Qualification17 { get => _QualificationList[17]; set => _QualificationList[17] = value; }
        public bool Qualification18 { get => _QualificationList[18]; set => _QualificationList[18] = value; }
        public bool Qualification19 { get => _QualificationList[19]; set => _QualificationList[19] = value; }
        public bool Qualification20 { get => _QualificationList[20]; set => _QualificationList[20] = value; }
        public bool Qualification21 { get => _QualificationList[21]; set => _QualificationList[21] = value; }
        public bool Qualification22 { get => _QualificationList[22]; set => _QualificationList[22] = value; }
        public bool Qualification23 { get => _QualificationList[23]; set => _QualificationList[23] = value; }
        public bool Qualification24 { get => _QualificationList[24]; set => _QualificationList[24] = value; }
        public bool Qualification25 { get => _QualificationList[25]; set => _QualificationList[25] = value; }
        public bool Qualification26 { get => _QualificationList[26]; set => _QualificationList[26] = value; }
        public bool Qualification27 { get => _QualificationList[27]; set => _QualificationList[27] = value; }



        public bool Equals(Personnel other)
        {
            // Would still want to check for null etc. first.
            return this.PersonID == other.PersonID;
        }




        public new Personnel? Clone()
        {
            if (this != null)
            {
                Personnel cloneTo = this.MemberwiseClone() as Personnel;
                return cloneTo;
            } return null;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

    }


    public static class PersonnelTools
    {
        public static List<Qualification> GetPersonnelQualifications(this Personnel person)
        {
            List<Qualification> qualifications = PersonnelTools.GetQualifications();
            foreach (Qualification q in qualifications)
            {
                q.PersonHas = person.QualificationList[q.QualificationListIndex];
            }

            return qualifications;
        }


       

        public static void removeTildeFromRecord(this Personnel p)
        {
            if ((p.Name??string.Empty).Contains("~")) { p.Name = (p.Name ?? string.Empty).Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.Address) && p.Address.Contains("~")) { p.Address = p.Address.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.Pronouns) && p.Pronouns.Contains("~")) { p.Pronouns = p.Pronouns.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.Phone) && p.Phone.Contains("~")) { p.Phone = p.Phone.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.Email) && p.Email.Contains("~")) { p.Email = p.Email.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.Callsign) && p.Callsign.Contains("~")) { p.Callsign = p.Callsign.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.Reference) && p.Reference.Contains("~")) { p.Reference = p.Reference.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.NOKName) && p.NOKName.Contains("~")) { p.NOKName = p.NOKName.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.NOKRelation) && p.NOKRelation.Contains("~")) { p.NOKRelation = p.NOKRelation.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.NOKPhone) && p.NOKPhone.Contains("~")) { p.NOKPhone = p.NOKPhone.Replace("~", ""); }
            if (!string.IsNullOrEmpty(p.Dietary) && p.Dietary.Contains("~")) { p.Dietary = p.Dietary.Replace("~", ""); }

        }


        public static string StringForQR(this Personnel p)
        {

            StringBuilder qr = new StringBuilder();
            qr.Append(p.PersonID.ToString()); qr.Append(";"); //remove?

            qr.Append(p.Name); qr.Append(";");
            qr.Append(p.OrganizationID); qr.Append(";");
            if (!string.IsNullOrEmpty(p.Address)) { qr.Append(p.Address.Replace(Environment.NewLine, " ")); } else { qr.Append(""); }
            qr.Append(";");
            qr.Append(p.Phone); qr.Append(";");
            qr.Append(p.Email); qr.Append(";");
            qr.Append(p.Callsign); qr.Append(";"); //remove
            qr.Append(p.Reference); qr.Append(";"); //remove
                                                    //qualifications
                                                    //pretend these are characters in a binary string and convert to int?
            List<Qualification> qualifications = PersonnelTools.GetQualifications();
            qualifications = qualifications.OrderBy(o => o.QRIndex).ToList();
            for (int x = 0; x < qualifications.Count && x < p.QualificationList.Length; x++)
            {
                Qualification q = qualifications[x];
                if (p.QualificationList[q.QualificationListIndex]) { qr.Append("1"); } else { qr.Append("0"); }
            }
            qr.Append(";");

            //nok
            qr.Append(p.NOKName); qr.Append(";");
            qr.Append(p.NOKRelation); qr.Append(";"); //remove
            qr.Append(p.NOKPhone); qr.Append(";");


            return qr.ToString();

        }

        public static string StringForQRCompressed(this Personnel p)
        {

            StringBuilder qr = new StringBuilder();
            // qr.Append(PersonID.ToString()); qr.Append(";"); //remove?

            qr.Append(p.Name); qr.Append(";");
            if (p.OrganizationID != Guid.Empty)
            {
                Organization org = OrganizationTools.GetOrganization(p.OrganizationID);

                if (org != null)
                {
                    qr.Append(org.ShortOrganizationID); qr.Append(";");
                }
                else { qr.Append(""); qr.Append(";"); }
            }
            else { qr.Append(""); qr.Append(";"); }
            if (!string.IsNullOrEmpty(p.Address)) { qr.Append(p.Address.Replace(Environment.NewLine, " ")); } else { qr.Append(""); }
            qr.Append(";");
            if (!string.IsNullOrEmpty(p.Phone)) { qr.Append(p.Phone.Replace("-", "").Replace(" ", "")); } else { qr.Append(""); }
            qr.Append(";");
            qr.Append(p.Email); qr.Append(";");
            // qr.Append(Callsign); qr.Append(";"); //remove
            // qr.Append(Reference); qr.Append(";"); //remove
            //qualifications
            //pretend these are characters in a binary string and convert to int?
            StringBuilder bin = new StringBuilder();
            if (p.GSAR) { bin.Append("1"); } else { bin.Append("0"); }
            if (p.GSTL) { bin.Append("1"); } else { bin.Append("0"); }
            if (p.SARM) { bin.Append("1"); } else { bin.Append("0"); }
            if (p.FirstAid) { bin.Append("1"); } else { bin.Append("0"); }
            if (p.RopeRescue) { bin.Append("1"); } else { bin.Append("0"); }
            if (p.Tracker) { bin.Append("1"); } else { bin.Append("0"); }
            if (p.Swiftwater) { bin.Append("1"); } else { bin.Append("0"); }
            if (p.MountainRescue) { bin.Append("1"); } else { bin.Append("0"); }
            int qualNumber = 0;
            qualNumber = Convert.ToInt32(bin.ToString(), 2);
            qr.Append(qualNumber);
            qr.Append(";");

            //nok
            qr.Append(p.NOKName); qr.Append(";");
            // qr.Append(NOKRelation); qr.Append(";"); //remove
            if (!string.IsNullOrEmpty(p.NOKPhone)) { qr.Append(p.NOKPhone.Replace("-", "").Replace(" ", "")); }
            qr.Append(";");


            return qr.ToString();

        }

      
        public static bool IsIdentical(this Personnel orig, Personnel compareTo)
        {
            if (orig.PersonID != compareTo.PersonID) { return false; }
            if (orig.Name != compareTo.Name) { return false; }
            if (orig.Group != compareTo.Group) { return false; }
            if (orig.Callsign != compareTo.Callsign) { return false; }
            if (orig.Phone != compareTo.Phone) { return false; }
            if (orig.SpecialSkills != compareTo.SpecialSkills) { return false; }
            if (orig.isAssignmentTeamLeader != compareTo.isAssignmentTeamLeader) { return false; }
            if (orig.Reference != compareTo.Reference) { return false; }
            if (orig.Barcode != compareTo.Barcode) { return false; }
            if (orig.OrganizationID != compareTo.OrganizationID) { return false; }
            if (orig.UserID != compareTo.UserID) { return false; }
            if (orig.MemberActive != compareTo.MemberActive) { return false; }
            if (orig.LastUpdatedUTC != compareTo.LastUpdatedUTC) { return false; }
            if (orig.Email != compareTo.Email) { return false; }
            if (orig.CreatedByOrgID != compareTo.CreatedByOrgID) { return false; }
            if (orig.Address != compareTo.Address) { return false; }
            if (orig.NOKName != compareTo.NOKName) { return false; }
            if (orig.NOKRelation != compareTo.NOKRelation) { return false; }
            if (orig.NOKPhone != compareTo.NOKPhone) { return false; }
            if (orig.D4HStatus != compareTo.D4HStatus) { return false; }
            if (orig.QualificationList != compareTo.QualificationList) { return false; }
            return true;
        }







        public static string getBCSARAQualificationName(this Qualification q)
        {
            switch (q.Code)
            {
                case "GSAR": return "Ground Search and Rescue (GSAR) [Search]";
                case "GSARTL": return "Ground Search Team Leader (GSTL) [Search]";
                case "SARM1": return "Search and Rescue Manager 1 (post Nov 2014) [Search]";
                case "SARM2": return "Search and Rescue Manager 2 [Search]";
                case "FA7h": return "07+ Hour First Aid Training or Equivalent [Medical]";
                case "FA40h": return "32-50+ Hour First Aid Training or Equivalent [Medical]";
                case "FA90h": return "70+ Hour First Aid Training or Equivalent [Medical]";
                case "PARAM": return "Licensed Paramedic (annual licensing body continuing competency requirements) [Medical]";
                case "ALS": return string.Empty;
                case "FR": return string.Empty;
                case "SWO": return "Swiftwater Operations (6h annual practice + 3y re-certification) [Swiftwater]";
                case "SWTL": return "Swiftwater Rescue Technician (20h annual practice + 3y re-certification) [Swiftwater]";
                case "SWA": return "Swiftwater Rescue Technician - Advanced (20h annual practice + 3y re-certification) [Swiftwater]";
                case "RR1": return "Rope Rescue Technician 1 (20h annual practice) [Rope Rescue]";
                case "RR2": return "Rope Rescue Technician 2 (20h annual practice) [Rope Rescue]";
                case "RRTL": return "Rope Rescue Team Leader (20h annual practice) [Rope Rescue]";
                case "OARTM": return "Organized Avalanche Response Team Member [Avalanche and Ice]";
                case "OARTL": return "Organized Avalanche Response Team Leader [Avalanche and Ice]";
                case "TA": return "BCTA - 1 - Track Aware [Tracking]";
                case "TK": return "BCTA - 2 - Tracker [Tracking]";
                case "AT": return "BCTA - 3 - Advanced Tracker [Tracking]";
                case "MR1": return "Mountain Rescue 1 [Mountain Rescue]";
                case "MR2": return "Mountain Rescue 2 [Mountain Rescue]";
                case "MR3": return "Mountain Rescue 3 [Mountain Rescue]";
                case "ORV": return "Off Road Vehicle (ORV) Operations [Vehicles]";
                case "MARINE": return "Small Vessel Operator Proficiency [Boat]";
                case "CDFL": return "HOTP Level 3 - CDFL (annual re-certification) [Aircraft]";
                case "K9": return "K-9 Wilderness Certification [Dog Handler]";



            }
            return string.Empty;
        }

        public static List<Qualification> GetQualifications()
        {
            List<Qualification> qualifications = new List<Qualification>();
            qualifications.Add(new Qualification("GSAR", "Ground Search and Rescue", 0, 0, 0, ""));
            qualifications.Add(new Qualification("GSARTL", "GSAR Team Leader", 1, 0, 1, "GSTL"));
            qualifications.Add(new Qualification("SARM1", "Search Manager 1", 2, 0, 2, "SARM"));
            qualifications.Add(new Qualification("SARM2", "Search Manager 2", 3, 0, 8, "SARM"));
            qualifications.Add(new Qualification("FA7h", "First Aid (7+ hrs)", 4, 0, 3, ""));
            qualifications.Add(new Qualification("FA40h", "First Aid (40+ hrs)", 5, 0, 9, ""));
            qualifications.Add(new Qualification("FA90h", "First Aid (90+ hrs)", 6, 0, 10, "OFA3"));
            qualifications.Add(new Qualification("PARAM", "Paramedic", 7, 0, 11, "OFA3"));
            qualifications.Add(new Qualification("ALS", "ALS", 8, 0, 12, "OFA3"));
            qualifications.Add(new Qualification("FR", "First Responder", 9, 0, 13, "OFA3"));
            qualifications.Add(new Qualification("SWO", "Swift Water Operations", 10, 0, 14, "SSO"));
            qualifications.Add(new Qualification("SWTL", "Swift Water Tech", 11, 0, 6, "SRT"));
            qualifications.Add(new Qualification("SWA", "Swift Water Advanced", 12, 0, 15, "SRT"));
            qualifications.Add(new Qualification("RR1", "Rope Tech 1", 13, 0, 4, "RRTM"));
            qualifications.Add(new Qualification("RR2", "Rope Tech 2", 14, 0, 16, "RRTM"));
            qualifications.Add(new Qualification("RRTL", "Rope Team Leader", 15, 0, 17, "RRTL"));
            qualifications.Add(new Qualification("OARTM", "Avalanche Team Member", 16, 0, 18, "OAR"));
            qualifications.Add(new Qualification("OARTL", "Avalanche Team Leader", 17, 0, 19, "OARTL"));
            qualifications.Add(new Qualification("TA", "Track Aware", 18, 0, 20, "TA"));
            qualifications.Add(new Qualification("TK", "Tracker", 19, 0, 5, "TK"));
            qualifications.Add(new Qualification("AT", "Advanced Tracker", 20, 0, 21, "AT"));
            qualifications.Add(new Qualification("MR1", "Mountain Rescue 1", 21, 0, 7, "MR"));
            qualifications.Add(new Qualification("MR2", "Mountain Rescue 2", 22, 0, 22, "MR2"));
            qualifications.Add(new Qualification("MR3", "Mountain Rescue 3", 23, 0, 23, "MR3"));
            qualifications.Add(new Qualification("ORV", "Off Road Vehicle", 24, 0, 24, ""));
            qualifications.Add(new Qualification("MARINE", "Marine / Boat Operator", 25, 0, 25, ""));
            qualifications.Add(new Qualification("CDFL", "Hoist / CDFL", 26, 0, 26, ""));
            qualifications.Add(new Qualification("K9", "K-9 Search", 27, 0, 27, ""));

            return qualifications;
        }

        public static void SetQualificationByCode(this Personnel p, string code, bool value)
        {
            int index = GetQualificationIndexByCode(code);
            if (index >= 0) { p.QualificationList[index] = value; }
        }
        public static int GetQualificationIndexByCode(string code)
        {
            List<Qualification> qualifications = GetQualifications();
            if (qualifications.Any(o => o.Code.Equals(code)))
            {
                return qualifications.First(o => o.Code.Equals(code)).QualificationListIndex;

            }
            return -1;
        }
        public static Qualification? GetQualificationByCode(string code)
        {
            List<Qualification> qualifications = GetQualifications();
            if (qualifications.Any(o => o.Code.Equals(code)))
            {
                return qualifications.First(o => o.Code.Equals(code));

            }
            return null;
        }



        public static string ExportPersonnelToCSV(List<Personnel> personnel, string delimiter = ",")
        {
            List<Qualification> qualifications = GetQualifications();

            StringBuilder csv = new StringBuilder();
            csv.Append("Name"); csv.Append(delimiter);
            csv.Append("Pronouns"); csv.Append(delimiter);
            csv.Append("Group"); csv.Append(delimiter);
            csv.Append("Callsign"); csv.Append(delimiter);
            csv.Append("Phone"); csv.Append(delimiter);
            csv.Append("Email"); csv.Append(delimiter);

            csv.Append("Address"); csv.Append(delimiter);

            csv.Append("SpecialSkills"); csv.Append(delimiter);

            csv.Append("Reference"); csv.Append(delimiter);



            csv.Append("NOKName"); csv.Append(delimiter);
            csv.Append("NOKRelation"); csv.Append(delimiter);
            csv.Append("NOKPhone"); csv.Append(delimiter);
            csv.Append("Other Dietary"); csv.Append(delimiter);
            csv.Append("Vegetarian"); csv.Append(delimiter);
            csv.Append("NoGluten"); csv.Append(delimiter);
            // csv.Append("Phone"); csv.Append(delimiter);


            foreach (Qualification q in qualifications)
            {
                csv.Append(q.FullName); csv.Append(delimiter);
            }

            csv.Append(Environment.NewLine);

            foreach (Personnel item in personnel)
            {

                csv.Append("\""); csv.Append(item.Name.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.Pronouns.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.Group.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.Callsign.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.Phone.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.Email.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.Address.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.SpecialSkills.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.Reference.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.NOKName.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.NOKRelation.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); csv.Append(item.NOKPhone.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);

                csv.Append("\""); csv.Append(item.Dietary.EscapeQuotes()); csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); if (item.Vegetarian) { csv.Append("YES"); } else { csv.Append("NO"); }
                csv.Append("\""); csv.Append(delimiter);
                csv.Append("\""); if (item.NoGluten) { csv.Append("YES"); } else { csv.Append("NO"); }
                csv.Append("\""); csv.Append(delimiter);

                foreach (Qualification q in qualifications)
                {
                    csv.Append("\"");
                    if (item.QualificationList != null && item.QualificationList.Length >= q.QualificationListIndex)
                    {
                        if (item.QualificationList[q.QualificationListIndex]) { csv.Append("YES"); } else { csv.Append("NO"); }

                    }
                    csv.Append("\""); csv.Append(delimiter);
                }


                csv.Append(Environment.NewLine);
            }
            return csv.ToString();
        }





        public static Personnel? getMemberFromSimplifiedQR(string qr)
        {
            if (!string.IsNullOrEmpty(qr))
            {
                Personnel member = new Personnel();
                string[] bits = qr.Split(';');
                if (bits.Length > 0)
                {
                    // member.PersonID = new Guid(bits[0]);
                    member.Name = bits[0];
                    try
                    {
                        string shortOrgID = bits[1];
                        List<Organization> allOrgs = OrganizationTools.GetOrganizations(Guid.Empty);// new Organization().getStaticOrganizationList();
                        if (allOrgs.Any(o => o.ShortOrganizationID == shortOrgID))
                        {
                            Organization org = allOrgs.First(o => o.ShortOrganizationID == shortOrgID);
                            member.OrganizationID = org.OrganizationID;
                            member.Group = org.OrganizationName;
                        }
                    }
                    catch (Exception) { }

                    member.Address = bits[2];
                    member.Phone = bits[3];
                    member.Email = bits[4];

                    //quals are 7
                    if (bits[5].Length >= 1)
                    {
                        int qualifications = 0;
                        if (int.TryParse(bits[5], out qualifications))
                        {
                            string binary = Convert.ToString(qualifications, 2);
                            while (binary.Length < 8)
                            {
                                binary = "0" + binary;
                            }

                            member.SetQualificationByCode("GSAR", binary.Substring(0, 1).Equals("1"));
                            member.SetQualificationByCode("GSARTL", binary.Substring(1, 1).Equals("1"));
                            member.SetQualificationByCode("SARM1", binary.Substring(2, 1).Equals("1"));
                            member.SetQualificationByCode("FA7h", binary.Substring(3, 1).Equals("1"));
                            member.SetQualificationByCode("RR1", binary.Substring(4, 1).Equals("1"));
                            member.SetQualificationByCode("TK", binary.Substring(5, 1).Equals("1"));
                            if (binary.Length > 6)
                            {
                                member.SetQualificationByCode("SWT", binary.Substring(6, 1).Equals("1"));
                            }
                            if (binary.Length > 7)
                            {
                                member.SetQualificationByCode("MR1", binary.Substring(7, 1).Equals("1"));
                            }

                        }


                    }
                    member.NOKName = bits[6];
                    member.NOKPhone = bits[7];
                }

                return member;
            }
            else { return null; }
        }




        private static bool YesOrFuture(string fieldText)
        {

            if (string.IsNullOrEmpty(fieldText)) { return false; }
            else if (fieldText.Equals("Yes")) { return true; }
            else
            {
                DateTime test = DateTime.MinValue;
                if (DateTime.TryParse(fieldText, out test))
                {
                    if (test > DateTime.Now) { return true; }
                    else { return false; }
                }
                else { return false; }
            }
        }



    }
}

