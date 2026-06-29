using NUnit.Framework;

namespace MarsAdvancedAutomation.Assertions.ProfilePageAssertions
{
    public static class LanguagesAssertions
    {
        public static void VerifyLanguageAdded(bool isPresent, string language)
        {
            Assert.That(
                isPresent,
                Is.True,
                $"Language '{language}' was not added successfully."
            );
        }

        public static void VerifyLanguageUpdated(bool isPresent, string language)
        {
            Assert.That(
                isPresent,
                Is.True,
                $"Language was not updated to '{language}'."
            );
        }

        public static void VerifyLanguageDeleted(bool isPresent, string language)
        {
            Assert.That(
                isPresent,
                Is.False,
                $"Language '{language}' was not deleted successfully."
            );
        }

        public static void VerifyDuplicateLanguageMessage(string actualMessage)
        {
            Assert.That(
                actualMessage,
                Does.Contain("already exist"),
                "Duplicate language validation message was not displayed."
            );
        }

        public static void VerifyMaximumLanguagesReached(bool isAddNewButtonVisible)
        {
            Assert.That(
                isAddNewButtonVisible,
                Is.False,
                "Add New button should not be visible after adding 4 languages."
            );
        }
    }
}