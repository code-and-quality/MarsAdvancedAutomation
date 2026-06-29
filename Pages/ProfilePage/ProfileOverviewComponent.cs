using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MarsAdvancedAutomation.Pages.ProfilePage
{
    public class ProfileOverviewComponent
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // Edit Icons
        private readonly By AvailabilityEditIconLocator =
            By.XPath("(//*[@id='account-profile-section']//i[contains(@class,'write')])[1]");

        private readonly By HoursEditIconLocator =
            By.XPath("(//*[@id='account-profile-section']//i[contains(@class,'write')])[2]");

        private readonly By EarnTargetEditIconLocator =
            By.XPath("(//*[@id='account-profile-section']//i[contains(@class,'write')])[3]");

        // Dropdowns
        private readonly By AvailabilityDropdownLocator =
            By.Name("availabiltyType");

        private readonly By HoursDropdownLocator =
            By.Name("availabiltyHour");

        private readonly By EarnTargetDropdownLocator =
            By.Name("availabiltyTarget");

        public ProfileOverviewComponent(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement WaitForClickable(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return element.Displayed && element.Enabled ? element : null;
            });
        }

        private IWebElement WaitForVisible(By locator)
        {
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return element.Displayed ? element : null;
            });
        }

        /// <summary>
        /// Generic method to update any dropdown field
        /// </summary>
        private void SelectDropdownValue(
            By editIconLocator,
            By dropdownLocator,
            string value)
        {
            var editIcon = WaitForClickable(editIconLocator);

            ((IJavaScriptExecutor)driver)
                .ExecuteScript("arguments[0].scrollIntoView(true);", editIcon);

            editIcon.Click();

            var dropdown = WaitForVisible(dropdownLocator);

            new SelectElement(dropdown).SelectByText(value);
        }

        // ---------------- Availability ----------------

        public void UpdateAvailability(string availability)
        {
            SelectDropdownValue(
                AvailabilityEditIconLocator,
                AvailabilityDropdownLocator,
                availability);
        }

        public string GetSelectedAvailability()
        {
            var dropdown = WaitForVisible(AvailabilityDropdownLocator);

            return new SelectElement(dropdown)
                .SelectedOption
                .Text
                .Trim();
        }

        // ---------------- Hours ----------------

        public void UpdateHours(string hours)
        {
            SelectDropdownValue(
                HoursEditIconLocator,
                HoursDropdownLocator,
                hours);
        }

        public string GetSelectedHours()
        {
            var dropdown = WaitForVisible(HoursDropdownLocator);

            return new SelectElement(dropdown)
                .SelectedOption
                .Text
                .Trim();
        }

        // ---------------- Earn Target ----------------

        public void UpdateEarnTarget(string earnTarget)
        {
            SelectDropdownValue(
                EarnTargetEditIconLocator,
                EarnTargetDropdownLocator,
                earnTarget);
        }

        public string GetSelectedEarnTarget()
        {
            var dropdown = WaitForVisible(EarnTargetDropdownLocator);

            return new SelectElement(dropdown)
                .SelectedOption
                .Text
                .Trim();
        }
        /*public string GetSelectedEarnTarget()
        {
            // Wait until dropdown closes after selection
            wait.Until(d =>
            {
                var elements = d.FindElements(EarnTargetDropdownLocator);
                return elements.Count == 0;
            });

            var earnTargetText = WaitForVisible(
                By.XPath("//strong[text()='Earn Target']/ancestor::div[contains(@class,'item')]//div[@class='right floated content']/span")
            );

            return earnTargetText.Text.Trim();
        }*/
    }
}

/*using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MarsAdvancedAutomation.Components
{
    public class ProfileOverviewComponent
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By EditIconLocator =
            By.XPath("//*[@id='account-profile-section']//i[contains(@class,'write') or contains(@class,'edit')]");

        private readonly By AvailabilityDropdownLocator =
            By.Name("availabiltyType");   // correct spelling from the site
        
        
        private readonly By HoursEditIconLocator =
            By.XPath("(//*[@id='account-profile-section']//i[contains(@class,'write')])[2]");
      
        private readonly By EarnTargetEditIconLocator =
            By.XPath("(//*[@id='account-profile-section']//i[contains(@class,'write')])[3]");

        //private readonly By EarnTargetEditIconLocator =
        // By.XPath("//*[@id='account-profile-section']//div[4]//span/i");

        private readonly By EarnTargetDropdownLocator =
            By.Name("availabiltyTarget");

        //private readonly By HoursEditIconLocator =
        //By.XPath("(//*[@id='account-profile-section']//i[contains(@class,'write')])[2]");


        //private IWebElement HoursEditIcon =>
        // driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[2]/div/div/div/div/div/div[3]/div/div[3]/div/span/i"));

        private IWebElement HoursDropdown =>
            driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[2]/div/div/div/div/div/div[3]/div/div[3]/div/span/select"));

        public ProfileOverviewComponent(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement WaitForClickable(By locator)
        {
            return wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return (el.Displayed && el.Enabled) ? el : null;
            });
        }

        private IWebElement WaitForVisible(By locator)
        {
            return wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return el.Displayed ? el : null;
            });
        }
        public void UpdateAvailability(string availability)
        {
            // Scroll edit icon into view
            var editIcon = WaitForClickable(EditIconLocator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", editIcon);
            editIcon.Click();

            // Wait for dropdown to appear
            var dropdown = WaitForVisible(AvailabilityDropdownLocator);

            // Select value
            new SelectElement(dropdown).SelectByText(availability);
        }

      
        public string GetSelectedAvailability()
        {
            var dropdown = WaitForVisible(AvailabilityDropdownLocator);
            var select = new SelectElement(dropdown);
            return select.SelectedOption.Text;
        }
        //---------------Update Hours---------------------//

        public void UpdateHours(string hours)
        {
            // Scroll and click Hours edit icon
            var hoursIcon = WaitForClickable(HoursEditIconLocator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", hoursIcon);
            hoursIcon.Click();

            // Wait for Hours dropdown
            var hoursDropdown = WaitForVisible(By.Name("availabiltyHour"));

            // Select value
            new SelectElement(hoursDropdown).SelectByText(hours);
        }



        public string GetSelectedHours()
        {
            var hoursDropdown = WaitForVisible(By.Name("availabiltyHour"));
            var select = new SelectElement(hoursDropdown);

            return select.SelectedOption.Text.Trim();
        }


        //--------------------Update Earn Target--------------------//

        public void UpdateEarnTarget(string earnTarget)
        {
            // Scroll and click Earn Target edit icon
            var icon = WaitForClickable(EarnTargetEditIconLocator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", icon);
            icon.Click();

            // Wait for dropdown
            var dropdown = WaitForVisible(By.Name("availabiltyTarget"));

            // Select value
            new SelectElement(dropdown).SelectByText(earnTarget);
        }

        public string GetSelectedEarnTarget()
        {
            // Wait until dropdown disappears
            wait.Until(d =>
            {
                var elements = d.FindElements(By.Name("availabiltyTarget"));
                return elements.Count == 0;   // dropdown is gone
            });

            // Now read the display text
            var earnTargetText = WaitForVisible(
                By.XPath("//strong[text()='Earn Target']/ancestor::div[contains(@class,'item')]//div[@class='right floated content']/span")
            );

            return earnTargetText.Text.Trim();
        }




    }
}
*/