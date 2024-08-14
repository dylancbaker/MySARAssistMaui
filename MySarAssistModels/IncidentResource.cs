using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels
{
    public class IncidentResource : SyncableItem, ICloneable
    {
        private string? _ResourceName;
        private string? _Kind;
        private string? _Type;
        private string? _ResourceIdentifier;
        private int _NumberOfPeople;
        private int _NumberOfVehicles;
        private string? _LeaderName;
        private string? _Contact;
        private int _UniqueIDNum;
        private string? _ResourceType;
        private Guid _ParentResourceID;

        public string? ResourceName { get => _ResourceName; set => _ResourceName = value; }
        public string? Kind { get => _Kind; set => _Kind = value; }
        public string? Type { get => _Type; set => _Type = value; }
        public string? ResourceIdentifier { get => _ResourceIdentifier; set => _ResourceIdentifier = value; }
        public int NumberOfPeople { get => _NumberOfPeople; set => _NumberOfPeople = value; }
        public int NumberOfVehicles { get => _NumberOfVehicles; set => _NumberOfVehicles = value; }

        public string? LeaderName { get => _LeaderName; set => _LeaderName = value; }
        public string? Contact { get => _Contact; set => _Contact = value; }
        public int UniqueIDNum { get => _UniqueIDNum; set => _UniqueIDNum = value; }
        public string? ResourceType { get => _ResourceType; set => _ResourceType = value; }
        public Guid ParentResourceID { get => _ParentResourceID; set => _ParentResourceID = value; }

        public string UniqueIDNumWithPrefix
        {
            get
            {
                switch (ResourceType)
                {
                    case "Personnel": return "P" + UniqueIDNum;
                    case "Vehicle": return "V" + UniqueIDNum;
                    case "Equipment": return "E" + UniqueIDNum;
                    case "Crew": return "C" + UniqueIDNum;
                    case "Heavy Equipment Crew": return "C" + UniqueIDNum;
                    default: return UniqueIDNum.ToString();
                }
            }
        }
        public string NameWithKindAndType
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ResourceName);
                if (!string.IsNullOrEmpty(Kind) || !string.IsNullOrEmpty(Type)) { sb.Append(" - "); }
                if (!string.IsNullOrEmpty(Kind))
                {
                    sb.Append(Kind);
                    if (!string.IsNullOrEmpty(Type)) { sb.Append(" / "); }
                }
                if (!string.IsNullOrEmpty(Type)) { sb.Append(Type); }

                return sb.ToString();
            }
        }

        public IncidentResource()
        {
            Active = true;
            LastUpdatedUTC = DateTime.UtcNow;
        }

        public IncidentResource? Clone()
        {
            if (this != null)
            {
                IncidentResource? cloneTo = this.MemberwiseClone() as IncidentResource;

                return cloneTo;
            }
            return null;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }


    }
}
