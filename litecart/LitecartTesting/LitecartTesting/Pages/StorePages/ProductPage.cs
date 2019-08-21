using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitecartTesting.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTesting.Pages.StorePages
{
    public class ProductPage : PageBase
    {
        public IWebElement ProductBox => webDriver.FindElement(By.CssSelector("#box-product"));

        public IWebElement Title => ProductBox.FindElement(By.CssSelector(".title"));

        public IWebElement RegularPrice
            => ProductBox.FindElements(By.CssSelector(".regular-price")).FirstOrDefault();

        public IWebElement CampaignPrice
            => ProductBox.FindElements(By.CssSelector(".campaign-price")).FirstOrDefault();

        public ProductPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        public void CheckPageIsLoaded()
        {
            wait.Until(driver => driver.Title.Contains("Rubber Ducks"));
        }

        public void CheckStylesForProduct(string productName, string usualPrice, string campaignPrice)
        {
            Assert.AreEqual(productName, Title.Text);
            Assert.AreEqual(usualPrice, RegularPrice.Text);
            Assert.AreEqual(campaignPrice, CampaignPrice.Text);

            StylesHelper.CheckProductStyles(RegularPrice, CampaignPrice);
        }
    }
}
