

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarsAdvancedAutomation.Helpers
{
    public static class DriverManager
    {
        public static IWebDriver GetDriver()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            return driver;
        }
    }
}