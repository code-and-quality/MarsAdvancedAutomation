namespace MarsAdvancedAutomation.Models.ProfileModels
{
    public class SkillsAddModel
    {
        public string Skill { get; set; }
        public string Level { get; set; }
    }

    public class SkillsUpdateModel
    {
        public string ExistingSkill { get; set; }
        public string NewSkill { get; set; }
        public string NewLevel { get; set; }
    }

    public class SkillsDeleteModel
    {
        public string Skill { get; set; }
    }
}