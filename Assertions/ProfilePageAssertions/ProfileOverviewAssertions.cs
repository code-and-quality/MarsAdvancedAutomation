using NUnit.Framework;

namespace MarsAdvancedAutomation.Assertions
{
    public static class ProfileOverviewAssertions
    {
        public static void VerifyAvailability(
            string expected,
            string actual)
        {
            Assert.That(
                actual,
                Is.EqualTo(expected),
                "Availability was not updated correctly");
        }
        public static void VerifyHours(
            string expected,
            string actual)
        {
            Assert.That(
                actual,
                Is.EqualTo(expected),
                "Hours were not updated correctly");
        }

        public static void VerifyEarnTarget(
            string expected,
            string actual)
        {
            Assert.That(
                actual,
                Is.EqualTo(expected),
                "Earn Target was not updated correctly");
        }
    }
}