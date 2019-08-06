using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Task1
{
    [TestFixture]
    public class SimpleTest
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
        public void Test()
        {
            webDriver.Url = "http://google.com";

            webDriver.FindElement(By.Name("q")).SendKeys("selenium");
            webDriver.FindElement(By.Name("btnK")).Click();

            var result = wait.Until(driver => driver.Title.Contains("selenium - Поиск в Google"));
            Assert.IsTrue(result);
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }
    }
}
