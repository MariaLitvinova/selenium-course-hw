using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class CreateNewProductPage : PageBase
    {
        public IWebElement Tabs
            => webDriver.FindElement(By.CssSelector(".tabs"));

        public IWebElement GeneralTab
            => Tabs.FindElement(By.LinkText("General"));

        public IWebElement InformationTab
            => Tabs.FindElement(By.LinkText("Information"));

        public IWebElement PricesTab
            => Tabs.FindElement(By.LinkText("Prices"));

        public IWebElement SaveButton
            => webDriver.FindElement(By.Name("save"));

        public CreateNewProductPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public string CreateNewProduct()
        {
            WaitUntilHeaderAppears("Add New Product");

            GeneralTab.Click();
            var generalTab = new GeneralTab(webDriver, wait);
            var productName = generalTab.FillInGeneralTab();

            InformationTab.Click();
            var informationTab = new InformationTab(webDriver, wait);
            informationTab.FillInInformationTab(productName);

            PricesTab.Click();
            var pricesTab = new PricesTab(webDriver, wait);
            pricesTab.FillInPricesTab();

            SaveButton.Click();

            return productName;
        }
    }
}
