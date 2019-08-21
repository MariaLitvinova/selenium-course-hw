using LitecartTesting.Helpers;
using LitecartTesting.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LitecartTesting
{
    [TestFixtureSource(typeof(MyFixtureData), "FixtureParameters")]
    public class CheckProductStyles
    {
        private IWebDriver webDriver;
        private WebDriverWait wait;
        private string webDriverName;

        public CheckProductStyles(Func<IWebDriver> creator)
        {
            webDriver = creator();
            webDriverName = webDriver.GetType().Name;
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));
        }

        [Test]
        public void CheckProductStylesTest()
        {
            var mainPage = new MainStorePage(webDriver, wait);
            mainPage.Load();

            mainPage.CheckYellowDuckStyle(webDriverName);
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
