using LitecartTesting.Forms;
using LitecartTesting.Pages;
using LitecartTesting.Pages.AdministrationMenu;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LitecartTesting
{
    [TestFixture]
    public class CheckProductStyles
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
        public void CheckProductStylesTest()
        {
            var mainPage = new MainStorePage(webDriver, wait);
            mainPage.Load();

            mainPage.CheckYellowDuckStyle();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
