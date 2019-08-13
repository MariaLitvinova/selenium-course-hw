using LitecartTesting.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LitecartTesting
{
    [TestFixture]
    public class LoginAndClickOnMenuItems
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
        public void LoginAndClickOnMenuItemsTest()
        {
            var loginPage = new LoginPage(webDriver);
            loginPage.Login("admin", "admin");

            var administrationPage = new AdministrationPage(webDriver, wait);
            administrationPage.ClickOnAllMenuItems();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
