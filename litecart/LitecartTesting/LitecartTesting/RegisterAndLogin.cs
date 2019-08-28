using LitecartTesting.Pages;
using LitecartTesting.Pages.StorePages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LitecartTesting
{
    [TestFixture]
    public class RegisterAndLogin
    {
        private IWebDriver webDriver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));
        }

        [Test]
        public void RegisterAndLoginTest()
        {
            var mainPage = new MainStorePage(webDriver, wait);
            mainPage.Load();

            mainPage.RegisterLink.Click();

            var registerPage = new RegisterPage(webDriver, wait);
            var (email, password) = registerPage.CreateAccount();

            mainPage.Logout.Click();
            mainPage.Login(email, password);

            mainPage.Logout.Click();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
