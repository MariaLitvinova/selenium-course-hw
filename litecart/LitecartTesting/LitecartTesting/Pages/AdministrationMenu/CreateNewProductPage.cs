using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

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

        public void CreateNewProduct()
        {
            GeneralTab.Click();
            var generalTab = new GeneralTab(webDriver, wait);
            generalTab.FillInGeneralTab();

            InformationTab.Click();

            PricesTab.Click();

            //SaveButton.Click();
        }
    }
}
