using MarsAdvancedAutomation.Helpers;
using OpenQA.Selenium;
using NUnit.Framework;

namespace MarsAdvancedAutomation.Base
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverManager.GetDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5003/");
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}



/*        /*LoginPage loginPage = new LoginPage(driver);

        //loginPage.Login(
         //   "susmitha.pinki@gmail.com",
          //  "123123#");

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        wait.Until(d =>
            d.FindElement(By.Id("account-profile-section")).Displayed);
    }*/


/*[SetUp]
public void Setup()
{
    driver = DriverManager.GetDriver();
    driver.Navigate().GoToUrl("http://localhost:5003/");
}

[SetUp]
        public void Setup()
         {
          driver = DriverManager.GetDriver();

          driver.Navigate().GoToUrl("http://localhost:5003/");

          LoginPage loginPage = new LoginPage(driver);

          loginPage.Login(
              "susmitha.pinki@gmail.com",
              "123123#");

          WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

          wait.Until(d =>
              d.FindElement(By.Id("account-profile-section")).Displayed);
      }
          


    [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}*/