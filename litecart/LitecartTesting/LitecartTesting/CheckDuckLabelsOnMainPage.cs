using LitecartTesting.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LitecartTesting
{
    [TestFixture]
    public class CheckDuckLabelsOnMainPage
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
        public void CheckDuckLabelsOnMainPageTest()
        {
            var mainPage = new MainStorePage(webDriver, wait);
            mainPage.Load();

            mainPage.CheckDuckLabels();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
