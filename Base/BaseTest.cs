using MarsAdvancedAutomation.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarsAdvancedAutomation.Base
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            ExtentReportManager.InitializeReport();
        }

        [SetUp]
        public void Setup()
        {
            //initialize driver FIRST
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5003/");

            // Extent test
            ExtentReportManager.Test =
                ExtentReportManager.Extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                string path = ScreenshotHelper.CaptureScreenshot(driver, "PASS_" + TestContext.CurrentContext.Test.Name);

                ExtentReportManager.Test.Pass("Test Passed")
                    .AddScreenCaptureFromPath(path);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string path = ScreenshotHelper.CaptureScreenshot(driver, "FAIL_" + TestContext.CurrentContext.Test.Name);

                ExtentReportManager.Test.Fail(TestContext.CurrentContext.Result.Message)
                    .AddScreenCaptureFromPath(path);
            }

            driver.Quit();
            driver.Dispose();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            ExtentReportManager.FlushReport();
        }
    }
}