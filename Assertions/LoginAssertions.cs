using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAdvancedAutomation.Assertions
{
    public static class LoginAssertions
    {

        public static void VerifySuccessfulLogin(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
                d.FindElements(By.XPath("//a[contains(.,'Sign') or contains(.,'Logout')]")).Count > 0
            );

            Assert.That(
                driver.FindElements(By.XPath("//a[contains(.,'Sign') or contains(.,'Logout')]")).Count,
                Is.GreaterThan(0)
            );
        }

        public static void VerifyInvalidLogin(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // ✅ Wait for ns-box-inner notification (your actual UI)
            IWebElement errorMessage = wait.Until(d =>
            {
                var el = d.FindElements(By.CssSelector(".ns-box-inner"));
                return el.Count > 0 ? el[0] : null;
            });

            string actualMessage = errorMessage.Text.Trim();

            Assert.That(actualMessage, Is.Not.Empty, "Error message was not displayed");

            // Optional: confirm still on login page
            Assert.That(driver.Url, Does.Contain("5003"));
        }
    }
}
