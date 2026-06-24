using MarsAdvancedAutomation.Assertions;
using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Pages.ProfilePage;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using RazorEngine;
using System.IO;

namespace MarsAdvancedAutomation.Tests
{
    public class SkillsTests : BaseTest
    {
        private LoginHelper loginHelper;
        private SkillsComponent skills;

        [SetUp]
        public void SetupTest()
        {
            loginHelper = new LoginHelper(driver);
            skills = new SkillsComponent(driver);

            // LOGIN HERE
            loginHelper.LoginAsValidUser();
        }

        // -----------------------------
        // ADD SKILL TEST
        // -----------------------------
        [Test]
        public void AddSkillTest()
        {
            var json = File.ReadAllText(@"TestData\SkillsData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            skills.AddSkill(
                (string)data.Add.Skill,
                (string)data.Add.Level);

            // Track for cleanup
            TestDataManager.AddSkill((string)data.Add.Skill);

            //Assert
            SkillsAssertions.VerifySkillAdded(
                                skills.IsSkillPresent((string)data.Add.Skill),(string)data.Add.Skill);
        }

        // -----------------------------
        // UPDATE SKILL TEST
        // -----------------------------
        [Test]
        public void UpdateSkillTest()
        {
            var json = File.ReadAllText(@"TestData\SkillsData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            // Arrange - add first
            skills.AddSkill(
                (string)data.Add.Skill,
                (string)data.Add.Level);

            // Act - update
            skills.UpdateSkill(
                (string)data.Update.ExistingSkill,
                (string)data.Update.NewSkill,
                (string)data.Update.NewLevel);

            // Track UPDATED skill for cleanup
            TestDataManager.AddSkill((string)data.Update.NewSkill);

            // Assert
            SkillsAssertions.VerifySkillUpdated(
                                          skills.IsSkillPresent((string)data.Update.NewSkill),(string)data.Update.NewSkill);
        }

        // -----------------------------
        // DELETE SKILL TEST
        // -----------------------------
        [Test]
        public void DeleteSkillTest()
        {
            var json = File.ReadAllText(@"TestData\SkillsData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            // Arrange - add first
            skills.AddSkill(
                (string)data.Add.Skill,
                (string)data.Add.Level);

            

            // Act - delete
            skills.DeleteSkill((string)data.Add.Skill);

            // Assert
            SkillsAssertions.VerifySkillDeleted( skills.IsSkillPresent((string)data.Add.Skill), (string)data.Add.Skill);
        }

        //-----------------------------------------
        //-----------Diplicate Test----//
        //---------------------------------------

        [Test]
        public void AddDuplicateSkillTest()
        {
            var json = File.ReadAllText(@"TestData\SkillsData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            SkillsComponent skills = new SkillsComponent(driver);

            string skill = (string)data.Add.Skill;
            string level = (string)data.Add.Level;

            // -----------------------
            // Arrange - add first time
            // -----------------------
            skills.AddSkill(skill, level);


            // -----------------------
            // Act - try duplicate add
            // -----------------------
            skills.AddSkill(skill, level);

            // -----------------------
            // Assert - validate warning message
            // -----------------------
            string message = skills.GetNotificationMessage();

            ToastHelper toast = new ToastHelper(driver);

            string actualMessage = toast.GetMessage(By.CssSelector(".ns-box-inner"));


            // Track ADD skill for cleanup
            TestDataManager.AddSkill((string)data.Add.Skill);

            SkillsAssertions.VerifyDuplicateSkillMessage(actualMessage);
        }

         

        // -----------------------------
        // CLEANUP AFTER TEST
        // -----------------------------
        [TearDown]
        public void Cleanup()
        {
            foreach (var skill in TestDataManager.SkillsAdded)
            {
                if (skills.IsSkillPresent(skill))
                {
                    skills.DeleteSkill(skill);
                }
            }

            TestDataManager.Clear();
        }
    }
}