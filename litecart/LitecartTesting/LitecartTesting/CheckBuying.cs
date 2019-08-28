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
    public class CheckBuying
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
        public void CheckBuyingTest()
        {
            var mainStorePage = new MainStorePage(webDriver, wait);
            mainStorePage.Load();

            for (int i = 0; i < 3; ++i)
            {
                mainStorePage.BuyFirstDuck();
            }

            mainStorePage.Checkout.Click();

            var cartPage = new CartPage(webDriver, wait);
            cartPage.RemoveAllItemsFromCart();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            webDriver = null;
        }
    }
}
