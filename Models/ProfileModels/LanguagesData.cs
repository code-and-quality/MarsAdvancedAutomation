namespace MarsAdvancedAutomation.Models
{
    public class LanguagesAddModel
    {
        public string Language { get; set; }
        public string Level { get; set; }
    }

    public class LanguagesUpdateModel
    {
        public string ExistingLanguage { get; set; }
        public string NewLanguage { get; set; }
        public string NewLevel { get; set; }
    }

    public class LanguagesDeleteModel
    {
        public string Language { get; set; }
    }
    public class MaximumLanguagesModel
    {
        public string Language1 { get; set; }
        public string Language2 { get; set; }
        public string Language3 { get; set; }
        public string Language4 { get; set; }
        public string Language5 { get; set; }
        public string Level { get; set; }
    }

    public class LanguagesData
    {
        public LanguagesAddModel Add { get; set; }

        public LanguagesUpdateModel Update { get; set; }

        public LanguagesDeleteModel Delete { get; set; }

        public MaximumLanguagesModel MaximumLanguages { get; set; }
    }
}