using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels.People
{
    public class Organization : IEquatable<Organization>
    {
        private Guid _organizationID;
        private string? _organizationName;
        private string? _primaryEmail;
        private string? _primaryPassword;
        private int _userCount = 0;
        private string? _logoFlieName;
        private Guid _ParentOrganizationID;


        public Organization() { }
        public Organization(Guid id, string name, string logo = null)
        {
            OrganizationID = id; OrganizationName = name;
            if (!string.IsNullOrEmpty(logo))
            {
                LogoFileName = logo;
            }
            else { LogoFileName = "BCSARA-Logo-960.png"; }
        }
        public Organization(Guid id, Guid parentid, string name, string logo = null)
        {
            OrganizationID = id; OrganizationName = name;
            ParentOrganizationID = parentid;

            if (!string.IsNullOrEmpty(logo))
            {
                LogoFileName = logo;
            }
            else if (ParentOrganizationID == new Guid("14CC75FE-75D3-44EE-B622-0C0727160675")) { LogoFileName = "saralberta-logo.png"; }
            else { LogoFileName = "BCSARA -Logo-960.png"; }
        }







        public Guid OrganizationID { get => _organizationID; set => _organizationID = value; }
        public Guid ParentOrganizationID { get => _ParentOrganizationID; set => _ParentOrganizationID = value; }
        public string ShortOrganizationID
        {
            get
            {
                return Convert.ToBase64String(OrganizationID.ToByteArray());

            }
        }


        public string? OrganizationName { get => _organizationName; set => _organizationName = value; }

        public string? PrimaryEmail { get => _primaryEmail; set => _primaryEmail = value; }


        public string? PrimaryPassword { get => _primaryPassword; set => _primaryPassword = value; }

        public int UserCount { get => _userCount; set => _userCount = value; }


        public string? LogoFileName { get => _logoFlieName; set => _logoFlieName = value; }

        public bool Equals(Organization other)
        {
            if (other == null) { return false; }
            return other.OrganizationID == OrganizationID;
        }
    }
}
