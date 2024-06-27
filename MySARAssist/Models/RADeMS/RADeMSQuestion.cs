namespace MySARAssist.Models.RADeMS
{
  
    public class RADeMSQuestion
    {
         private int _ID;
         private string _QuestionText;
         private bool _IsOperationalRisk;
         private string _EgOption1;
         private string _EgOption2;
         private string _EgOption3;
         private string _Option1Label;
         private string _Option2Label;
         private string _Option3Label;
         private string _Description;
         private int _CategoryID;

        public int ID { get => _ID; set => _ID = value; }
        public string QuestionText { get => _QuestionText; set => _QuestionText = value; }
        public bool IsOperationalRisk { get => _IsOperationalRisk; set => _IsOperationalRisk = value; }
        public string EgOption1 { get => _EgOption1; set => _EgOption1 = value; }
        public string EgOption2 { get => _EgOption2; set => _EgOption2 = value; }
        public string EgOption3 { get => _EgOption3; set => _EgOption3 = value; }

        public string[] EgOption1List { get { return OptionAsList(0); } }
        public string EgOption1Bullets { get => OptionsWithBullets(0); }
        public string[] EgOption2List { get { return OptionAsList(1); } }
        public string EgOption2Bullets { get => OptionsWithBullets(1); }
        public string[] EgOption3List { get { return OptionAsList(2); } }
        public string EgOption3Bullets { get => OptionsWithBullets(2); }

        private string[] OptionAsList(int num)
        {
            string str = null;
            switch (num)
            {
                case 0: str = EgOption1; break;
                case 1: str = EgOption2; break;
                case 2: str = EgOption3; break;
            }
            if (!string.IsNullOrEmpty(str))
            {
                string[] lst = str.Split('~');
                return lst;
            }
            else { return new string[0]; }
        }
        private string OptionsWithBullets(int num)
        {
            string str = null;
            switch (num)
            {
                case 0: str = EgOption1; break;
                case 1: str = EgOption2; break;
                case 2: str = EgOption3; break;
            }
            if (!string.IsNullOrEmpty(str))
            {
                return " • " + str.Replace("~", Environment.NewLine + " • ");
            }
            return null;
        }

        public string Option1Label { get => _Option1Label; set => _Option1Label = value; }
        public string Option2Label { get => _Option2Label; set => _Option2Label = value; }
        public string Option3Label { get => _Option3Label; set => _Option3Label = value; }
        public string Description { get => _Description; set => _Description = value; }
        public int CategoryID { get => _CategoryID; set => _CategoryID = value; }

        public RADeMSQuestion() { }
        public RADeMSQuestion(int id, int categoryid, string question, string desc, bool isOpRisk, string eg1, string eg2, string eg3)
        {
            _ID = id;
            _CategoryID = categoryid;
            _QuestionText = question;
            _Description = desc;
            _IsOperationalRisk = isOpRisk;
            EgOption1 = eg1;
            EgOption2 = eg2;
            EgOption3 = eg3;
            if (isOpRisk)
            {
                Option1Label = "Low";
                Option2Label = "Moderate";
                Option3Label = "High";
            }
            else
            {
                Option1Label = "High";
                Option2Label = "Moderate";
                Option3Label = "Low";

            }
        }
    }
}
