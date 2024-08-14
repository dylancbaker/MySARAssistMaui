

using MySarAssistModels.People;

namespace MySarAssistModels.Interfaces
{
    public interface IPersonnel
    {
        string? Address { get; set; }
        string? AddressWithPhone { get; }
        string? Barcode { get; set; }
        bool BoatOperator { get; }
        string? Callsign { get; set; }
        bool CDFL { get; }
        Guid CreatedByOrgID { get; set; }
        bool CurrentlySelected { get; set; }
        int D4HID { get; set; }
        string? D4HStatus { get; set; }
        string? Dietary { get; set; }
        string Email { get; set; }
        bool FirstAid { get; }
        bool FirstAid40Plus { get; }
        string? Group { get; set; }
        bool GSAR { get; }
        bool GSTL { get; }
        bool isAssignmentTeamLeader { get; set; }
        bool K9 { get; }
        bool MemberActive { get; set; }
        Organization? MemberOrganization { get; set; }
        bool MountainRescue { get; }
        string Name { get; set; }
        string? NameWithGroup { get; }
        string? NameWithGroupAndCurrent { get; }
        string? NameWithSARM { get; }
        string? NameWithTL { get; }
        bool NoGluten { get; set; }
        string? NOKName { get; set; }
        string? NOKOneLine { get; }
        string? NOKPhone { get; set; }
        string? NOKRelation { get; set; }
        bool OffRoadVehicleOperator { get; }
        Guid OrganizationID { get; set; }
        double PacesPer100 { get; set; }
        Guid PersonID { get; set; }
        string? Phone { get; set; }
        string? Pronouns { get; set; }
        bool Qualification0 { get; set; }
        bool Qualification1 { get; set; }
        bool Qualification10 { get; set; }
        bool Qualification11 { get; set; }
        bool Qualification12 { get; set; }
        bool Qualification13 { get; set; }
        bool Qualification14 { get; set; }
        bool Qualification15 { get; set; }
        bool Qualification16 { get; set; }
        bool Qualification17 { get; set; }
        bool Qualification18 { get; set; }
        bool Qualification19 { get; set; }
        bool Qualification2 { get; set; }
        bool Qualification20 { get; set; }
        bool Qualification21 { get; set; }
        bool Qualification22 { get; set; }
        bool Qualification23 { get; set; }
        bool Qualification24 { get; set; }
        bool Qualification25 { get; set; }
        bool Qualification26 { get; set; }
        bool Qualification27 { get; set; }
        bool Qualification3 { get; set; }
        bool Qualification4 { get; set; }
        bool Qualification5 { get; set; }
        bool Qualification6 { get; set; }
        bool Qualification7 { get; set; }
        bool Qualification8 { get; set; }
        bool Qualification9 { get; set; }
        bool[] QualificationList { get; set; }
        string? Reference { get; set; }
        bool RopeRescue { get; }
        bool SARM { get; }
        bool SignedInToTask { get; set; }
        string? SpecialSkills { get; set; }
        bool Swiftwater { get; }
        bool Tracker { get; }
        Guid UserID { get; set; }
        bool Vegetarian { get; set; }

        Personnel? Clone();
        bool Equals(Personnel other);
    }
}