namespace MySarAssistModels.Clues
{
    public static class ClueTools
    {
        public static List<ClueValueOption> GetClueValueOptions()
        {
            List<ClueValueOption> options = new List<ClueValueOption>();
            options.Add(new ClueValueOption() { ClueValueId = 1, ClueValueName = "Absolutely related" });
            options.Add(new ClueValueOption() { ClueValueId = 2, ClueValueName = "Likely related" });
            options.Add(new ClueValueOption() { ClueValueId = 3, ClueValueName = "Possibly related" });
            options.Add(new ClueValueOption() { ClueValueId = 4, ClueValueName = "Not likely related" });
            return options;
        }

        public static ClueValueOption? GetValueOptionById(int Id)
        {
            return GetClueValueOptions().FirstOrDefault(o => o.ClueValueId == Id);
            
        }
    }
}
