using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace MarsAdvancedAutomation.Pages.LoginPage
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private readonly By signInButton = By.XPath("//a[contains(.,'Sign In')]");
        private readonly By emailTextbox = By.Name("email");
        private readonly By passwordTextbox = By.Name("password");
        private readonly By loginButton = By.XPath("//button[contains(.,'Login')]");
        private readonly By loginErrorMessage = By.XPath("//div[contains(@class,'message') and contains(@class,'error')]");
        private readonly By signOutButton = By.XPath("//button[contains(.,'Sign Out') or contains(.,'Logout')]");



        public void Login(string email, string password)
        {
            // Click Sign In if exists
            var signIn = driver.FindElements(signInButton);
            if (signIn.Count > 0)
                signIn[0].Click();

            // WAIT FOR LOGIN FORM (IMPORTANT CHANGE)
            wait.Until(d => d.FindElements(emailTextbox).Count > 0);

            var emailField = driver.FindElement(emailTextbox);
            wait.Until(d => emailField.Displayed);
            emailField.Clear();
            emailField.SendKeys(email);

            var passwordField = driver.FindElement(passwordTextbox);
            passwordField.Clear();
            passwordField.SendKeys(password);

            driver.FindElement(loginButton).Click();
        }
        /*public void Login(string email, string password)
        {
            // Click visible Sign In button
            var signIn = driver.FindElements(signInButton)
                               .FirstOrDefault(e => e.Displayed);

            if (signIn != null)
                signIn.Click();

            // Wait for email field
            wait.Until(d => d.FindElement(emailTextbox).Displayed);

            driver.FindElement(emailTextbox).Clear();
            driver.FindElement(emailTextbox).SendKeys(email);

            driver.FindElement(passwordTextbox).Clear();
            driver.FindElement(passwordTextbox).SendKeys(password);

            // Wait for login button to be clickable
            wait.Until(d =>
            {
                var btn = d.FindElements(loginButton);
                return btn.Count > 0 && btn[0].Displayed && btn[0].Enabled;
            });

            driver.FindElement(loginButton).Click();

            // Wait for either redirect OR error message
            wait.Until(d =>
                d.Url.Contains("profile") ||
                d.FindElements(loginErrorMessage).Any(e => e.Displayed)
            );
        }*/

        public string GetLoginErrorMessage()
        {
            var msg = wait.Until(d =>
            {
                var els = d.FindElements(loginErrorMessage);
                return els.FirstOrDefault(e => e.Displayed);
            });

            return msg.Text.Trim();
        }
    }
}


/*
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MarsAdvancedAutomation.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Locators
        private readonly By signInButton = By.XPath("//a[contains(.,'Sign In')]");
        private readonly By emailTextbox = By.Name("email");
        private readonly By passwordTextbox = By.Name("password");
        private readonly By loginButton = By.XPath("//button[contains(.,'Login')]");
        private readonly By loginErrorMessage = By.XPath("//div[contains(@class,'message')]");

        // Actions
        public void ClickSignIn()
        {
            wait.Until(d => d.FindElement(signInButton)).Click();
        }

        public void EnterEmail(string email)
        {
            wait.Until(d => d.FindElement(emailTextbox)).SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            wait.Until(d => d.FindElement(passwordTextbox)).SendKeys(password);
        }

        public void ClickLogin()
        {
            driver.FindElement(loginButton).Click();
        }


        public void Login(string email, string password)
        {
            // Click Sign In only if available
            var signInElements = driver.FindElements(signInButton);
            if (signInElements.Count > 0)
                signInElements[0].Click();

            // WAIT until email field is visible (pure Selenium)
            wait.Until(d =>
            {
                var el = d.FindElements(emailTextbox);
                return el.Count > 0 && el[0].Displayed;
            });

            var emailField = driver.FindElement(emailTextbox);
            emailField.Clear();
            emailField.SendKeys(email);

            var passwordField = driver.FindElement(passwordTextbox);
            passwordField.Clear();
            passwordField.SendKeys(password);

            driver.FindElement(loginButton).Click();
        }

        /* public void Login(string email, string password)
         {
             ClickSignIn();
             EnterEmail(email);
             EnterPassword(password);
             ClickLogin();
         }
        
        // Validation
        public string GetLoginErrorMessage()
        {
            return wait.Until(d =>
                d.FindElement(loginErrorMessage).Text.Trim()
            );
        }

        public bool IsLoginErrorDisplayed()
        {
            return driver.FindElements(loginErrorMessage).Count > 0;
        }
    }
}



/*using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsAdvancedAutomation.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement SignInButton =>
            driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]"));

        private IWebElement EmailTextbox =>
            driver.FindElement(By.Name("email"));

        private IWebElement PasswordTextbox =>
            driver.FindElement(By.Name("password"));

        private IWebElement LoginButton =>
            driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));

        private readonly By loginErrorMessage =
            By.XPath("//div[contains(@class,'message')]");

        public void Login(string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.FindElement((By)SignInButton)).Click();

            wait.Until(d => d.FindElement((By)EmailTextbox)).SendKeys(email);

            wait.Until(d => d.FindElement((By)PasswordTextbox)).SendKeys(password);

            wait.Until(d => d.FindElement((By)LoginButton)).Click();
        }

        /* public void Login(string email, string password)
         {
             SignInButton.Click();
             EmailTextbox.SendKeys(email);
             PasswordTextbox.SendKeys(password);
             LoginButton.Click();
         }
        
public string GetLoginErrorMessage()
        {
            return driver.FindElement(loginErrorMessage)
                         .Text
                         .Trim();
        }
    }
}*/