using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MarsAdvancedAutomation.Helpers
{
    public class WaitHelper
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public WaitHelper(IWebDriver driver, int timeoutInSeconds = 10)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        // Wait until element is visible
        public IWebElement WaitForElementVisible(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return element.Displayed ? element : null;
            });
        }

        // Wait until element is clickable
        public IWebElement WaitForElementClickable(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return (element.Displayed && element.Enabled) ? element : null;
            });
        }

        // Wait until element exists in DOM
        public IWebElement WaitForElementExists(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return element != null ? element : null;
            });
        }

        // Wait for page load (JS readyState)
        public void WaitForPageLoad()
        {
            wait.Until(d =>
                ((IJavaScriptExecutor)d)
                .ExecuteScript("return document.readyState")
                .ToString()
                .Equals("complete", StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}