using MarsAdvancedAutomation.Assertions.ProfilePageAssertions;
using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Models;
using MarsAdvancedAutomation.Pages.ProfilePage;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MarsAdvancedAutomation.Tests.ProfileTests
{
    internal class LanguagesTest : BaseTest
    {
        private LoginHelper loginHelper;
        private LanguagesComponent languages;

        [SetUp]
        public void SetupTest()
        {
            loginHelper = new LoginHelper(driver);
            languages = new LanguagesComponent(driver);

            //  LOGIN HERE
            loginHelper.LoginAsValidUser();
        }

        //Add Language

        [Test]
        public void AddLanguageTest()
        {
            var json = File.ReadAllText(@"TestData\Languages\AddLanguage.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);

            // Act
     
            language.AddLanguage(
                (string)data.Language,
                (string)data.Level
            );

           

            // Track test data
            TestDataManager.AddLanguage((string)data.Language);

            // Assert

            LanguagesAssertions.VerifyLanguageAdded(
                                 language.IsLanguagePresent((string)data.Language),
                                     (string)data.Language); 
          
        }

        //Update Language

        [Test]

        public void UpdateLanguageTest()
        {
           
            var json = File.ReadAllText(@"TestData\Languages\AddLanguage.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);

            // Arrange
            language.AddLanguage(
                (string)data.Language,
                (string)data.Level
            );

            var json1 = File.ReadAllText(@"TestData\Languages\UpdateLanguage.json");
            dynamic data1 = JsonConvert.DeserializeObject(json1);

            // Act
            language.UpdateLanguage(
                (string)data1.ExistingLanguage,
                (string)data1.NewLanguage,
                (string)data1.NewLevel
            );


            // Track test data
            TestDataManager.AddLanguage((string)data.NewLanguage);

            // Assert


            LanguagesAssertions.VerifyLanguageUpdated(
                             language.IsLanguagePresent((string)data1.NewLanguage),
                                 (string)data1.NewLanguage);
         
        }

        //Delete Language
    
        [Test]
       
        public void DeleteLanguageTest()
        {
            var json = File.ReadAllText(@"TestData\Languages\DeleteLanguage.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);


            // Arrange
            language.AddLanguage(
                (string)data.Language,
                (string)data.Level
            );

            // Act
            language.DeleteLanguage(
                (string)data.Language
            );

            // Assert

            LanguagesAssertions.VerifyLanguageDeleted(
                                language.IsLanguagePresent((string)data.Language),
                                     (string)data.Language);
          
        }

        // Duplicate Language

        [Test]
        public void PreventDuplicateLanguageTest()
        {
            var json = File.ReadAllText(@"TestData\Languages\DuplicateLanguage.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);

            // Add first time
            language.AddLanguage(
                (string)data.Language,
                (string)data.Level
            );

            // Add same language again
            language.AddLanguage(
                (string)data.Language,
                (string)data.Level
            );


            ToastHelper toast = new ToastHelper(driver);

            string actualMessage = toast.GetMessage(By.CssSelector(".ns-box-inner"));

            // Track test data
            TestDataManager.AddLanguage((string)data.Language);

            //Assert

            LanguagesAssertions.VerifyDuplicateLanguageMessage(actualMessage);
           
        }

        // Prevent Morethan 4 Languages

        [Test]
     
        public void PreventMoreThanFourLanguagesTest()
        {

            var json = File.ReadAllText(@"TestData\Languages\MaximumLanguages.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);

            foreach (var item in data)
            {
                string languageName = (string)item.Language;
                string level = (string)item.Level;

                language.AddLanguage(languageName, level);

                TestDataManager.AddLanguage(languageName);
            }
            LanguagesAssertions.VerifyMaximumLanguagesReached(
                 language.IsAddNewButtonVisible()
             );

        }

        [TearDown]

        public void Cleanup()
        {
            foreach (var language in TestDataManager.LanguagesAdded)
            {
                if (languages.IsLanguagePresent(language))
                {
                    languages.DeleteLanguage(language);
                }
            }

            TestDataManager.Clear();
        }
    }
}

