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
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var skillsTab = wait.Until(d =>
                d.FindElement(By.XPath("//a[@data-tab='second']"))
            );

            skillsTab.Click();
        }
        /*public void OpenSkillsTab()
        {
            driver.FindElement(skillsTab).Click();
        }*/

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


        public void UpdateSkill(string existingSkill, string newSkill, string newLevel)
        {
            OpenSkillsTab();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // 1️⃣ Wait for the row containing the existing skill
            var row = wait.Until(d =>
                d.FindElement(By.XPath(
                    "//table[.//th[normalize-space()='Skill']]//tbody/tr[td[1][normalize-space()='" + existingSkill + "']]"
                ))
            );

            // 2️⃣ Click the edit icon inside that row
            row.FindElement(By.XPath(".//i[contains(@class,'write')]")).Click();

            // 3️⃣ Update skill name
            var skillField = wait.Until(d => d.FindElement(skillTextbox));
            skillField.Clear();
            skillField.SendKeys(newSkill);

            // 4️⃣ Update level
            var levelSelect = new SelectElement(wait.Until(d => d.FindElement(levelDropdown)));
            levelSelect.SelectByText(newLevel);

            // 5️⃣ Click Update button
            wait.Until(d => d.FindElement(By.XPath("//input[@value='Update']"))).Click();

            // 6️⃣ Optional: wait for updated row to appear
            wait.Until(d =>
                d.FindElement(By.XPath(
                    "//table[.//th[normalize-space()='Skill']]//tbody/tr[td[1][normalize-space()='" + newSkill + "']]"
                ))
            );
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