using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MarsAdvancedAutomation.Helpers
{
    public class ToastHelper
    {
        private readonly IWebDriver driver;

        public ToastHelper(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetMessage(By toastLocator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);

            IWebElement toast = wait.Until(d =>
            {
                var el = d.FindElements(toastLocator);

                if (el.Count > 0)
                {
                    string text = el[0].Text.Trim();

                    if (!string.IsNullOrEmpty(text))
                        return el[0];
                }

                return null;
            });

            return toast.Text.Trim();
        }
    }
}