using MarsAdvancedAutomation.Assertions;
using MarsAdvancedAutomation.Base;
using MarsAdvancedAutomation.Helpers;
using MarsAdvancedAutomation.Models;
using MarsAdvancedAutomation.Pages.ProfilePage;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MarsAdvancedAutomation.Tests
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
        public void TC3_AddLanguageTest()
        {
            var json = File.ReadAllText(@"TestData\LanguagesData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);

            // Act
     
            language.AddLanguage(
                (string)data.Add.Language,
                (string)data.Add.Level
            );

           

            // Track test data
            TestDataManager.AddLanguage((string)data.Add.Language);

            // Assert

            LanguagesAssertions.VerifyLanguageAdded(
                                 language.IsLanguagePresent((string)data.Add.Language),
                                     (string)data.Add.Language); 
          
        }

        //Update Language

        [Test]

        public void UpdateLanguageTest()
        {
            var json = File.ReadAllText(@"TestData\LanguagesData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);

            // Arrange
            language.AddLanguage(
                (string)data.Add.Language,
                (string)data.Add.Level
            );

            // Act
            language.UpdateLanguage(
                (string)data.Update.ExistingLanguage,
                (string)data.Update.NewLanguage,
                (string)data.Update.NewLevel
            );


            // Track test data
            TestDataManager.AddLanguage((string)data.Update.NewLanguage);

            // Assert


            LanguagesAssertions.VerifyLanguageUpdated(
                             language.IsLanguagePresent((string)data.Update.NewLanguage),
                                 (string)data.Update.NewLanguage);
         
        }

        //Delete Language
    
        [Test]
       
        public void DeleteLanguageTest()
        {
            var json = File.ReadAllText(@"TestData\LanguagesData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);


            // Arrange
            language.AddLanguage(
                (string)data.Add.Language,
                (string)data.Add.Level
            );

            // Act
            language.DeleteLanguage(
                (string)data.Delete.Language
            );

            // Assert

            LanguagesAssertions.VerifyLanguageDeleted(
                                language.IsLanguagePresent((string)data.Delete.Language),
                                     (string)data.Delete.Language);
          
        }

        // Duplicate Language

        [Test]
        public void PreventDuplicateLanguageTest()
        {
            var json = File.ReadAllText(@"TestData\LanguagesData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);

            // Add first time
            language.AddLanguage(
                (string)data.Add.Language,
                (string)data.Add.Level
            );

            // Add same language again
            language.AddLanguage(
                (string)data.Add.Language,
                (string)data.Add.Level
            );


            ToastHelper toast = new ToastHelper(driver);

            string actualMessage = toast.GetMessage(By.CssSelector(".ns-box-inner"));

            // Track test data
            TestDataManager.AddLanguage((string)data.Add.Language);

            //Assert

            LanguagesAssertions.VerifyDuplicateLanguageMessage(actualMessage);
           
        }

        // Prevent Morethan 4 Languages

        [Test]
        public void PreventMoreThanFourLanguagesTest()
        {
            var json = File.ReadAllText(@"TestData\LanguagesData.json");
            dynamic data = JsonConvert.DeserializeObject(json);

            LanguagesComponent language = new LanguagesComponent(driver);

            for (int i = 1; i <= 4; i++)
            {
                string languageName = (string)data.Add.Language + i;

                language.AddLanguage(
                    languageName,
                    (string)data.Add.Level
                );

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

