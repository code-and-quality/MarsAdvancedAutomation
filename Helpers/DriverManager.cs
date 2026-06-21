using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarsAdvancedAutomation.Helpers
{
        public class DriverManager
        {
            public static IWebDriver GetDriver()
            {
                IWebDriver driver = new ChromeDriver();

                driver.Manage().Window.Maximize();

                return driver;
            }
        }
    }
    /*public class DriverManager
    {
        public static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
            }

            return driver;
        }
    }*/
