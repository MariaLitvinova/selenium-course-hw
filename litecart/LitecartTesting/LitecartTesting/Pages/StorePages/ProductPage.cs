using System.Linq;
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

        public IWebElement AddToCart => webDriver.FindElement(By.Name("add_cart_product"));

        public IWebElement SizeOptions => webDriver.FindElements(By.Name("options[Size]")).FirstOrDefault();

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

        public void CheckStylesForProduct(
            string productName, 
            string usualPrice, 
            string campaignPrice,
            string webDriverName)
        {
            Assert.AreEqual(productName, Title.Text);
            Assert.AreEqual(usualPrice, RegularPrice.Text);
            Assert.AreEqual(campaignPrice, CampaignPrice.Text);

            StylesHelper.CheckProductStyles(RegularPrice, CampaignPrice, webDriverName);
        }

        public void BuyProduct()
        {
            if (SizeOptions != null)
            {
                ExecuteScriptsHelper.SelectItemFromDropDown(SizeOptions, 2, webDriver);
            }

            AddToCart.Click();
        }
    }
}
