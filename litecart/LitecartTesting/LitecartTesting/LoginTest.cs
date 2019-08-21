using LitecartTesting.Forms;
using LitecartTesting.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections;

namespace LitecartTesting
{
    [TestFixtureSource(typeof(MyFixtureData), "FixtureParameters")]
    public class ParameterizedLogin
    {
        private IWebDriver webDriver;

        public ParameterizedLogin(Func<IWebDriver> creator)
        {
            webDriver = creator();
        }

        [Test]
        public void Login()
        {
            var loginPage = new LoginPage(webDriver);
            loginPage.Login("admin", "admin");
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
