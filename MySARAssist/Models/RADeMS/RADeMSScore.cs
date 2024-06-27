using System.Text;

namespace MySARAssist.Models.RADeMS
{
    public class RADeMSScore : SyncableItem, ICloneable
    {
         private Guid _SetByPositionID;
         private string _SetByName;
         private int[] _Scores = new int[10];
         private int _CategoryID;
         private string _Comment;
         private int _ManualOpRisk = -1;
         private int _ManualRespCap = -1;

        public DateTime LastUpdatedLocal { get => LastUpdatedUTC.ToLocalTime(); }
        public Guid SetByPositionID { get => _SetByPositionID; set => _SetByPositionID = value; }
        public string SetByName { get => _SetByName; set => _SetByName = value; }
        public int[] Scores { get => _Scores; set => _Scores = value; }
        public int CategoryID { get => _CategoryID; set => _CategoryID = value; }
        public string Comment { get => _Comment; set => _Comment = value; }
        public int ManualOpRisk { get => _ManualOpRisk; set => _ManualOpRisk = value; }
        public int ManualRespCap { get => _ManualRespCap; set => _ManualRespCap = value; }

        public RADeMSScore()
        {
            ID = Guid.NewGuid();
            LastUpdatedUTC = DateTime.UtcNow;
            Active = true;
        }

        public int OperationalRisk
        {
            get
            {
                if (ManualOpRisk >= 0) { return ManualOpRisk; }
                return CalculatedOperationalRisk;
            }
        }
        public int CalculatedOperationalRisk
        {
            get
            {
                return Scores[0] + Scores[1] + Scores[2] + Scores[3] + Scores[4];

            }
        }
        public int ResponseCapacity
        {
            get
            {
                if (ManualRespCap >= 0) { return ManualRespCap; }
                return CalculatedResponseCapacity;

            }
        }
        public int CalculatedResponseCapacity
        {
            get
            {
                return Scores[5] + Scores[6] + Scores[7] + Scores[8] + Scores[9];
            }
        }

        public string FullTextForLog
        {
            get
            {
                StringBuilder fullText = new StringBuilder();
                fullText.Append("RADeMS ");
                fullText.Append(" Op. Risk: "); fullText.Append(OperationalRisk);
                fullText.Append(", Response Cap.: "); fullText.Append(ResponseCapacity);
                fullText.Append(" | Conducted ");
                fullText.Append(LastUpdatedLocal.ToString("yyyy-MMM-dd HH:mm"));
                if (!string.IsNullOrEmpty(SetByName)) { fullText.Append(" by "); fullText.Append(SetByName); }
                if (!string.IsNullOrEmpty(Comment)) { fullText.Append(" "); fullText.Append(Comment); }
                return fullText.ToString();
            }
        }



        public string ShortText
        {
            get
            {
                StringBuilder fullText = new StringBuilder();
                fullText.Append(" Op. Risk: "); fullText.Append(OperationalRisk);
                fullText.Append(", Response Cap.: "); fullText.Append(ResponseCapacity);
                return fullText.ToString();
            }
        }

        public string VeryShortText
        {
            get
            {
                StringBuilder fullText = new StringBuilder();
                fullText.Append(OperationalRisk);
                fullText.Append(" x "); fullText.Append(ResponseCapacity);
                return fullText.ToString();

            }
        }
        public Color ScoreColor
        {
            get
            {
                Color color = Color.White;




                return color;
            }
        }

        public RADeMSScore Clone()
        {
            return this.MemberwiseClone() as RADeMSScore;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
