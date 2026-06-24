using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RazorEngine;

namespace MarsAdvancedAutomation.Pages.ProfilePage
{
    public class LanguagesComponent
    {
        private readonly IWebDriver driver;

        public LanguagesComponent(IWebDriver driver)
        {
            this.driver = driver;
        }

        private readonly By LanguageNewBtn = By.XPath("//div[@data-tab='first']//div[text()='Add New']");
        private readonly By languageInput = By.Name("name");
        private readonly By levelDropdown = By.Name("level");
        private readonly By addBtn = By.XPath("//input[@value='Add']");
        private readonly By notificationMessage =By.XPath("//div[@class='ns-box-inner']");

        private readonly By languagesTab = By.XPath("//a[@data-tab='first']");

        public void OpenLanguagesTab()
        {
            driver.FindElement(languagesTab).Click();
        }

        //------------------Add Language-------------------//

        public void AddLanguage(string language, string level)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //  1. Click Add New button safely
            wait.Until(d => d.FindElement(LanguageNewBtn)).Click();

            // ✅ 2. Wait for input field to be visible
            var languageField = wait.Until(d =>
            {
                var el = d.FindElements(languageInput);
                return el.Count > 0 && el[0].Displayed ? el[0] : null;
            });

            languageField.Clear();
            languageField.SendKeys(language);

            // ✅ 3. Select dropdown
            new SelectElement(wait.Until(d => d.FindElement(levelDropdown)))
                .SelectByText(level);

            // ✅ 4. Click Add button
            wait.Until(d => d.FindElement(addBtn)).Click();

            // ✅ 5. Wait for table update
            wait.Until(d =>
                d.FindElements(By.XPath($"//table//td[contains(text(),'{language}')]")).Count > 0
            );
        }
        /*public void AddLanguage(string language, string level)
        {
            driver.FindElement(LanguageNewBtn).Click();

         

            driver.FindElement(languageInput).SendKeys(language);

            new SelectElement(driver.FindElement(levelDropdown))
                .SelectByText(level);

            driver.FindElement(addBtn).Click();

            //  WAIT HERE
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
                d.FindElements(By.XPath($"//table//td[contains(text(),'{language}')]"))
                  .Count > 0
            );
        }
        */
        public string GetAddedLanguage()
        {
            var element = driver.FindElement(
                By.XPath("//table/tbody[last()]/tr[last()]/td[1]")
            );

            return element.Text.Trim();
        }

        //----------------Update Language----------------------//

        public void UpdateLanguage(string existingLanguage, string newLanguage, string newLevel)
        {
            // locate row containing the language
            var row = driver.FindElement(By.XPath($"//table/tbody/tr[td[1][normalize-space()='{existingLanguage}']]"
               
            ));

            // click edit icon in same row
            row.FindElement(By.XPath(".//i[contains(@class,'write') or contains(@class,'edit')]")).Click();

            // update fields
            var languageField = driver.FindElement(By.Name("name"));
            languageField.Clear();
            languageField.SendKeys(newLanguage);

            var levelDropdownElement = driver.FindElement(By.Name("level"));
            new SelectElement(levelDropdownElement).SelectByText(newLevel);

            // save
            driver.FindElement(By.XPath("//input[@value='Update']")).Click();



            // IMPORTANT WAIT FOR UPDATE TO COMPLETE
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
                d.FindElements(By.XPath($"//table//td[contains(normalize-space(),'{newLanguage}')]"))
                  .Count > 0
            );
        }


        //--------------------------Delete language ----------------------------//


        public void DeleteLanguage(string language)
        {
            // Find the row containing the language
            var row = driver.FindElement(By.XPath(
                $"//table/tbody/tr[td[1][normalize-space()='{language}']]"
            ));

            // Click delete icon
            row.FindElement(
                By.XPath(".//i[contains(@class,'remove')]")
            ).Click();

            // Wait until language disappears from table
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d =>
                d.FindElements(
                    By.XPath($"//table//td[normalize-space()='{language}']")
                ).Count == 0
            );
        }

        //--------------Langauge Avaialble-------------------//

        public bool IsLanguagePresent(string language)
        {
            return driver.FindElements(
                By.XPath($"//table//td[normalize-space()='{language}']")
            ).Count > 0;
        }

        //--------------------- Notification Message----------------//

        public string GetNotificationMessage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement message = wait.Until(d =>
            {
                var el = d.FindElements(notificationMessage);

                if (el.Count > 0)
                {
                    string text = el[0].Text.Trim();

                    if (!string.IsNullOrEmpty(text))
                        return el[0];
                }

                return null;
            });

            return message.Text.Trim();
        }

        //------------------- Language Count--------------------//
        public int GetLanguageCount()
        {
            return driver.FindElements(By.XPath("//table//tr")).Count;
        }

        //----------Add New Button Available------------------//
        public bool IsAddNewButtonVisible()
        {
            return driver.FindElements(LanguageNewBtn).Count > 0;
        }
    }
}



