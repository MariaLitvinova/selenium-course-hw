using LitecartTesting.Pages.StorePages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Linq;

namespace LitecartTesting.Pages
{
    public class MainStorePage : PageBase
    {
        private void CheckLabel(IWebElement duckElement)
        {
            var labelElements = duckElement.FindElements(By.ClassName("sticker"));

            Assert.AreEqual(1, labelElements.Count);
        }

        public ReadOnlyCollection<IWebElement> DucksList 
            => webDriver.FindElements(By.ClassName("product"));

        public IWebElement HomeButton
            => webDriver.FindElement(By.CssSelector("li.general-0 a"));

        public MainStorePage(IWebDriver webDriver, WebDriverWait wait) : base(webDriver, wait) { }

        public void Load()
        {
            webDriver.Url = "http://localhost/litecart/en/";

            wait.Until(driver => driver.Title.Contains("Online Store"));
        }

        public void CheckDuckLabels()
        {
            Assert.AreEqual(11, DucksList.Count);

            for (int i = 0; i < DucksList.Count; ++i)
            {
                CheckLabel(DucksList[i]);
            }
        }

        public void CheckProductStyles()
        {
            Assert.AreEqual(11, DucksList.Count);

            for (int i = 0; i < DucksList.Count; ++i)
            {
                var productName = DucksList[i].FindElement(By.CssSelector(".name"));
                var regularPrice = DucksList[i].FindElements(By.CssSelector(".regular-price")).FirstOrDefault()
                    ?? DucksList[i].FindElement(By.CssSelector(".price"));
                var campaignPrice = DucksList[i].FindElements(By.CssSelector(".campaign-price")).FirstOrDefault();

                var productNameText = productName.Text;
                var regularPriceText = regularPrice.Text;
                var campaignPriceText = campaignPrice?.Text;

                var link = DucksList[i].FindElement(By.CssSelector("a.link"));
                link.Click();

                var productPage = new ProductPage(webDriver, wait);
                productPage.CheckPageIsLoaded();

                productPage.CheckStylesForProduct(productNameText, regularPriceText, campaignPriceText);

                HomeButton.Click();
            }
        }
    }
}
