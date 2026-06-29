using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Models.LoginModels;
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
            var data = JsonReader.ReadJson<ValidCredentials>(
                @"TestData/Login/ValidLogin.json");

            loginPage.Login(data.Email, data.Password);
        }

        public void LoginAsInvalidUser()
        {
            var data = JsonReader.ReadJson<InvalidCredentials>(
                @"TestData/Login/InValidLogin.json");

            loginPage.Login(data.Email, data.Password);
        }
    }
}