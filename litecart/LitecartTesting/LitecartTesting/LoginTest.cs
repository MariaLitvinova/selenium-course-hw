using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LitecartTesting
{
    [TestFixture]
    public class LoginTest
    {
        private IWebDriver webDriver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            webDriver = new ChromeDriver();
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
        }

        [Test]
        public void LoginWithCorrectCredentials()
        {
            webDriver.Url = "http://localhost/litecart/admin/";

            webDriver.FindElement(By.Name("username")).SendKeys("admin");
            webDriver.FindElement(By.Name("password")).SendKeys("admin");

            webDriver.FindElement(By.Name("login")).Click();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
