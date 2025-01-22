using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels
{
    public abstract class SyncableItem
    {
        private Guid _ID;
        private bool _Active;
        private int _OpPeriod;
        private DateTime _LastUpdatedUTC;
        private string? _IncidentIdentifier;
        private DateTime _DateCreatedUTC;
        private string? _CreatedBy;



        [PrimaryKey]
        public Guid ID { get { return _ID; } set => _ID = value; }
        public bool Active { get => _Active; set => _Active = value; }
        public int OpPeriod { get => _OpPeriod; set => _OpPeriod = value; }
        public DateTime LastUpdatedUTC { get => _LastUpdatedUTC; set => _LastUpdatedUTC = value; }

        public DateTime DateCreatedUTC { get => _DateCreatedUTC; set => _DateCreatedUTC = value; }
        public string? IncidentIdentifier { get => _IncidentIdentifier; set => _IncidentIdentifier = value; }
        public string? CreatedBy { get => _CreatedBy; set => _CreatedBy = value; }

        public SyncableItem() { ID = Guid.NewGuid(); LastUpdatedUTC = DateTime.UtcNow; DateCreatedUTC = LastUpdatedUTC;  Active = true; }
    }
}
