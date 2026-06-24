using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Models;
using MarsAdvancedAutomation.Assertions;
using NUnit.Framework;
using MarsAdvancedAutomation.Pages.ProfilePage;

namespace MarsAdvancedAutomation.Tests
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
                @"TestData\ProfileOverviewData.json");

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
                @"TestData\ProfileOverviewData.json");

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
                    @"TestData\ProfileOverviewData.json");

            ProfileOverviewComponent profile =
                new ProfileOverviewComponent(driver);

            profile.UpdateEarnTarget(data.EarnTarget);

            ProfileOverviewAssertions.VerifyEarnTarget(
                data.EarnTarget,
                profile.GetSelectedEarnTarget());
        }
    }
}