using MarsAdvancedAutomation.Assertions.ProfilePageAssertions;
using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Pages.ProfilePage;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using RazorEngine;
using System.IO;

namespace MarsAdvancedAutomation.Tests.ProfileTests
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
            var json = File.ReadAllText(@"TestData\Skills\AddSkill.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            skills.AddSkill(
                (string)data.Skill,
                (string)data.Level);

            // Track for cleanup
            TestDataManager.AddSkill((string)data.Skill);

            //Assert
            SkillsAssertions.VerifySkillAdded(
                                skills.IsSkillPresent((string)data.Skill),(string)data.Skill);
        }

        // -----------------------------
        // UPDATE SKILL TEST
        // -----------------------------
        [Test]
        public void UpdateSkillTest()
        {
            var json = File.ReadAllText(@"TestData\Skills\AddSkill.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            // Arrange - add first
            skills.AddSkill(
                (string)data.Skill,
                (string)data.Level);

            var json1 = File.ReadAllText(@"TestData\Skills\UpdateSkill.json");
            dynamic data1 = JsonConvert.DeserializeObject(json1);
            // Act - update
            skills.UpdateSkill(
                (string)data1.ExistingSkill,
                (string)data1.NewSkill,
                (string)data1.NewLevel);

            // Track UPDATED skill for cleanup
            TestDataManager.AddSkill((string)data.NewSkill);

            // Assert
            SkillsAssertions.VerifySkillUpdated(
                                          skills.IsSkillPresent((string)data1.NewSkill),(string)data1.NewLevel);
        }

        // -----------------------------
        // DELETE SKILL TEST
        // -----------------------------
        [Test]
        public void DeleteSkillTest()
        {
            var json = File.ReadAllText(@"TestData\Skills\DeleteSkill.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            // Arrange - add first
            skills.AddSkill(
                (string)data.Skill,
                (string)data.Level);

            

            // Act - delete
            skills.DeleteSkill((string)data.Skill);

            // Assert
            SkillsAssertions.VerifySkillDeleted( skills.IsSkillPresent((string)data.Skill), (string)data.Skill);
        }

        //-----------------------------------------
        //-----------Diplicate Test----//
        //---------------------------------------

        [Test]
        public void AddDuplicateSkillTest()
        {
            var json = File.ReadAllText(@"TestData\Skills\DuplicateSkill.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            SkillsComponent skills = new SkillsComponent(driver);

            string skill = (string)data.Skill;
            string level = (string)data.Level;

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
            TestDataManager.AddSkill((string)data.Skill);

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