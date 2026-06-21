using System.Collections.Generic;

namespace MarsAdvancedAutomation.Helpers
{
    public static class TestDataManager
    {
        public static List<string> LanguagesAdded { get; } = new();
        public static List<string> SkillsAdded { get; } = new();
        public static List<string> CertificationsAdded { get; } = new();
        public static List<string> EducationsAdded { get; } = new();

        public static void AddLanguage(string language)
        {
            if (!LanguagesAdded.Contains(language))
                LanguagesAdded.Add(language);
        }

        public static void AddSkill(string skill)
        {
            if (!SkillsAdded.Contains(skill))
                SkillsAdded.Add(skill);
        }

        public static void Clear()
        {
            LanguagesAdded.Clear();
            SkillsAdded.Clear();
            CertificationsAdded.Clear();
            EducationsAdded.Clear();
        }
    }
}