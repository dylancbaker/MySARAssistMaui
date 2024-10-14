namespace MySarAssistModels.Clues
{
    public static class ClueTools
    {
        #region Clue Values
        public static List<ClueValueOption> GetClueValueOptions()
        {
            List<ClueValueOption> options = new List<ClueValueOption>();
            options.Add(new ClueValueOption() { Id = 1, ValueName = "Absolutely related" });
            options.Add(new ClueValueOption() { Id = 2, ValueName = "Likely related" });
            options.Add(new ClueValueOption() { Id = 3, ValueName = "Possibly related" });
            options.Add(new ClueValueOption() { Id = 4, ValueName = "Not likely related" });
            return options;
        }

        public static ClueValueOption? GetValueOptionById(int Id)
        {
            return GetClueValueOptions().FirstOrDefault(o => o.Id == Id);
            
        }
        #endregion

        #region Ages
        public static List<ClueAgeOption> GetClueAgeOptions()
        {
            List<ClueAgeOption> options = new List<ClueAgeOption>();
            options.Add(new ClueAgeOption() { Id = 1, AgeName = "Unknown" });
            options.Add(new ClueAgeOption() { Id = 2, AgeName = "Years" });
            options.Add(new ClueAgeOption() { Id = 3, AgeName = "Months" });
            options.Add(new ClueAgeOption() { Id = 4, AgeName = "Days" });
            options.Add(new ClueAgeOption() { Id = 4, AgeName = "Hours" });
            return options;
        }

        public static ClueAgeOption? GetAgeOptionById(int Id)
        {
            return GetClueAgeOptions().FirstOrDefault(o => o.Id == Id);

        }
        #endregion
    }
}
