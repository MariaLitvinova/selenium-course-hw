using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LitecartTesting.Pages.AdministrationMenu
{
    public class CatalogPage : PageBase
    {
        public IWebElement CreateNewProductButton
            => webDriver.FindElement(By.LinkText("Add New Product"));

        public IWebElement Table
            => webDriver.FindElement(By.ClassName("dataTable"));

        public CatalogPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public string CreateNewProduct()
        {
            WaitUntilHeaderAppears("Catalog");

            CreateNewProductButton.Click();

            var createNewProductPage = new CreateNewProductPage(webDriver, wait);
            var createdProductName = createNewProductPage.CreateNewProduct();

            var products = Table.FindElements(By.CssSelector("tr.row"));

            var productNames = products
                .Select(x =>
                {
                    var columns = x.FindElements(By.CssSelector("td"));
                    return columns[2].Text;
                })
                .ToList();

            Assert.Contains(createdProductName, productNames);

            return createdProductName;
        }
    }
}
