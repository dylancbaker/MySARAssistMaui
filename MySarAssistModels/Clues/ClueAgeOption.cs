namespace MySarAssistModels.Clues
{
    public class ClueAgeOption
    {
        private int _Id;
        private string _AgeName = string.Empty;

        public int Id { get => _Id; set => _Id = value; }
        public string AgeName { get => _AgeName; set => _AgeName = value; }
    }
}
