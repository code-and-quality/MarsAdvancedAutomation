using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Models;
using MarsAdvancedAutomation.Pages.LoginPage;
using OpenQA.Selenium;

namespace MarsAdvancedAutomation.Helpers
{
    public class LoginHelper
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        public LoginHelper(IWebDriver driver)
        {
            this.driver = driver;
            this.loginPage = new LoginPage(driver);
        }

        public void LoginAsValidUser()
        {
            var data = JsonReader.ReadJson<LoginTestData>(
                @"TestData/LoginData.json");

            loginPage.Login(data.Valid.Email, data.Valid.Password);
        }

        public void LoginAsInvalidUser()
        {
            var data = JsonReader.ReadJson<LoginTestData>(
                @"TestData/LoginData.json");

            loginPage.Login(data.Invalid.Email, data.Invalid.Password);
        }
    }
}