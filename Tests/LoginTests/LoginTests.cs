using MarsAdvancedAutomation.Assertions;
using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using NUnit.Framework;

namespace MarsAdvancedAutomation.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        private LoginHelper loginHelper;

        [SetUp]
        public void SetupTest()
        {
            loginHelper = new LoginHelper(driver);
        }

        
        [Test]
        public void LoginSuccessfully()
        {
            loginHelper.LoginAsValidUser();

            LoginAssertions.VerifySuccessfulLogin(driver);

            ExtentReportManager.Test.Pass("Login successful");
        }

     
        [Test]
        public void LoginWithInvalidCredentials()
        {
            ExtentReportManager.Test =
                ExtentReportManager.Extent.CreateTest("Login With Invalid Credentials");

            loginHelper.LoginAsInvalidUser();

            LoginAssertions.VerifyInvalidLogin(driver);

            ExtentReportManager.Test.Pass(
                "Invalid login validation completed successfully.");
        }
    }
}


























//using MarsAdvancedAutomation.Assertions;
//using MarsAdvancedAutomation.Base;
//using MarsAdvancedAutomation.Helpers;

//namespace MarsAdvancedAutomation.Tests
//{
//    public class LoginTests : BaseTest
//    {
//        private LoginHelper loginHelper;

//        [SetUp]
//        public void SetupTest()
//        {
//            loginHelper = new LoginHelper(driver);
//        }

//        [Test]
//        public void LoginSuccessfully()
//        {
//            loginHelper.LoginAsValidUser();

//            LoginAssertions.VerifySuccessfulLogin(driver);
//        }

//        [Test]
//        public void LoginWithInvalidCredentials()
//        {
//            loginHelper.LoginAsInvalidUser();

//            LoginAssertions.VerifyInvalidLogin(driver);
//        }
//    }

//}


