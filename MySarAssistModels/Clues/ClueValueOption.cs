namespace MySarAssistModels.Clues
{
    public class ClueValueOption
    {
        private int _Id;
        private string _ValueName = string.Empty;

        public int Id { get => _Id; set => _Id = value; }
        public string ValueName { get => _ValueName; set => _ValueName = value; }
    }
}
