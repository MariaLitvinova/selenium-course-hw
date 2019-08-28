using LitecartTesting.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class PricesTab : PageBase
    {
        public IWebElement PurchasePrice
            => webDriver.FindElement(By.Name("purchase_price"));

        public IWebElement PurchasePriceCurrencyCode
            => webDriver.FindElement(By.Name("purchase_price_currency_code"));

        public IWebElement PricesUSD
            => webDriver.FindElement(By.Name("prices[USD]"));

        public IWebElement PricesEUR
            => webDriver.FindElement(By.Name("prices[EUR]"));

        public PricesTab(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void FillInPricesTab()
        {
            wait.Until(driver => driver.FindElements(By.Name("purchase_price")).Count > 0);

            PurchasePrice.SendKeys(Keys.Control + "a" + Keys.Backspace);
            PurchasePrice.SendKeys("10");

            ExecuteScriptsHelper.SelectItemFromDropDown(PurchasePriceCurrencyCode, 1, webDriver);

            PricesUSD.SendKeys("10");
            PricesEUR.SendKeys("8");
        }
    }
}
