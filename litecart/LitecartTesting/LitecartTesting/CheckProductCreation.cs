using LitecartTesting.Forms;
using LitecartTesting.Pages;
using LitecartTesting.Pages.AdministrationMenu;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

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
            var newProductName = catalogPage.CreateNewProduct();

            var mainStorePage = new MainStorePage(webDriver, wait);
            mainStorePage.Load();
            var ducksNames = mainStorePage.DucksList.Select(x =>
            {
                var name = x.FindElement(By.ClassName("name"));
                return name.Text;
            }).ToList();

            Assert.Contains(newProductName, ducksNames);
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
