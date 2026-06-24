using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAdvancedAutomation.Pages.ProfilePage
{
    public class SkillsComponent
    {
        private readonly IWebDriver driver;

        public SkillsComponent(IWebDriver driver)
        {
            this.driver = driver;
        }

        // -----------------------------
        // LOCATORS
        // -----------------------------

        private readonly By skillsTab =
            By.XPath("//a[@data-tab='second']");

        private readonly By addNewButton =
            By.XPath("//div[@data-tab='second']//div[text()='Add New']");

        private readonly By skillTextbox =
            By.Name("name");

        private readonly By levelDropdown =
            By.Name("level");

        private readonly By addButton =
            By.XPath("//input[@value='Add']");

        // -----------------------------
        // OPEN SKILLS TAB
        // -----------------------------

        public void OpenSkillsTab()
        {
            driver.FindElement(skillsTab).Click();
        }

        // -----------------------------
        // ADD SKILL
        // -----------------------------

        public void AddSkill(string skill, string level)
        {
            OpenSkillsTab();

            driver.FindElement(addNewButton).Click();

            // wait until textbox exists
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(skillTextbox).Count > 0);

            driver.FindElement(skillTextbox).SendKeys(skill);

            new SelectElement(driver.FindElement(levelDropdown))
                .SelectByText(level);

            driver.FindElement(addButton).Click();
        }

      

        // -----------------------------
        // UPDATE SKILL
        // -----------------------------

        public void UpdateSkill(
            string existingSkill,
            string newSkill,
            string newLevel)
        {
            OpenSkillsTab();

            var row = driver.FindElement(
                By.XPath(
                    $"//table/tbody/tr[td[1][text()='{existingSkill}']]"));

            row.FindElement(
                By.XPath(".//i[contains(@class,'write')]"))
                .Click();

            var skillField =
                driver.FindElement(skillTextbox);

            skillField.Clear();
            skillField.SendKeys(newSkill);

            new SelectElement(
                driver.FindElement(levelDropdown))
                .SelectByText(newLevel);

            driver.FindElement(
                By.XPath("//input[@value='Update']"))
                .Click();
        }

        // -----------------------------
        // DELETE SKILL
        // -----------------------------

        public void DeleteSkill(string skill)
        {
            OpenSkillsTab();

            var row = driver.FindElement(
                By.XPath(
                    $"//table/tbody/tr[td[1][text()='{skill}']]"));

            row.FindElement(
                By.XPath(".//i[contains(@class,'remove')]"))
                .Click();
        }

        // -----------------------------
        // VERIFY SKILL EXISTS
        // -----------------------------

        public bool IsSkillPresent(string skill)
        {
            OpenSkillsTab();

            return driver.FindElements(
                By.XPath(
                    $"//table/tbody/tr/td[text()='{skill}']"))
                .Count > 0;
        }

        // -----------------------------
        // DUPLICATE MESSAGE
        // -----------------------------

        public string GetNotificationMessage()
        {
            return driver.FindElement(
                By.XPath("//div[contains(@class,'ns-box-inner')]"))
                .Text;
        }
    }
}