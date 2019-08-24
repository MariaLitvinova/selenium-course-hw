using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class CatalogPage : PageBase
    {
        public IWebElement CreateNewProductButton
            => webDriver.FindElement(By.LinkText("Add New Product"));

        public CatalogPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public void CreateNewProduct()
        {
            WaitUntilHeaderAppears("Catalog");

            CreateNewProductButton.Click();

            var createNewProductPage = new CreateNewProductPage(webDriver, wait);
            createNewProductPage.CreateNewProduct();
        }
    }
}
