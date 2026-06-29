using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using NUnit.Framework;
using MarsAdvancedAutomation.Pages.ProfilePage;
using MarsAdvancedAutomation.Assertions.ProfilePageAssertions;
using MarsAdvancedAutomation.Models.ProfileModels;

namespace MarsAdvancedAutomation.Tests.ProfileTests
{
    public class ProfileOverviewTests : BaseTest
    {
        private LoginHelper loginHelper;
        private ProfileOverviewComponent ProfileOverview;

        [SetUp]
        public void SetupTest()
        {
            loginHelper = new LoginHelper(driver);
            ProfileOverview = new ProfileOverviewComponent(driver);

            // LOGIN
            loginHelper.LoginAsValidUser();
        }
        [Test]
        public void UpdateAvailabilitySuccessfully()
        {
            var data =
                JsonReader.ReadJson<ProfileOverviewData>(
                @"TestData\ProfileOverview\Availability.json");

            ProfileOverviewComponent profile =
                new ProfileOverviewComponent(driver);


            Console.WriteLine(driver.Url);

            profile.UpdateAvailability( data.Availability);

            ProfileOverviewAssertions.VerifyAvailability( data.Availability,
                                                          profile.GetSelectedAvailability());
        }
        [Test]
        public void UpdateHoursSuccessfully()
        {
            var data =
                JsonReader.ReadJson<ProfileOverviewData>(
                @"TestData\ProfileOverview\Hours.json");

            ProfileOverviewComponent profile =
                new ProfileOverviewComponent(driver);

            profile.UpdateHours(data.Hours);

            ProfileOverviewAssertions.VerifyHours(data.Hours,
                profile.GetSelectedHours());
        }

        [Test]
        public void UpdateEarnTargetSuccessfully()
        {
            var data =
                JsonReader.ReadJson<ProfileOverviewData>(
                    @"TestData\ProfileOverview\EarnTarget.json");

            ProfileOverviewComponent profile =
                new ProfileOverviewComponent(driver);

            profile.UpdateEarnTarget(data.EarnTarget);

            ProfileOverviewAssertions.VerifyEarnTarget(
                data.EarnTarget,
                profile.GetSelectedEarnTarget());
        }
    }
}