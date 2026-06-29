using NUnit.Framework;

namespace MarsAdvancedAutomation.Assertions.ProfilePageAssertions
{
    public static class SkillsAssertions
    {
        public static void VerifySkillAdded(bool isPresent, string skill)
        {
            Assert.That(
                isPresent,
                Is.True,
                $"Skill '{skill}' was not added successfully."
            );
        }

        public static void VerifySkillUpdated(bool isPresent, string skill)
        {
            Assert.That(
                isPresent,
                Is.True,
                $"Skill was not updated to '{skill}'."
            );
        }

        public static void VerifySkillDeleted(bool isPresent, string skill)
        {
            Assert.That(
                isPresent,
                Is.False,
                $"Skill '{skill}' was not deleted successfully."
            );
        }

        public static void VerifyDuplicateSkillMessage(string actualMessage)
        {
            Assert.That(
                actualMessage,
                Does.Contain("This skill is already exist in your skill list."),
                "Duplicate skill validation message was not displayed."
            );
        }
    }
}