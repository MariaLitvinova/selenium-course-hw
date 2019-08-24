using LitecartTesting.Forms;
using LitecartTesting.Pages.AdministrationMenu;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LitecartTesting
{
    [TestFixture]
    public class CheckProductCreation
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
        public void CheckProductCreationTest()
        {
            var loginPage = new LoginPage(webDriver);
            loginPage.Login("admin", "admin");

            var administrationPage = new AdministrationPage(webDriver, wait);

            administrationPage.CatalogMenu.Click();

            var catalogPage = new CatalogPage(webDriver, wait);
            catalogPage.CreateNewProduct();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
