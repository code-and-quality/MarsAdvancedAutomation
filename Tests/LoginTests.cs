using MarsAdvancedAutomation.Assertions;
using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;

namespace MarsAdvancedAutomation.Tests
{
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
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            loginHelper.LoginAsInvalidUser();

            LoginAssertions.VerifyInvalidLogin(driver);
        }
    }

}



/*using MarsAdvancedAutomation.Assertions;
using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Models;
using MarsAdvancedAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MarsAdvancedAutomation.Tests
{
    public class LoginTests
    {
        private LoginPage loginPage;
        protected IWebDriver driver;

        [SetUp]
        public void TestSetup()
        {
            driver = DriverManager.GetDriver();
            driver.Navigate().GoToUrl("http://localhost:5003/");
            loginPage = new LoginPage(driver);
        }

        [Test]
        public void LoginSuccessfully()
        {
            var data = JsonReader.ReadJson<LoginTestData>("TestData/LoginData.json");

            loginPage.Login(data.Valid.Email, data.Valid.Password);

            LoginAssertions.VerifySuccessfulLogin(driver);
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            var data = JsonReader.ReadJson<LoginTestData>("TestData/LoginData.json");

            loginPage.Login(data.Invalid.Email, data.Invalid.Password);

            string actualMessage = loginPage.GetLoginErrorMessage();

            LoginAssertions.VerifyInvalidLogin(driver);
        }
    }
}



/*using NUnit.Framework;
using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Pages;
using MarsAdvancedAutomation.Models;
using MarsAdvancedAutomation.Helpers;
using System.IO;

namespace MarsAdvancedAutomation.Tests
{
    public class LoginTests : BaseTest
    {
        private LoginPage loginPage;

        [SetUp]
        public void TestSetup()
        {
            driver.Navigate().GoToUrl("http://localhost:5003/");
            loginPage = new LoginPage(driver);
        }

        // -------------------------
        // VALID LOGIN TEST
        // -------------------------
        [Test]
        public void LoginSuccessfully()
        {
            //var json = File.ReadAllText(@"TestData\LanguagesData.json");
            //dynamic data = JsonConvert.DeserializeObject(json);
            var data = MarsAdvancedAutomation.Helpers.JsonReader.ReadJson<LoginTestData>(
             @"TestData\LoginData.json");

            loginPage.Login(
                data.Valid.Email,
                data.Valid.Password);

            // Example assertion (change based on your app)
            Assert.That(driver.Url, Does.Contain("profile"));
        }

        // -------------------------
        // INVALID LOGIN TEST
        // -------------------------
        [Test]
        public void LoginWithInvalidCredentials()
        {
            var data = MarsAdvancedAutomation.Helpers.JsonReader.ReadJson<LoginTestData>(
            @"TestData\LoginData.json");

            loginPage.Login(
                data.Invalid.Email,
                data.Invalid.Password);

            string actualMessage = loginPage.GetLoginErrorMessage();

            Console.WriteLine("Error: " + actualMessage);

            Assert.That(
                actualMessage,
                Does.Contain("check your email")
            );
        }
    }
}

/*using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Models;
using MarsAdvancedAutomation.Pages;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MarsAdvancedAutomation.Tests
{
    public class LoginTests : BaseTest
    {
        [Test]
        public void LoginSuccessfully()
        {
            driver.Navigate().GoToUrl("http://localhost:5003/");

            var json = File.ReadAllText(@"TestData\LoginData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LoginPage loginPage =
                new LoginPage(driver);

            loginPage.Login(
                            (string)data.Valid.Email,
                            (string)data.Valid.Password);

            Assert.Pass();
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            driver.Navigate().GoToUrl("http://localhost:5003/");

            var json = File.ReadAllText(@"TestData\LoginData.json");
            dynamic data = JsonConvert.DeserializeObject(json);


            LoginPage loginPage =
                new LoginPage(driver);

            loginPage.Login(
                (string)data.Invalid.Email,
                (string)data.Invalid.Password);

            string actualMessage =
                loginPage.GetLoginErrorMessage();

            Assert.That(
                actualMessage,
                Does.Contain("invalid")
            );
        }
    }
}*/